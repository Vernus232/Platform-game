using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Наследуем много чего от CommonProjectile
public class PlayerProjectile : CommonProjectile
{
    public GameObject hitPrefab;
    public bool destroysAnything = false;
    //public int penetration = 0;



    // Регистрация попадания во что-либо
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        // Во врага
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Урон цели
            FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
            entity.RecieveDamage(damage);
        }

        // В Obstacle
        if (collision.gameObject.layer == 3)
        {
            // Уничтожаем Obstacle, если можем
            if (destroysAnything)
                Destroy(collision.gameObject);
        }


        #region Частицы
        // Размещаем (Создаем его копию) префаб системы партиклов
        GameObject hitGameObject = Instantiate( hitPrefab, transform.position, transform.rotation );
            
        // Запускаем переменную ParticleSystem
        ParticleSystem hitPartileSystem = hitGameObject.GetComponent<ParticleSystem>();
        hitPartileSystem.Play();

        // Таймер на смерть
        Destroy(hitGameObject, 3);
        #endregion


        Destroy(gameObject);


    }



}
