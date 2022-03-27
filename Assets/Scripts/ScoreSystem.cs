using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Enemy enemy;
    public float score;
    public float scoreIncreaseOverTime;
    public float scoreForKill;

    public Text text;


    private void FixedUpdate()
    {
        score += scoreIncreaseOverTime;

        text.text = score.ToString("0000000");
    }

    public void AddScoreForKill()
    {
        score += scoreForKill;
    }
}
