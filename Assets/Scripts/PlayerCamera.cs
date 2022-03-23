using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    private Camera playerCamera;


    private void Start()
    {
        playerCamera = gameObject.GetComponent<Camera>();
        player = FindObjectOfType<GameObject>();
    }

    void LateUpdate()
    {
        if (player)
            playerCamera.transform.position = new Vector3(player.transform.position.x, -1 / 5 + player.transform.position.y, playerCamera.transform.position.z);
        else
            Debug.LogError("no player");
    }
}
