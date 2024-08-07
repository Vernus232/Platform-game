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
        {
            StartCoroutine(InvokeWave(spawnWave, spawnWave.offset * 60));
        }
    }

    private IEnumerator InvokeWave(SpawnWave wave, float time)
    {
        yield return new WaitForSeconds(time);
        print(wave + "Has started!");
        SpawnInstantiator.main.InstantiateWave(wave);
    }



}
