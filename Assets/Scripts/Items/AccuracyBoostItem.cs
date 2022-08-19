using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyBoostItem : Item
{
    [SerializeField] private float accuracyAddup;

    

    protected override bool TryDoActionOnPlayer()
    {
        Player.main.AccuracyModifier += accuracyAddup;
        return true;
    }
}
