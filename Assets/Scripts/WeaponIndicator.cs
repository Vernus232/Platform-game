using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour
{
    private Weapon currentWeapon;
    //public PlayerProjectile projectile;

    public Text weaponNameText;
    //public Text weaponDamage;



    public void OnWeaponChanged(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        //UpdateProjectile();

        UpdateUI();
    }


    private void UpdateUI()
    {
        weaponNameText.text = "Current weapon:     " + currentWeapon.name;

        //weaponDamage.text = "Damage : " + projectile.damageForIndicator;

        //Debug.Log(projectile.damageForIndicator);
    }

    //public void UpdateProjectile()

}
