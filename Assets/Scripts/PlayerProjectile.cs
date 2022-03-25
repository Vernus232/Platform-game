using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Наследуем много чего от CommonProjectile
public class PlayerProjectile : CommonProjectile
{
    public GameObject hitPrefab;
    public bool destroysAnything;


    // Регистрация попадания во что-либо
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Игнорим коллижн с объектом тега Player
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision( collision.collider, gameObject.GetComponent<Collider2D>() );
        }

        // Обрабатываем колижн не с игроком
        else
        {
            // Пытаемся сообщить урон Fraglie Entity
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            if (entity)
                entity.RecieveDamage(damage);

            // Уничтожаем цель, если можем всё
            if (destroysAnything)
                Destroy(collision.gameObject);



            
            // Размещаем (Создаем его копию) префаб системы партиклов
            GameObject hitGameObject = Instantiate( hitPrefab, transform.position, transform.rotation );
            
            // Запускаем переменную ParticleSystem
            ParticleSystem hitPartileSystem = hitGameObject.GetComponent<ParticleSystem>();
            hitPartileSystem.Play();

            // Уничтожаем пулю
            Destroy(gameObject);

            Destroy(hitGameObject, 5);


        }
    }





}
