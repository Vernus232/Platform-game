using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text text;

    [HideInInspector] public static ScoreView main;



    private void Start()
    {
        main = this;
    }

    public void UpdateUIScore(float score)
    {
        text.text = score.ToString("0000000");
    }
}
