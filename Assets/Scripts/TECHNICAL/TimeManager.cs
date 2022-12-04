using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float winTimescaleSlowdownTime;
    public float currentGameTimeInSeconds = 0;
    public float currentGameTimeInMinutes = 0;

    public static TimeManager main;


    private void Start()
    {
        main = this;

        StartCoroutine(GameTime());
    }

    public void OnWin()
    {
        StartCoroutine(SmoothTimeChange());
    }

    public IEnumerator SmoothTimeChange()
    {
        float slowdownStep = 1f / 10f / winTimescaleSlowdownTime;

        while (Time.timeScale - slowdownStep > 0.01f)
        {
            Time.timeScale -= slowdownStep;
            yield return new WaitForSecondsRealtime(0.1f);
        }

    }

    public void ResetTime()
    {
        Time.timeScale = 1;
    }

    public IEnumerator GameTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentGameTimeInSeconds += 1;
            currentGameTimeInMinutes += 1/60f;
        }
    }

}
