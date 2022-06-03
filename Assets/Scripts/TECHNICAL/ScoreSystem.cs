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
            SpawnManager.main.OnScoreChanged();
            LevelSystem.main.OnScoreChanged();
        }
    }

    [HideInInspector] public static ScoreSystem main;


    private void Start()
    {
        main = this;
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
