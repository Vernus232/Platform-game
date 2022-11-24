using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuSliderValue : MonoBehaviour
{
    private Slider slider;
    private Text value;

    // Start is called before the first frame update
    private void Start()
    {
        value = GetComponent<Text>();
        slider = GetComponentInParent<Slider>();
    }

    private void Update()
    {
        value.text = slider.value.ToString("00");
    }
}
