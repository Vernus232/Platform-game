using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    private Camera playerCamera;


    private void Start()
    {
        playerCamera = Camera.main;
        player = FindObjectOfType<GameObject>();
    }

    // Вызывается после всех симмуляций (как раз перед "снимком" кадра)
    void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        Vector3 camPos = Camera.main.transform.position;

        // Двигаем камеру в точку между положением плеера и курсора
        playerCamera.transform.position = new Vector3(  (mousePos.x + playerPos.x)/3,
                                                        (mousePos.y + playerPos.y)/3,
                                                        camPos.z);
    }
}
