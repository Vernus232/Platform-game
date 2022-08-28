using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public float damageModifier = 1;

    private FragileEntity fragileEntity;


    private void Start()
    {
        fragileEntity = GetComponentInParent<FragileEntity>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            CommonProjectile projectile = collision.gameObject.GetComponent<CommonProjectile>();
            fragileEntity.RecieveDamage(projectile.damage * damageModifier);
        }
    }
}
