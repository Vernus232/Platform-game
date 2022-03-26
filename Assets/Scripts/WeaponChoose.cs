using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoose : MonoBehaviour
{
    public WeaponIndicator indicator;
    public GameObject[] weaponObjects;
    public bool[] weaponUnlocks;

    private int activeWeaponIndex = 0;
    public int ActiveWeaponIndex
    {
        get
        {
            return activeWeaponIndex;
        }
        set
        {
            Weapon weapon = weaponObjects[activeWeaponIndex].GetComponent<Weapon>();
            activeWeaponIndex = value;
            indicator.OnWeaponChanged(weapon);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TryChooseWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            TryChooseWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            TryChooseWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            TryChooseWeapon(3);
    }

    private void TryChooseWeapon(int index)
    {
        if (weaponUnlocks[index])
            ChooseWeapon(index);
    }

    private void ChooseWeapon(int index)
    {
        weaponObjects[activeWeaponIndex].SetActive(false);
        weaponObjects[index].SetActive(true);
        activeWeaponIndex = index;
    }

}
