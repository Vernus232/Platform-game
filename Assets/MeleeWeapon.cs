using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float meleeDamage;
    public float betweenHitsTime;
    [SerializeField] private GameObject damagingObject;

    private float prevHitTime = 0;



    private void Start()
    {
        damagingObject.GetComponent<DamageCircle>().damage = meleeDamage;
        damagingObject.GetComponent<DamageCircle>().betweenHitsTime = betweenHitsTime;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float currTime = Time.time;
            if ((currTime - prevHitTime) > betweenHitsTime)
            {
                StartCoroutine(Hit());

                prevHitTime = currTime;
                WeaponView.main.OnWeaponReloadStarted(betweenHitsTime);
            }
        }
    }

    private IEnumerator Hit()
    {
        damagingObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        damagingObject.SetActive(false);
    }



}
