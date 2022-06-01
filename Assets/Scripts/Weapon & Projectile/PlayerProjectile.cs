using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Наследуем много чего от CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("Объект партикл системы при попадании")]
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

    // Регистрация попадания во что-либо
    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // В Obstacle
        if (collision.gameObject.layer == 3)
        {
            Color collisionColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Spawn_particleCaster_withColor(collisionColor);

            // Пробитие
            PenetrationCheck(collision);

            return;
        }

        // Во врага или декорации
        if (collision.gameObject.CompareTag("Enemy")  ||  collision.gameObject.CompareTag("Decoration"))
        {
            // Урон цели
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);

            // Пробитие
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
        // Размещаем (Создаем его копию) префаб системы партиклов
        GameObject particleCaster_gameObject = Instantiate(particleCaster_prefab, transform.position, transform.rotation);

        // Запускаем анимацию выбранного инстанса(т.е. объект этого класса на сцене) партикл-системы
        ParticleSystem particleCaster_particleSystem = particleCaster_gameObject.GetComponent<ParticleSystem>();
        particleCaster_particleSystem.startColor = collisionColor;

        particleCaster_particleSystem.Play();

        // Таймер на смерть
        Destroy(particleCaster_gameObject, 3);
    }

}
