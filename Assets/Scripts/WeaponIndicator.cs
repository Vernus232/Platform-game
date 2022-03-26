using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    private Weapon currentWeapon;

    public Text weaponNameText;



    public void OnWeaponChanged(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        UpdateUI();
    }

    private void UpdateUI()
    {
        weaponNameText.text = "Current weapon - " + currentWeapon.name;
    }

}
