using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItem : Item
{

    protected override bool TryDoActionOnPlayer()
    {
        WinscreenView.main.gameObject.SetActive(true);
        TimeManager.main.OnWin();
        return true;
    }



}
