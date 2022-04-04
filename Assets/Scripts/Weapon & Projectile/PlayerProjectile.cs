using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("������ ������� ������� ��� ���������")]
    [SerializeField] private GameObject particleCaster_prefab;

    public int penetration = 0;



    // ����������� ��������� �� ���-����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // � Obstacle
        if (collision.gameObject.layer == 3)
        {
            Color collisionColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Spawn_particleCaster_withColor(collisionColor);

            Debug.Log("Died to the wall");
            Destroy(gameObject);

            return;
        }

        // �� ����� ��� ���������
        if (collision.gameObject.CompareTag("Enemy")  ||  collision.gameObject.CompareTag("Decoration"))
        {
            // ���� ����
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);

            // ��������
            if (penetration > 0)
            {
                //SpawnProjectileCopy(collision); 

                Debug.Log("Mom died");
                Destroy(gameObject);
            }

            if (penetration == 0)
            {
                Debug.Log("I died");
                Destroy(gameObject);
            }

            return;
        }
    }

    private void SpawnProjectileCopy(Collision2D collision)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f);
        //Debug.Log(hit.collider.name);

        GameObject newProjectile = Instantiate(gameObject, transform.position - new Vector3(0.01f, 0), transform.rotation);
        Physics2D.IgnoreCollision(newProjectile.GetComponent<Collider2D>(), collision.collider);
        newProjectile.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        newProjectile.GetComponent<PlayerProjectile>().penetration = gameObject.GetComponent<PlayerProjectile>().penetration -= 1;
    }

    private void Spawn_particleCaster_withColor(Color collisionColor)
    {
        // ��������� (������� ��� �����) ������ ������� ���������
        GameObject particleCaster_gameObject = Instantiate(particleCaster_prefab, transform.position, transform.rotation);

        // ��������� �������� ���������� ��������(�.�. ������ ����� ������ �� �����) �������-�������
        ParticleSystem particleCaster_particleSystem = particleCaster_gameObject.GetComponent<ParticleSystem>();
        particleCaster_particleSystem.startColor = collisionColor;

        particleCaster_particleSystem.Play();

        // ������ �� ������
        Destroy(particleCaster_gameObject, 3);
    }

}
