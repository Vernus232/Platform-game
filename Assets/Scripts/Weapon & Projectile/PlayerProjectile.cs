using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ���� �� CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("������ ������� ������� ��� ���������")]
    [SerializeField] private GameObject particleCaster_prefab;

    public int penetration = 0;

    private Quaternion prevRotation;
    private Vector2 prevVelocity;



    private void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        yield return new WaitForFixedUpdate();

        prevRotation = transform.rotation;
        prevVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    }


    // ����������� ��������� �� ���-����
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
        transform.position = transform.position - transform.right * 0.01f;
        //transform.rotation = prevRotation;
        gameObject.GetComponent<Rigidbody2D>().velocity = prevVelocity;
        
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        
        penetration -= 1;
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
