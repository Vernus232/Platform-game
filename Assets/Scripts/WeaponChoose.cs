using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoose : MonoBehaviour
{
    public WeaponIndicator indicator;
    public GameObject[] weaponObjects;
    public bool[] weaponUnlocks;

    private int activeWeaponIndex;
    public int ActiveWeaponIndex
    {
        get
        {
            return activeWeaponIndex;
        }
        set
        {
            activeWeaponIndex = value;

            Weapon weapon = weaponObjects[activeWeaponIndex].GetComponent<Weapon>();
            indicator.OnWeaponChanged(weapon);
        }
    }


    private void Start()
    {
        int DEFAULT_WEAPON_INDEX = 0;
        TryChooseWeapon(DEFAULT_WEAPON_INDEX);
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
        weaponObjects[ActiveWeaponIndex].SetActive(false);
        weaponObjects[index].SetActive(true);
        ActiveWeaponIndex = index;
    }

}
