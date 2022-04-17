using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Slider levelSlider;
    [SerializeField] private Text levelText;

    [HideInInspector] public static LevelView main;

    private void Start()
    {
        main = this;
    }


    public void UpdateUI(float levelSliderValue)
    {
        levelSlider.value = levelSliderValue;
        levelText.text = "Level: " + LevelSystem.main.currentLevel.ToString();
    }
}
