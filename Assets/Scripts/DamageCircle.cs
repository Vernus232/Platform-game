using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float betweenHitsTime;

    private float prevHitTime = 0;



    // ��� ��������� � collision...
    private void OnTriggerStay2D(Collider2D other)
    {
        //..������...
        if (other.gameObject.tag == "Player")
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
