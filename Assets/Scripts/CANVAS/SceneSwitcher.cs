using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(int i)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
