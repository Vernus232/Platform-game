using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("������ ������� ������� ��� ���������")]
    [SerializeField] private GameObject hitPrefab;
        [Tooltip("���������� �� ���� ����� �����")]
    [SerializeField] private bool destroysAnything = false;



    // ����������� ��������� �� ���-����
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        // �� �����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ���� ����
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);
        }

        // � ���������
        if (collision.gameObject.CompareTag("Decoration"))
        {
            // ���� ����
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);
        }

        // � Obstacle
        if (collision.gameObject.layer == 3)
        {
            // ���������� Obstacle, ���� �����
            if (destroysAnything)
                Destroy(collision.gameObject);
        }


        #region �������
        // ��������� (������� ��� �����) ������ ������� ���������
        GameObject hitGameObject = Instantiate( hitPrefab, transform.position, transform.rotation );
            
        // ��������� ���������� ParticleSystem
        ParticleSystem hitPartileSystem = hitGameObject.GetComponent<ParticleSystem>();
        hitPartileSystem.Play();

        // ������ �� ������
        Destroy(hitGameObject, 3);
        #endregion


        Destroy(gameObject);
    }



}
