using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float winTimescaleSlowdownTime;

    public static TimeManager main;


    private void Start()
    {
        main = this;
    }

    public void OnWin()
    {
        StartCoroutine(SmoothTimeChange());
    }

    public IEnumerator SmoothTimeChange()
    {
        float slowdownStep = 1f / 10f / winTimescaleSlowdownTime;

        while (Time.timeScale > 0.01f)
        {
            Time.timeScale -= slowdownStep;
            Debug.Log(Time.timeScale);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }



}
