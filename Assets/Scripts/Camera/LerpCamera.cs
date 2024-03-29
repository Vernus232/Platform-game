using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float camLerpSpeed;

    [Header("Offset")]
    [SerializeField] [Range(0, 1)] private float xMovementOffsetMul;
    [SerializeField] [Range(0, 1)] private float yMovementOffsetMul;
    [SerializeField] private float maxMovementOffset;
    [SerializeField] [Range(0, 1)] private float xMouseOffsetMul;
    [SerializeField] [Range(0, 1)] private float yMouseOffsetMul;
    [SerializeField] [Range(0, 1)] private float xRecoilOffsetMul;
    [SerializeField] [Range(0, 1)] private float yRecoilOffsetMul;
    
    [Header("Shake")]
    [SerializeField] [Range(0, 1)] private float shakeRecoveringMult;
    [SerializeField] [Range(1, 3)] private float shakeMult;


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
        recoilOffset *= shakeRecoveringMult;
    }

    void LateUpdate()
    {
        if (playerRb == null)
            return;

        // ������ �� ������
        //recoilOffset = 

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
        float DOWNSCALE = 50;
        mouseOffset = camToMouseDir * new Vector2(xMouseOffsetMul, yMouseOffsetMul) / DOWNSCALE;

        // ˸�� � (������ + �������)
        targetPos2D = (Vector2)player.transform.position + mouseOffset + movementOffset + recoilOffset;
        Vector3 targetPos = new Vector3(targetPos2D.x,
                                        targetPos2D.y, 
                                        camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, camLerpSpeed * Time.deltaTime);


    }

    public void OnShot(float cameraShakeMult, Vector2 rukiPivotDirection)
    {
        ShotShake(cameraShakeMult, rukiPivotDirection);
    }

    private void ShotShake(float weaponShakeMult, Vector2 direction)
    {
        Vector2 shakeVector2 = weaponShakeMult * shakeMult * -direction;
        recoilOffset += shakeVector2;
    }
}