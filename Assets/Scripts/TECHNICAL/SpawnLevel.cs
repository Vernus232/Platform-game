using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public List<SpawnWave> spawnWaves;

    private void Start() 
    {
        InvokeWaves();
    }

    private void InvokeWaves()
    {
        foreach (SpawnWave spawnWave in spawnWaves)

            InvokeWave(spawnWave, spawnWave.duration * 60);
    }

    private IEnumerator InvokeWave(SpawnWave wave, float time)
    {
        yield return new WaitForSeconds(time);
        SpawnInstantiator.main.InstantiateWave(wave);
    }



}
