using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (TryDoActionOnPlayer() == true)
            {
                Destroy(gameObject);
            }
            else
            {
                // Игнорим
            }
        }
    }

    protected abstract bool TryDoActionOnPlayer();

}
