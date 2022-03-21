using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{

    // ����������� ��������� �� ���-����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������� ������� � �������� ���� Player
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision( collision.collider, gameObject.GetComponent<Collider2D>() );
        }

        // ������������ ������ �� � �������
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
