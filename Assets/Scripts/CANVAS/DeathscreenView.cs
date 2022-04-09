using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathscreenView : MonoBehaviour
{
    public static DeathscreenView main;

    [SerializeField] private Text text;


    private void Start()
    {
        main = this;
    }

    private void OnEnable()
    {
        text.text = "Your Score: " + ScoreSystem.main.Score.ToString("000000");
    }


}
