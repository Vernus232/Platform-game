using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    private Weapon currentWeapon;

    public Text weaponNameText;
    public Text currentAmmo;
    public Text maxAmmo;



    public void OnWeaponChanged(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        UpdateUI();
    }

    public void OnShot()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        weaponNameText.text = "Current weapon:     " + currentWeapon.name;

        currentAmmo.text = currentWeapon.ammo.ToString("00");
        maxAmmo.text = currentWeapon.maxAmmo.ToString("00");

    }


}
