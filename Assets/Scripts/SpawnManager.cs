using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float scoreToSpawnSpeed;
    [SerializeField] private float hiddenInitialScore;

    [SerializeField] private List<Spawner> spawners;

    [SerializeField] private float currentSpawnStep;

    [HideInInspector] public static SpawnManager main;

    private ScoreSystem scoreSystem;


    private void Start()
    {
        main = this;

        scoreSystem = FindObjectOfType<ScoreSystem>();
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
            spawner.gameObject.SetActive(false);
        }
    }

    public void OnScoreChanged()
    {
        ChangeSpawnStep();
    }

    private void ChangeSpawnStep()
    {
        currentSpawnStep = scoreToSpawnSpeed / (hiddenInitialScore + scoreSystem.Score);

        foreach (Spawner spawner in spawners)
        {
            spawner.spawnStep = currentSpawnStep;
        }
    }


}
