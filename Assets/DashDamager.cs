using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            FragileEntity entity = other.gameObject.GetComponent<FragileEntity>();
            if (entity)
                entity.RecieveDamage(entity.maxHp / 2);
        }
    }
}
