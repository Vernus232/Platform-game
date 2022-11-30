using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Text[] texts;
    public int[] scores;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int len = scores.Length > texts.Length ? texts.Length : scores.Length;
        for (int i = 0; i < len; i++)
        {
            texts[i].text = (i+1).ToString() + ". " + scores[i].ToString();
        }
    }

    public void LoadScore()
    {
        scores = Saver.LoadScores();
        Array.Sort(scores);
        Array.Reverse(scores);
        UpdateUI();
    }

}
