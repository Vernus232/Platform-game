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
        int GetRandomWinIndex()
        {
            int x = Random.Range(1, 100 +1);

            if (x <= 33)
                return 0;
            else if (x <= 66)
                return 1;
            else if (x <= 99)
                return 2;
            else if (x == 100)
                return 3;

            Debug.LogError("strange index");
            return 999;
        }
        
        winText.text = winStrings[GetRandomWinIndex()];
        scoreText.text = ScoreSystem.main.Score.ToString("000000");
    }

}
