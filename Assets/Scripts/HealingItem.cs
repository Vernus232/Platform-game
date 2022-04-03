using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] private float healthAddup;
    private HpView hpView;

    private void Start()
    {
        hpView = FindObjectOfType<HpView>();
    }

    protected override void DoActionOnPlayer()
    {
        Player.main.hp += healthAddup;

        hpView.SetViewHp(Player.main.hp);
    }
}
