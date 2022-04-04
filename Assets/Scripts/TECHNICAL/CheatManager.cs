using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private GameObject respawn;


    private void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.main.transform.position = respawn.transform.position;
        }
    }
}
