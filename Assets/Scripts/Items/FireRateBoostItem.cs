using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoostItem : Item
{
    [SerializeField] private float fireRateMultAddup;

    

    protected override bool TryDoActionOnPlayer()
    {
        Player.main.FireRateModifier += fireRateMultAddup;
        return true;
    }
}
