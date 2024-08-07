using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private float scoreToSpawnSpeed;
    [SerializeField] private float hiddenInitialSpawn;
    [SerializeField] private List<StepSpawner> spawners;

    [SerializeField] private float currentSpawnSpeed;
    [SerializeField] private float currentSpawnStep;


    private void OnEnable()
    {
        foreach (StepSpawner spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (StepSpawner spawner in spawners)
        {
            if (spawner)
                spawner.gameObject.SetActive(false);
        }
    }

    public void OnScoreChanged()
    {
        ChangeSpawnStep();
    }

    private void ChangeSpawnStep()
    {
        float Function(float k, float x, float b)
        {
            return k * x + b;
        }
        currentSpawnSpeed = Function(scoreToSpawnSpeed / 100000, ScoreSystem.main.Score, hiddenInitialSpawn);
        currentSpawnStep = 1 / currentSpawnSpeed;

        foreach (StepSpawner spawner in spawners)
        {
            spawner.spawnStep = currentSpawnStep;
        }
    }


}
