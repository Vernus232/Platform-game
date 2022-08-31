using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float maxDamage;

    private float radius;

    private void Awake()
    {
        radius = GetComponent<CircleCollider2D>().radius;

        ApplyDamageInRadius(radius);

        Destroy(this);
        Destroy(gameObject, 3);
    }

    private void ApplyDamageInRadius(float radius)
    {
        Collider2D[] allOverlappingColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D overlappingCollider in allOverlappingColliders)
        {
            FragileEntity fragileEntity = overlappingCollider.GetComponent<FragileEntity>();
            if (fragileEntity)
            {
                float damage = GetDamageForEntity(fragileEntity);
                fragileEntity.RecieveDamage(damage);
            }
        }
    }

    private float GetDamageForEntity(FragileEntity fragileEntity)
    {
        float distance = (transform.position - fragileEntity.transform.position).magnitude;

        float GetLinearDistanceModifier(float distance)
        {
            float mul = distance / radius;

            return mul;
        }
        //float GetSquarerDistanceModifier(float distance)
        //{
        //    float mul = distance / radius;

        //    return mul*mul;
        //}

        float actualDamage = GetLinearDistanceModifier(distance) * maxDamage;
        return actualDamage;
    }


}
