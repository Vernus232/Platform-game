using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{

    void Awake()
    {
        Transform playerTransform = GameObject.FindObjectOfType<Player>().transform;
        if (playerTransform)
            gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = playerTransform;
    }


}
