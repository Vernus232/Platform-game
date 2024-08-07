using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Get mob names
        MobEnum[] mobEnums = wave.mobMix.names;

        // Compute default spawnRates
        float[] defaultSpawnRates = new float[mobEnums.Length];
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
            float mobProbability = wave.mobMix.odds[i] / SumIntArray(wave.mobMix.odds);
            float mobCount = (int)(mobProbability * wave.scale);
            float spawnRate = mobCount / wave.duration;
            defaultSpawnRates[i] = spawnRate;
        }

        // Spawn all the mob enums in separate coroutines
        for (int i = 0; i < mobEnums.Length; i++)
        {
            float modifiedSpawnRate = defaultSpawnRates[i];
            StartCoroutine(Spawning(mobEnums[i], modifiedSpawnRate, wave.duration));   
        }
    }


    private IEnumerator Spawning(MobEnum mobEnum, float mobs_perMinute, float durationInMinutes)
    {
        float SPAWN_STEP = 1f;
        float mobParts_perSpawnStep = mobs_perMinute / 60 * SPAWN_STEP;
        
        float mobParts = 0;
        for (float t = 0; t < durationInMinutes * 60; t += SPAWN_STEP)
        {
            // If we can spawn one whole mob
            if (mobParts >= 1)
            {
                int wholeMobs = (int) mobParts;
                mobParts -= wholeMobs;
                SpawnMobs(mobEnum, wholeMobs);
            }

            // Adding mob part
            mobParts += mobParts_perSpawnStep * difficultyMultiplier;
            yield return new WaitForSeconds(SPAWN_STEP);
        } 
    }
    private void SpawnMobs(MobEnum mobEnum, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnMob(mobEnum);
        }
    }
    private void SpawnMob(MobEnum mobEnum)
    {
        SpawnField RandomizeSpawnField()
        {
            int index = Random.Range(0, (spawnFields.Length - 1));
            return spawnFields[index];
        }
        SpawnField spawnField = RandomizeSpawnField();

        Vector2 spawnPoint = spawnField.RequestViableSpawnPoint();
        GameObject mobPrefab = MobPrefabManager.main.GetPrefab(mobEnum);

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
