using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Text weaponNameText;
    [SerializeField] private Text currentAmmo;
    [SerializeField] private Text maxAmmo;
    [SerializeField] private Slider slider;

    [HideInInspector] public static WeaponView main;

    [SerializeField] private Weapon currentWeapon;



    private void Start()
    {
        main = this;
    }

    public void OnWeaponChanged()
    {
        StopAllCoroutines();

        currentWeapon = WeaponChoose.main.currentWeapon;

        UpdateUI();
    }

    public void OnAmmoChanged()
    {
        UpdateUI();
    }


    public void UpdateUI()
    {
        weaponNameText.text = "Current weapon:     " + currentWeapon.weaponName;


        if (currentWeapon.type == WeaponType.Burst)
        {
            BurstRangedWeapon burstWeapon = currentWeapon.GetComponent<BurstRangedWeapon>();

            currentAmmo.text = burstWeapon.Ammo.ToString("00");
            maxAmmo.text = burstWeapon.maxAmmo.ToString("00");

            slider.value = (float) burstWeapon.Ammo / (float) burstWeapon.maxAmmo * 100f;
        }


        if (currentWeapon.type == WeaponType.Melee)
        {
            currentAmmo.text = "--";
            maxAmmo.text = "--";

            slider.value = 0;
        }
    }


    public void OnWeaponReloadStarted(float reloadTime)
    {
        StopCoroutine("ReloadProcess");
        StartCoroutine(ReloadProcess(reloadTime));        
    }

    private IEnumerator ReloadProcess(float reloadTime)
    {
        float timePassed = 0;

        while (timePassed < reloadTime)
        {
            yield return new WaitForEndOfFrame();

            slider.value = timePassed / reloadTime * 100;
            timePassed += Time.deltaTime;
        }
    }





}
