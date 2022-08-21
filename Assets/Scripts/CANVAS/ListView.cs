using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour
{
    [SerializeField] private int requestedWeaponIndex;
    [SerializeField] private GameObject uiUnlockGameObject;
    [SerializeField] private GameObject uiActiveGameObject;

    private WeaponChoose weaponChoose;

    private void Start()
    {
        weaponChoose.weaponUnlocks =
    }


    public void OnWeaponChanged(int activeIndex)
    {
        if (requestedWeaponIndex == activeIndex)
        {
            uiActiveGameObject.SetActive(true);
        }
        else
        {
            uiActiveGameObject.SetActive(false);
        }
    }
    public void OnWeaponUnlocked(int index)
    {
        if (requestedWeaponIndex == index)
        {
            uiUnlockGameObject.SetActive(true);
        }
        
    }
}
