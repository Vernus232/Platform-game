using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{
    private Pathfinding.AIDestinationSetter AI;
    private GameObject playerObj;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = playerObj.transform;
    }


}
