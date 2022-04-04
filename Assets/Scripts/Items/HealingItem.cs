using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] private float healthAddup;
    private PlayerView hpView;

    private void Start()
    {
        hpView = FindObjectOfType<PlayerView>();
    }

    protected override void DoActionOnPlayer()
    {
        Player.main.Hp += healthAddup;
    }
}
