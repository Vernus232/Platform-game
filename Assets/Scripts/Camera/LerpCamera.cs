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

    [SerializeField] [Range(0, 1)] private float xRecoilOffsetMul;
    [SerializeField] [Range(0, 1)] private float yRecoilOffsetMul;


    [Header("Debug")]
    [SerializeField] private Vector2 recoilOffset;
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
        playerRb = player.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        recoilOffset - new Vector2(xRecoilOffsetMul, yRecoilOffsetMul)
    }

    void LateUpdate()
    {
        if (playerRb == null)
            return;

        // ־פפסוע מע ־עהאקט
        recoilOffset = 

        // ־פפסוע מע סךמנמסעט
        movementOffset = playerRb.velocity * new Vector2(xMovementOffsetMul, yMovementOffsetMul);

        if (movementOffset.x > maxMovementOffset)
            movementOffset.x = maxMovementOffset;
        if (movementOffset.x < -maxMovementOffset)
            movementOffset.x = -maxMovementOffset;

        if (movementOffset.y > maxMovementOffset)
            movementOffset.y = maxMovementOffset;
        if (movementOffset.y < -maxMovementOffset)
            movementOffset.y = -maxMovementOffset;


        // ־פפסוע מע ךףנסמנא
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 camScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 camToMouseDir = mouseScreenPos - camScreenPos;
        float DOWNSCALE = 50;
        mouseOffset = camToMouseDir * new Vector2(xMouseOffsetMul, yMouseOffsetMul) / DOWNSCALE;

        // ֻ¸נן ג (ןכוונא + מפפסועû)
        targetPos2D = (Vector2) player.transform.position  +  mouseOffset  +  movementOffset +;
        Vector3 targetPos = new Vector3(targetPos2D.x,
                                        targetPos2D.y, 
                                        camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, camLerpSpeed * Time.deltaTime);


    }

    public void OnShot(float a)
    {
        
    }

    private void ShotShake()
    {

    }
}