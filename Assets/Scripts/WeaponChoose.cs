using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoose : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponObjects;
    [SerializeField] private bool[] weaponUnlocks;
    [SerializeField] private int fastButtonWeaponIdx = 0;
    

    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public static WeaponChoose main;


    private int weaponIdx_beforeFastButton;
    private WeaponView weaponView;

    private int activeWeaponIndex = 1;
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
            currentWeapon = weapon;
            weaponView.OnWeaponChanged();
        }
    }



    private void Start()
    {
        main = this;
        weaponView = FindObjectOfType<WeaponView>();

        int STARTING_WEAPON_IDX = 1;
        TryChooseWeapon(STARTING_WEAPON_IDX);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            weaponIdx_beforeFastButton = ActiveWeaponIndex;
            TryChooseWeapon(fastButtonWeaponIdx);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
            TryChooseWeapon(weaponIdx_beforeFastButton);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            TryChooseWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            TryChooseWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            TryChooseWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            TryChooseWeapon(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            TryChooseWeapon(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            TryChooseWeapon(6);

        if (Input.mouseScrollDelta.y > 0)
            ChooseNextUnlockedWeapon();

        if (Input.mouseScrollDelta.y < 0)
            ChoosePrevUnlockedWeapon();
    }

    private void ChooseNextUnlockedWeapon()
    {
        // От текущего до последнего
        for (int index = ActiveWeaponIndex + 1; index < weaponUnlocks.Length; index++)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }

        //От первого до текущего
        for (int index = 0; index <= ActiveWeaponIndex; index++)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }
    }

    private void ChoosePrevUnlockedWeapon()
    {
        // От текущего до первого
        for (int index = ActiveWeaponIndex - 1; index >= 0; index--)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }

        //От первого до текущего
        for (int index = weaponUnlocks.Length - 1; index >= ActiveWeaponIndex; index--)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }
    }

    private void TryChooseWeapon(int index)
    {
        if (weaponUnlocks[index])
        {
            ChooseWeapon(index);
        }
    }

    private void ChooseWeapon(int index)
    {
        if (ActiveWeaponIndex != index)
        {
            weaponObjects[ActiveWeaponIndex].SetActive(false);
            weaponObjects[index].SetActive(true);
            ActiveWeaponIndex = index;
        }
    }

}
