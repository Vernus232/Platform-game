using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    public int[] scores;

    private void Start()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            texts[i].text = i.ToString() + ". " + scores[i].ToString();
        }
    }

    public void LoadScore()
    {
        int[] newScores = SaverV2.LoadScores();
    }

}
