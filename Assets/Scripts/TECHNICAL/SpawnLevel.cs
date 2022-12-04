using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public List<SpawnWave> spawnWaves;
    private SpawnInstantiator spawnInstantiator;

    private void Start() 
    {
        InvokeWaves();
    }

    private void InvokeWaves()
    {
        foreach (SpawnWave spawnWave in spawnWaves)
            Invoke("spawnInstantiator.StartWave(spawnWave)", spawnWave.duration * 60);
    }
}
