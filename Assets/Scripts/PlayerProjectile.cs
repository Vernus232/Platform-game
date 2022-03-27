using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{
    [HideInInspector] public string damageForIndicator;
    public GameObject hitPrefab;
    public bool destroysAnything = false;
    //public int penetration = 0;



    // ����������� ��������� �� ���-����

    private void Start()
    {
        damageForIndicator = damage.ToString("000");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        // �� �����
        if (collision.gameObject.CompareTag("Enemy"))
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
