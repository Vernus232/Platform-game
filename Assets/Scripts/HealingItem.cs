using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] private float healthAddup;


    protected override void DoActionOnPlayer()
    {
        Player.main.hp += healthAddup;
    }
}
