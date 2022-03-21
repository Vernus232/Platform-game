using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    public float lifespan;



    // Ставим таймер смерти пули (при спавне пули)
    private void Awake()
    {
        Destroy(gameObject, lifespan);
    }


    // Поворот пули в полёте
    private void FixedUpdate()
    {
        Vector2 flightDir = GetComponent<Rigidbody2D>().velocity;
        float angle = Vector2.Angle(flightDir.normalized, Vector2.left);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    // Регистрация попадания во что-либо
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        // Пытаемся сообщить урон Fraglie Entity
        FragileEntity entity = collision.gameObject.GetComponent<FragileEntity>();
        if (entity)
        {
            // Сообщаем урон entity
            entity.RecieveDamage(damage);
        }


        // Уничтожаем пулю
        Destroy(gameObject);
        
    }


}
