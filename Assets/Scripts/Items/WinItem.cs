using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItem : Item
{

    protected override bool TryDoActionOnPlayer()
    {
        FindObjectOfType<WinscreenView>(true).gameObject.SetActive(true);
        TimeManager.main.OnWin();
        return true;
    }



}
