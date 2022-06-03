using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCaster : MonoBehaviour
{
    public GameObject pauseScreen;
    private RukiPivot rukiPivo;
    private WeaponChoose weaponChoose;

    private void Start()
    {
        rukiPivo = FindObjectOfType<RukiPivot>();
        weaponChoose = FindObjectOfType<WeaponChoose>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            rukiPivo.enabled = false;
            weaponChoose.currentWeapon.enabled = false;
            weaponChoose.enabled = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        rukiPivo.enabled = true;
        weaponChoose.currentWeapon.enabled = true;
        weaponChoose.enabled = true;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
