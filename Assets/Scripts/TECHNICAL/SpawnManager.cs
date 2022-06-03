using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float scoreToSpawnStep_K;
    [SerializeField] private float hiddenInitialSpawn;
    [SerializeField] private List<Spawner> spawners;

    [SerializeField] private float currentSpawnSpeed;
    [SerializeField] private float currentSpawnStep;
    [HideInInspector] public static SpawnManager main;



    private void Start()
    {
        main = this;
    }

    private void OnEnable()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (Spawner spawner in spawners)
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
        currentSpawnSpeed = Function(scoreToSpawnStep_K / 1000, ScoreSystem.main.Score, hiddenInitialSpawn);
        currentSpawnStep = 1 / currentSpawnSpeed;

        foreach (Spawner spawner in spawners)
        {
            spawner.spawnStep = currentSpawnStep;
        }
    }


}
