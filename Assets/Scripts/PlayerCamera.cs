using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float closeToPlayerCoef;

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
        float a = closeToPlayerCoef;
        playerCamera.transform.position = new Vector3(  mousePos.x * (1-a) +  playerPos.x * a,
                                                        mousePos.y * (1-a) +  playerPos.y * a,
                                                        camPos.z);
    }
}
