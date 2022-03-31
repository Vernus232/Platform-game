using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float betweenHitsTime;

    private float prevHitTime = 0;



    // При попадании в collision...
    private void OnTriggerStay2D(Collider2D other)
    {
        //..игрока...
        if (other.gameObject.tag == "Player")
        {
            //...чекаем когда последний раз мы его били, и если это было давнее betweenHitsTime - бьём снова * сына
            float currTime = Time.time;
            if ((currTime - prevHitTime) > betweenHitsTime)
            {
                // Вычисляем его по айпи (коллайдеру) и сообщаем урон
                FragileEntity entity = other.gameObject.GetComponent<FragileEntity>();
                entity.RecieveDamage(damage);

                prevHitTime = currTime;
            }
        }
    }


}
