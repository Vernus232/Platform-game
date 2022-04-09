using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpView : MonoBehaviour
{
    [SerializeField] private Slider xpSlider;
    [SerializeField] private Text xpText;


    public void UpdateUI()
    {
        xpSlider.value = ScoreSystem.main.Score;
        
    }
}
