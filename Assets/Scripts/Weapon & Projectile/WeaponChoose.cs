using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoose : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    public bool[] weaponUnlocks;
    [SerializeField] private int fastButtonWeaponIdx = 0;

    private ListView list;
    
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

            Weapon weapon = weapons[activeWeaponIndex];
            currentWeapon = weapon;
            weaponView.OnWeaponChanged();
        }
    }

    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public static WeaponChoose main;

    private int weaponIdx_beforeFastButton;
    private WeaponView weaponView;



    private void Start()
    {
        main = this;
        weaponView = FindObjectOfType<WeaponView>();
        list = FindObjectOfType<ListView>();

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
        if (Input.GetKeyDown(KeyCode.Alpha7))
            TryChooseWeapon(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            TryChooseWeapon(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            TryChooseWeapon(9);

        if (Input.mouseScrollDelta.y > 0)
            ChoosePrevUnlockedWeapon();
        if (Input.mouseScrollDelta.y < 0)
            ChooseNextUnlockedWeapon();
    }

    private void ChooseNextUnlockedWeapon()
    {
        // �� �������� �� ����������
        for (int index = ActiveWeaponIndex + 1; index < weaponUnlocks.Length; index++)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }

        //�� ������� �� ��������
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
        // �� �������� �� �������
        for (int index = ActiveWeaponIndex - 1; index >= 0; index--)
        {
            if (weaponUnlocks[index])
            {
                ChooseWeapon(index);

                return;
            }
        }

        //�� ������� �� ��������
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
            weapons[ActiveWeaponIndex].gameObject.SetActive(false);
            weapons[index].gameObject.SetActive(true);
            ActiveWeaponIndex = index;

            list.OnWeaponChanged(index);
        }
    }

    public void UnlockWeaponByName(string name)
    {
        foreach (Weapon weapon in weapons)
        {
            if (name == weapon.weaponName)
            {
                UnlockWeapon(weapon);
            }
        }
    }

    private void UnlockWeapon(Weapon weapon)
    {
        int weaponIndex = System.Array.IndexOf(weapons, weapon);

        weaponUnlocks[weaponIndex] = true;

        list.OnWeaponUnlocked();
    }


}
