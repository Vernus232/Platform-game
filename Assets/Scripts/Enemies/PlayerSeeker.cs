using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{

    void Awake()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
            GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
    }


}
