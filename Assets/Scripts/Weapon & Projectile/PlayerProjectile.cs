using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("������ ������� ������� ��� ���������")]
    [SerializeField] private GameObject particleCaster_prefab;

    public int penetration = 0;

    private Vector2 prevFrameVelocity;
    private Vector3 prevFramePosition;


    public void SetParametersOnSpawn(Vector2 vel, Vector3 pos)
    {
        prevFrameVelocity = vel;
        prevFramePosition = pos;
    }

    private void Update()
    {
        prevFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        prevFramePosition = transform.position;
    }

    // ����������� ��������� �� ���-����
    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // � Obstacle
        if (collision.gameObject.layer == 3)
        {
            Color collisionColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Spawn_particleCaster_withColor(collisionColor);

            // ��������
            PenetrationCheck(collision);

            return;
        }

        // �� ����� ��� ���������
        if (collision.gameObject.CompareTag("Enemy")  ||  collision.gameObject.CompareTag("Decoration"))
        {
            // ���� ����
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);

            // ��������
            PenetrationCheck(collision);

            return;
        }
    }

    private void PenetrationCheck(Collision2D collision)
    {
        if (penetration > 0)
        {
            ReplaceProjectile(collision);
        }
        if (penetration <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ReplaceProjectile(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = prevFrameVelocity;
        transform.position = prevFramePosition;

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);

        penetration -= 1;
    }

    [System.Obsolete]
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
