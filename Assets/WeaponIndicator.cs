using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    private Weapon currentWeapon;

    public Text text;



    public void OnWeaponChanged(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        
    }
}
