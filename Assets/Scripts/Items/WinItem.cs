using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItem : Item
{
    private GameObject winScreen;

    
    private void Start()
    {
        winScreen = FindObjectOfType<WinscreenView>(true).gameObject;
    }

    protected override bool TryDoActionOnPlayer()
    {
        winScreen.SetActive(true);
        TimeManager.main.OnWin();
        return true;
    }




}
