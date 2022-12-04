using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInstantiator : MonoBehaviour
{
    public float dificultyMultiplier = 1;

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
        MobEnum[] mobEnums = wave.mobMix.mobNames;

        // Compute default spawnRates
        float[] defaultSpawnRates = new float[mobEnums.Length];
        for (int i = 0; i < defaultSpawnRates.Length; i++)
        {
            float mobCount = (int)(wave.mobMix.mobOdds[i] * wave.scale);
            float spawnRate = mobCount / wave.duration;
            defaultSpawnRates[i] = spawnRate;
        }

        // Spawn all the mob enums in separate coroutines
        for (int i = 0; i < mobEnums.Length; i++)
        {
            float modifiedSpawnRate = defaultSpawnRates[i] * dificultyMultiplier;
            StartCoroutine(Spawning(mobEnums[i], modifiedSpawnRate, wave.duration));   
        }
    }


    private IEnumerator Spawning(MobEnum mobEnum, float mobsPerMinute, float duration)
    {
        float SPAWN_STEP = 0.2f;

        float mobsPerSpawnStep_float = mobsPerMinute / 60 * SPAWN_STEP;
        int mobsPerSpawnStep = Mathf.RoundToInt(mobsPerSpawnStep_float);

        for (float t = 0; t < duration; t += SPAWN_STEP)
        {
            SpawnMobs(mobEnum, mobsPerSpawnStep);
            yield return new WaitForSeconds(SPAWN_STEP);
        }
    }
    private void SpawnMobs(MobEnum mobEnum, int count)
    {
        for (int i = 0; i <= count; i++)
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
        GameObject mobPrefab = MobPrefabManager.main.GetMobPrefab(mobEnum);

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
