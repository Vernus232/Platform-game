using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    public float damage;
    public float betweenHitsTime;
    [SerializeField] private List<string> vulnerableTags;

    private float prevHitTime = 0;



    // ��� ��������� � collision...
    private void OnTriggerStay2D(Collider2D other)
    {
        //..������ �� �����...
        if (vulnerableTags.Contains(other.tag))
        {
            //...������ ����� ��������� ��� �� ��� ����, � ���� ��� ���� ������ betweenHitsTime - ���� ����� * ����
            float currTime = Time.time;
            if ((currTime - prevHitTime) > betweenHitsTime)
            {
                // ��������� ��� �� ���� (����������) � �������� ����
                FragileEntity entity = other.gameObject.GetComponent<FragileEntity>();
                entity.RecieveDamage(damage);

                prevHitTime = currTime;
            }
        }
    }


}
