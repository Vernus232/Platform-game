using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Text weaponNameText;

    [Header("Ammunition")]
    [SerializeField] private Text currentAmmo;
    [SerializeField] private Text maxAmmo;
    [SerializeField] private Slider ammoSlider;
    [SerializeField] private Text ammoOnCursor;
    [SerializeField] private Slider cursorAmmoSlider;


    [Space(10)]
    [SerializeField] private Slider flySlider;

    [Header("Refs")]
    [HideInInspector] public static WeaponView main;
    [SerializeField] private FlyAbility flyAbility;

    private Weapon currentWeapon;



    private void Start()
    {
        main = this;
        flySlider.maxValue = flyAbility.maxCharge;
    }

    public void OnWeaponChanged()
    {
        currentWeapon = WeaponChoose.main.currentWeapon;

        cursorAmmoSlider.value = 0;

        BurstRangedWeapon burstRangedWeapon = currentWeapon.GetComponent<BurstRangedWeapon>();
        if (burstRangedWeapon && burstRangedWeapon.Ammo > 0)
            StopAllCoroutines();
        if (burstRangedWeapon == null)
            StopAllCoroutines();

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

            currentAmmo.text = burstWeapon.Ammo.ToString("0");
            maxAmmo.text = burstWeapon.maxAmmo.ToString("0");
            ammoOnCursor.text = burstWeapon.Ammo.ToString("0");

            ammoSlider.value = (float) burstWeapon.Ammo / (float) burstWeapon.maxAmmo * 100f;
        }


        if (currentWeapon.type == WeaponType.Melee)
        {
            currentAmmo.text = "--";
            maxAmmo.text = "--";
            ammoOnCursor.text = "--";

            ammoSlider.value = 100;
        }
    }

    
    public void OnWeaponReloadStarted(float reloadTime)
    {
        StartCoroutine(ReloadProcess(reloadTime));
    }


    private IEnumerator ReloadProcess(float reloadTime)
    {
        float timePassed = 0;

        while (timePassed < reloadTime)
        {
            yield return new WaitForEndOfFrame();

            ammoSlider.value = timePassed / reloadTime * 100;
            cursorAmmoSlider.value = timePassed / reloadTime * 100;
            timePassed += Time.deltaTime;
        }

        cursorAmmoSlider.value = 0;
    }

    public void UpdateFlyUi()
    {
        flySlider.value = flyAbility.charge;
    }

}
