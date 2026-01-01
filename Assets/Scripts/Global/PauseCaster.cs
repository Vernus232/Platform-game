using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCaster : MonoBehaviour
{
    [SerializeField] private GameObject crosshairGameObject;
    [SerializeField] private GameObject pauseScreen;
    [HideInInspector] public static PauseCaster main;
    private RukiPivot rukiPivo;
    private bool paused;
    private WeaponChoose weaponChoose;

    private void Start()
    {
        main = this;
        rukiPivo = FindObjectOfType<RukiPivot>();
        weaponChoose = FindObjectOfType<WeaponChoose>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        rukiPivo.enabled = false;
        weaponChoose.currentWeapon.enabled = false;
        weaponChoose.enabled = false;
        crosshairGameObject.SetActive(false);
        paused = true;
        Cursor.visible = true;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        rukiPivo.enabled = true;
        weaponChoose.currentWeapon.enabled = true;
        weaponChoose.enabled = true;
        paused = false;
        Cursor.visible = false;
        crosshairGameObject.SetActive(true);
    }
    public void Exit()
    {
        
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void FreezeGame(){
        Time.timeScale = 0;
    }
    public void OnPlayerDeath(){
        Invoke("FreezeGame", 1f);
    }


    
}
