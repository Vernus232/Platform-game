using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private float scoreIncreaseOverTime;
    [SerializeField] private Text text;

    private float score;


    private void FixedUpdate()
    {
        score += scoreIncreaseOverTime;
    }

    public void AddScoreForKill(float addScore)
    {
        score += addScore;

        UpdateUIScore();
    }

    public void UpdateUIScore()
    {
        text.text = score.ToString("0000000");
    }
}
