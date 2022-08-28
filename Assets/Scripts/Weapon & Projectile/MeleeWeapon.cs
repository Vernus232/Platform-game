using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float meleeDamage;
    public float startBetweenHitsTime;
    [SerializeField] private GameObject damagingObject;
    [SerializeField] private AudioSource SwingAudio;

    private float currentBetweenHitsTime;
    private float currTime = 999;
    private float prevHitTime = 0;


    private void Start()
    {
        currentBetweenHitsTime = startBetweenHitsTime;

        damagingObject.GetComponent<DamagingField>().damage = meleeDamage;
        damagingObject.GetComponent<DamagingField>().betweenHitsTime = currentBetweenHitsTime;
    }

    public void OnReloadSpeedChanged(float newReloadSpeedModifier)
    {
        currentBetweenHitsTime = startBetweenHitsTime / newReloadSpeedModifier;
        damagingObject.GetComponent<DamagingField>().betweenHitsTime = currentBetweenHitsTime;
    }

    private void Update()
    {
        currTime = Time.time;
        if (currTime - prevHitTime > currentBetweenHitsTime &&  Input.GetMouseButton(0))
        {
            StartCoroutine(Hit());
            WeaponView.main.OnWeaponReloadStarted(currentBetweenHitsTime);

            prevHitTime = currTime;
        }
    }

    private IEnumerator Hit()
    {
        damagingObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        SwingAudio.Play();

        damagingObject.SetActive(false);
    }



}
