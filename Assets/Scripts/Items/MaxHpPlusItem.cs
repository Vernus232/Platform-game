using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpPlusItem : Item
{
    [SerializeField] private float maxHealthAddup;


    protected override bool TryDoActionOnPlayer()
    {
        Player.main.maxHp += maxHealthAddup;
        PlayerView.main.UpdateUI();
        return true;
    }
}

