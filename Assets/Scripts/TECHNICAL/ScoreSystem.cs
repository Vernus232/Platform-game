using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private float scoreIncreaseOverTime;

    private float score = 0;
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreView.main.UpdateUIScore(score);
            LevelSystem.main.OnScoreChanged();
            foreach (SpawnManager spawnManager in spawnManagers)
            {
                spawnManager.OnScoreChanged();
            }
        }
    }

    [HideInInspector] public static ScoreSystem main;
    private SpawnManager[] spawnManagers;


    private void Start()
    {
        main = this;

        spawnManagers = FindObjectsOfType<SpawnManager>();
    }


    private void FixedUpdate()
    {
        Score += scoreIncreaseOverTime;
    }

    public void AddScoreForKill(float addScore)
    {
        Score += addScore;
    }

}
