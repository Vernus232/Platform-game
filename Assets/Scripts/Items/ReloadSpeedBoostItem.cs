using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSpeedBoostItem : Item
{
    [SerializeField] private float reloadSpdMultAddup;

    

    protected override bool TryDoActionOnPlayer()
    {
        Player.main.ReloadSpeedModifier += reloadSpdMultAddup;
        return true;
    }
}
