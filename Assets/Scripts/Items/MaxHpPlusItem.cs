using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpPlusItem : Item
{
    [SerializeField] private float maxHealthAddup;
    private PlayerView hpView;

    private void Start()
    {
        hpView = FindObjectOfType<PlayerView>();
    }

    protected override bool TryDoActionOnPlayer()
    {
        Player.main.maxHp += maxHealthAddup;
        hpView.UpdateUI();
        return true;
    }
}

