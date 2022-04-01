using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurpCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float walkingOffsetChangeSpeed;
    [SerializeField] private float maxWalkingOffset;
    [Space(5)]
    [SerializeField] private float mouseOffset;
    [SerializeField] private float closeToPlayerCoef;

    [Space (10)]
    [Header("Refs")]
    [SerializeField] private Transform playerPivotTransform;

    private float walkingOffset;

    private new Camera camera;
    private Player player;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
    }


    void LateUpdate()
    {
        // Оффсет от ходьбы
        //float walkingOffset
        if (Input.GetKey(KeyCode.A))
            walkingOffset -= walkingOffsetChangeSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            walkingOffset += walkingOffsetChangeSpeed * Time.deltaTime;

        if (walkingOffset > maxWalkingOffset)
            walkingOffset = maxWalkingOffset;
        if (walkingOffset < -maxWalkingOffset)
            walkingOffset = -maxWalkingOffset;


        // Оффсет от курсора
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = Player.main.transform.position;

        float a = closeToPlayerCoef;
        Vector2 mouseOffset = new Vector3(  mousePos.x * (1 - a) + playerPos.x * a,
                                            mousePos.y * (1 - a) + playerPos.y * a);

       
        // Лёрп в плеера + оффсеты
        Vector3 targetPos = new Vector3(player.transform.position.x + mouseOffset.x + walkingOffset, 
                                        player.transform.position.y + mouseOffset.y, 
                                        camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, camMoveSpeed * Time.deltaTime);


    }
}