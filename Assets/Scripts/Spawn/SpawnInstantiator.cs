using System.Collections;
using System;
using UnityEngine;
using System.Diagnostics.Tracing;

public class SpawnInstantiator : MonoBehaviour
{
    public float difficultyMultiplier = 1;

    [SerializeField] private GameObject particleSysPrefab;
    private SpawnField[] spawnFields; 
    [HideInInspector] public static SpawnInstantiator main;


    private void Start() 
    {
        main = this;
        spawnFields = FindObjectsOfType<SpawnField>();
    }

    public void InstantiateWave(SpawnWave wave)
    {
        // Cast the spawnMix to the appropriate type
        if (wave.spawnMix is SpawnMix<ItemEnum> itemMix)
        {
            // Convert ItemEnum[] to System.Enum[]
            System.Enum[] systemEnums = Array.ConvertAll(itemMix.names, item => (System.Enum)item);
            print(wave.spawnMix);
            print(systemEnums);
        }
        else if (wave.spawnMix is SpawnMix<MobEnum> mobMix)
        {
            // Convert MobEnum[] to System.Enum[]
            System.Enum[] systemEnums = Array.ConvertAll(mobMix.names, mob => (System.Enum)mob);
            print(wave.spawnMix);
            print(systemEnums);
        }
        else
        {
            Debug.LogError("Unknown spawn mix type");
        }

        // Compute default spawnRates
        float[] defaultSpawnRates = new float[systemEnums.Length];
        for (int i = 0; i < defaultSpawnRates.Length; i++)
        {
            float SumIntArray(int[] arr)
            {
                int sum = 0;
                foreach (int x in arr)
                {
                    sum += x;
                }

                return sum;
            }
            float mobProbability = wave.spawnMix.odds[i] / SumIntArray(wave.spawnMix.odds);
            float mobCount = (int)(mobProbability * wave.count);
            float spawnRate = mobCount / wave.duration;
            defaultSpawnRates[i] = spawnRate;
        }

        // Spawn all the mob enums in separate coroutines
        for (int i = 0; i < systemEnums.Length; i++)
        {
            float modifiedSpawnRate = defaultSpawnRates[i];
            StartCoroutine(Spawning(systemEnums[i], modifiedSpawnRate, wave.duration));   
        }
    }


    private IEnumerator Spawning(System.Enum systemEnum, float mobs_perMinute, float durationInMinutes)
    {
        float SPAWN_STEP = 1f;
        float spawnParts_perSpawnStep = mobs_perMinute / 60 * SPAWN_STEP;
        
        float spawnParts = 0;
        for (float t = 0; t < durationInMinutes * 60; t += SPAWN_STEP)
        {
            // If we can spawn one whole mob
            if (spawnParts >= 1)
            {
                int wholeMobs = (int) spawnParts;
                spawnParts -= wholeMobs;
                SpawnMobs(systemEnum, wholeMobs);
            }

            // Adding mob part
            spawnParts += spawnParts_perSpawnStep * difficultyMultiplier;
            yield return new WaitForSeconds(SPAWN_STEP);
        } 
    }
    private void SpawnMobs(System.Enum systemEnum, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnMob(systemEnum);
        }
    }
    private void SpawnMob(System.Enum systemEnum)
    {
        SpawnField RandomizeSpawnField()
        {
            int index = Random.Range(0, (spawnFields.Length - 1));
            return spawnFields[index];
        }
        SpawnField spawnField = RandomizeSpawnField();

        Vector2 spawnPoint = spawnField.RequestViableSpawnPoint();
        GameObject mobPrefab = SpawnPrefabManager.main.GetPrefab(systemEnum);

        StartCoroutine(DoSpawnEntity(mobPrefab, spawnPoint));
    }


    private IEnumerator DoSpawnEntity(GameObject prefab, Vector2 randomPoint)
    {
        if (GameOptions.Particles)
        {
            Instantiate(particleSysPrefab, randomPoint, particleSysPrefab.transform.rotation);
            float particleSystemLifetime = particleSysPrefab.GetComponent<ParticleSystem>().startLifetime;

            yield return new WaitForSeconds(particleSystemLifetime);
        }
        
        GameObject instantiatedObj = Instantiate(prefab, randomPoint, prefab.transform.rotation);
        instantiatedObj.SetActive(true);
    }


}
