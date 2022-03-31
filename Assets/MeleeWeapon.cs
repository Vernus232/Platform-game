using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float meleeDamage;
    public float betweenHitsTime;
    [SerializeField] private GameObject damagingObject;

    private float prevHitTime = 0;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float currTime = Time.time;
            if ((currTime - prevHitTime) > betweenHitsTime)
            {
                StartCoroutine(Hit());
                prevHitTime = currTime;
            }
        }
    }

    private IEnumerator Hit()
    {
        damagingObject.SetActive(true);

        yield return new WaitForEndOfFrame();

        damagingObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
        
            if (entity)
                entity.RecieveDamage(meleeDamage);
        }
    }




}
