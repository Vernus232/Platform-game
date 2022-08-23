using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingField : MonoBehaviour
{
    public float damage;
    public float betweenHitsTime;
    [SerializeField] private List<string> vulnerableTags;

    private float currTime = 999;
    private float prevHitTime = 0;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (vulnerableTags.Contains(other.tag))
        {
            currTime = Time.time;
            if (currTime - prevHitTime > betweenHitsTime || currTime - prevHitTime < 0.01f)
            {
                FragileEntity entity = other.gameObject.GetComponent<FragileEntity>();
                entity.RecieveDamage(damage);
                prevHitTime = currTime;
            }
        }
    }


}
