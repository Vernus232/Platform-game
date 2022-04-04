using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Наследуем много чего от CommonProjectile
public class PlayerProjectile : CommonProjectile
{
        [Tooltip("Объект партикл системы при попадании")]
    [SerializeField] private GameObject particleCaster_prefab;

    public int penetration = 0;



    // Регистрация попадания во что-либо
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // В Obstacle
        if (collision.gameObject.layer == 3)
        {
            Color collisionColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Spawn_particleCaster_withColor(collisionColor);

            Debug.Log("Died to the wall");
            Destroy(gameObject);

            return;
        }

        // Во врага или декорации
        if (collision.gameObject.CompareTag("Enemy")  ||  collision.gameObject.CompareTag("Decoration"))
        {
            // Урон цели
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);

            // Пробитие
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
