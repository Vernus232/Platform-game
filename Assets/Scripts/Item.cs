using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoActionOnPlayer();

            Destroy(gameObject);
        }
    }

    protected abstract void DoActionOnPlayer();

}
