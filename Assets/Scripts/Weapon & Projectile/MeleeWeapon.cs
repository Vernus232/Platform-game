using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float meleeDamage;
    public float betweenHitsTime;
    [SerializeField] private GameObject damagingObject;

    private float currTime = 999;
    private float prevHitTime = 0;


    private void Start()
    {
        damagingObject.GetComponent<DamagingField>().damage = meleeDamage;
        damagingObject.GetComponent<DamagingField>().betweenHitsTime = betweenHitsTime;
    }

    private void Update()
    {
        currTime = Time.time;
        if (currTime - prevHitTime > betweenHitsTime  &&  Input.GetMouseButton(0))
        {
            StartCoroutine(Hit());
            WeaponView.main.OnWeaponReloadStarted(betweenHitsTime);

            prevHitTime = currTime;
        }
    }

    private IEnumerator Hit()
    {
        damagingObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        damagingObject.SetActive(false);
    }



}
