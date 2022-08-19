using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStepSpawner : StepSpawner
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float[] odds;
    [SerializeField] private float[] randomValues;


    private void Awake()
    {
        randomValues = new float[prefabs.Length];
        randomValues[0] = odds[0];
        for (int i = 1; i < prefabs.Length; i++)
        {
            randomValues[i] = randomValues[i - 1] + odds[i];
        }
        
        //StartCoroutine(RandomizeSpawn());
    }

    protected override void OnEnable()
    {
        StartCoroutine(RandomizeSpawn());
        base.OnEnable();
    }

    private IEnumerator RandomizeSpawn()
    {
        while (true)
        {
            float x = Random.Range(0, randomValues[randomValues.Length - 1]);
            int GetNearestElementIndexToX(float[] array, float x)
            {
                for (int i = 0; i < prefabs.Length; i++)
                {
                    if (x - randomValues[i] <= 0)
                        return i;
                }

                Debug.LogError("Random Spawner, not found nearest element");
                return 0;
            }
            int newIndex = GetNearestElementIndexToX(randomValues, x);
            prefab = prefabs[newIndex];

            yield return new WaitForSeconds(0.5f);
        }
    }


}
