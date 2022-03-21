using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnPoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
