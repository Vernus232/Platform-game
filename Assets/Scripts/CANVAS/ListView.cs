using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour
{
    [SerializeField] private GameObject[] iconObjects;
    [SerializeField] private GameObject[] highlightObjects;

    private WeaponChoose weaponChoose;


    private void Start()
    {
        weaponChoose = FindObjectOfType<WeaponChoose>();

        UpdateUnlocks();
        HighlightUIElement(iconObjects[1]);
    }


    public void OnWeaponChanged(int activeIndex)
    {
        HighlightUIElement(highlightObjects[activeIndex]);
    }
    public void OnWeaponUnlocked()
    {
        UpdateUnlocks();        
    }

    private void HighlightUIElement(GameObject activeHighlightObject)
    {
        // �������� ��� ��������
        foreach (GameObject highlightObject in highlightObjects)
        {
            if (highlightObject)
                highlightObject.SetActive(false);
        }

        // ������������ ������ �������
        if (activeHighlightObject)
            activeHighlightObject.SetActive(true);
    }

    private void UpdateUnlocks()
    {
        for (int i = 0; i < weaponChoose.weaponUnlocks.Length; i++)
        {
            bool isWeaponUnlocked = weaponChoose.weaponUnlocks[i];
            if (iconObjects[i])
                iconObjects[i].SetActive(isWeaponUnlocked);
        }
    }


}
