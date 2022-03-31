using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] private Text weaponNameText;
    [SerializeField] private Text currentAmmo;
    [SerializeField] private Text maxAmmo;

    [HideInInspector] public static WeaponIndicator mainInstance;

    private Weapon currentWeapon;


    private void Start()
    {
        mainInstance = this;
    }

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
        weaponNameText.text = "Current weapon:     " + currentWeapon.weaponName;

        if (currentWeapon.type == WeaponType.Burst)
        {
            BurstWeapon burstWeapon = currentWeapon.GetComponent<BurstWeapon>();

            currentAmmo.text = burstWeapon.ammo.ToString("00");
            maxAmmo.text = burstWeapon.maxAmmo.ToString("00");
        }
        if (currentWeapon.type == WeaponType.Melee)
        {
            currentAmmo.text = "--";
            maxAmmo.text = "--";
        }

    }


}
