using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float camLerpSpeed;
    [SerializeField] [Range(0, 1)] private float xMovementOffsetMul;
    [SerializeField] [Range(0, 1)] private float yMovementOffsetMul;
    [SerializeField] private float maxMovementOffset;
    [SerializeField] [Range(0, 1)] private float xMouseOffsetMul;
    [SerializeField] [Range(0, 1)] private float yMouseOffsetMul;

    [Header("Debug")]
    [SerializeField] private Vector2 movementOffset;
    [SerializeField] private Vector2 mouseOffset;
    [SerializeField] private Vector2 targetPos2D;

    private new Camera camera;
    private Player player;
    private Rigidbody2D playerRb;



    void Start()
    {
        camera = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
        playerRb = FindObjectOfType<Rigidbody2D>();
    }


    void LateUpdate()
    {
        // ������ �� ��������
        movementOffset = playerRb.velocity * new Vector2(xMovementOffsetMul, yMovementOffsetMul);

        if (movementOffset.x > maxMovementOffset)
            movementOffset.x = maxMovementOffset;
        if (movementOffset.x < -maxMovementOffset)
            movementOffset.x = -maxMovementOffset;

        if (movementOffset.y > maxMovementOffset)
            movementOffset.y = maxMovementOffset;
        if (movementOffset.y < -maxMovementOffset)
            movementOffset.y = -maxMovementOffset;


        // ������ �� �������
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 camScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 camToMouseDir = mouseScreenPos - camScreenPos;
        float DOWNSCALE = 10;
        mouseOffset = camToMouseDir * new Vector2(xMouseOffsetMul, yMouseOffsetMul) / DOWNSCALE;

        // ˸�� � (������ + �������)
        targetPos2D = (Vector2) player.transform.position  +  mouseOffset  +  movementOffset;
        Vector3 targetPos = new Vector3(targetPos2D.x,
                                        targetPos2D.y, 
                                        camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, camLerpSpeed * Time.deltaTime);


    }
}