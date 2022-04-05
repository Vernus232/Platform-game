using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgboostItem : Item
{
    [SerializeField] private float damageAddup;


    protected override bool TryDoActionOnPlayer()
    {
        Player.main.DamageModifier += damageAddup;
        return true;
    }
}
