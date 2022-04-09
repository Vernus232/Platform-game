using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinscreenView : MonoBehaviour
{
    [SerializeField] private Text winText;
    [SerializeField] private Text scoreText;
    [SerializeField] private string[] winStrings;


    private void OnEnable()
    {
        int winStringIndex = 0;
        int x = Random.Range(1, 100);

        if (x <= 33)
            winStringIndex = 1;
        else if (x <= 66)
            winStringIndex = 2;
        else if (x <= 99)
            winStringIndex = 3;
        else if (x == 100)
            winStringIndex = 4;

        winText.text = winStrings[winStringIndex];
        scoreText.text = ScoreSystem.main.Score.ToString("000000");
    }

}
