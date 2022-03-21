using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonProjectile : MonoBehaviour
{
    public float damage;
    public float lifespan;



    // ������ ������ ������ ���� (��� ������ ����)
    private void Awake()
    {
        Destroy(gameObject, lifespan);
    }


    // ������� ���� � �����
    private void FixedUpdate()
    {
        Vector2 flightDir = GetComponent<Rigidbody2D>().velocity;
        float angle = Vector2.Angle(flightDir.normalized, Vector2.left);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    // ����������� ��������� �� ���-����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������� ������� � �������� ���� Player
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision( collision.collider, gameObject.GetComponent<Collider2D>() );
        }

        //������������ ������ �� � �������
        else
        {
            // �������� �������� ���� Fraglie Entity
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            if (entity)
            {
                // �������� ���� entity
                entity.RecieveDamage(damage);
            }


            // ���������� ����
            Destroy(gameObject);
        }


        
    }



}
