using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoIndicator : MonoBehaviour
{
    public Weapon ammo;
    public Text currentAmmo;
    public Text maxAmmo;


    public void OnWeaponChanged(Weapon newAmmo)
    {
        ammo = newAmmo;

        UpdateUI();
    }

    void Update()
    {
        currentAmmo.text = ammo.ammo.ToString("00");
        maxAmmo.text = ammo.maxAmmo.ToString("00");
    }

    private void UpdateUI()
    {

    }
    
}
