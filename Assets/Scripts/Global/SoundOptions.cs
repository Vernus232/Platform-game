using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }
    public void SoundChange(float volume)
    {
        AudioListener.volume = volume;
    }
}
