using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private float scoreIncreaseOverTime;

    [HideInInspector] public static ScoreSystem main;

    private float score;
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
        }
    }


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
