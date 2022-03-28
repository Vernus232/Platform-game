using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float damageMultiplier;

    private Player player;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.damageModifier *= damageMultiplier;

        Destroy(gameObject);
    }

}
