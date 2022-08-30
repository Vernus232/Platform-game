using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonProjectile : VanishingProjectile
{
        [Tooltip("Объект партикл системы при попадании")]
    [SerializeField] private GameObject obstacleHit_prefab;
    [SerializeField] private GameObject npcHit_prefab;

    public int penetration = 0;
    public int ricochets = 0;
    public float damage;

    private Vector2 prevFrameVelocity;
    private Vector3 prevFramePosition;


    public void SetParametersOnSpawn(Vector2 vel, Vector3 pos)
    {
        prevFrameVelocity = vel;
        prevFramePosition = pos;
    }

    private void Update()
    {
        Vector2 currFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (currFrameVelocity.magnitude >= 5)
        {
            prevFrameVelocity = currFrameVelocity;
        }
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
            SpawnPrefabWithColor(obstacleHit_prefab, collisionColor);

            PenetrationCheck(collision);

            return;
        }

        // Во врага или декорации
        if (collision.gameObject.CompareTag("Enemy")  ||  collision.gameObject.CompareTag("Decoration"))
        {
            SpawnPrefab(npcHit_prefab);

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
        if (penetration <= 0 && ricochets == 0)
        {
            Destroy(gameObject);
        }
        if (penetration <= 0 && ricochets > 0)
        {
            ricochets -= 1;
            return;
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
    // Метод для спавна геймобщекта после исчезновения пули (Для взрывов и партиклов))
    private void SpawnPrefab(GameObject prefab)
    {
        // Размещаем (Создаем его копию) префаб системы партиклов
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);

        // Запускаем анимацию выбранного инстанса(т.е. объект этого класса на сцене) партикл-системы
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            particleSystem.Play();
            Destroy(obj, 3);
        }
    }
    [System.Obsolete]
    private void SpawnPrefabWithColor(GameObject prefab, Color color)
    {
        // Размещаем (Создаем его копию) префаб системы партиклов
        GameObject obj = Instantiate(prefab, transform.position, transform.rotation);

        // Запускаем анимацию выбранного инстанса(т.е. объект этого класса на сцене) партикл-системы
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            particleSystem.Play();
            particleSystem.startColor = color;
            Destroy(obj, 3);
        }
    }

}
