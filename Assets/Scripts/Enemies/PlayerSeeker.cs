using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeeker : MonoBehaviour
{
    private GameObject playerObj;


    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
            gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = playerObj.transform;
    }


}
