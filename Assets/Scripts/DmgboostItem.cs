using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgboostItem : Item
{
    [SerializeField] private float damageAddup;


    protected override void DoActionOnPlayer()
    {
        Player.main.damageModifier += damageAddup;
    }
}
