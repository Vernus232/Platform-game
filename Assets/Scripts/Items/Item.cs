using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float timeToLive = 30;

    private void Awake()
    {
        if (timeToLive > 0)
            Destroy(gameObject, timeToLive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (TryDoActionOnPlayer() == true)
            {
                Destroy(gameObject);
            }
        }
    }

    protected abstract bool TryDoActionOnPlayer();

}
