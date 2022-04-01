using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurpCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float offsetChangeSpeed;
    [SerializeField] private float maxOffset;
    [SerializeField] private float mouseOffset;

    [Space (5)]
    [Header("Пиво)")]
    [SerializeField] private Transform playerPivoTransform;
    private new Camera camera;
    private Player player;
    private float offset;
    



    void Start()
    {
        camera = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        #region Попа какаято
        if (Input.GetKey(KeyCode.D))
        {
            offset += offsetChangeSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            offset -= offsetChangeSpeed * Time.deltaTime;
        }
        if (offset > maxOffset)
        {
            offset = maxOffset;
        }
        if (offset < -maxOffset)
        {
            offset = -maxOffset;
        }
        #endregion

        float additionalOffset = 0;
        if (playerPivoTransform.localScale.x == 1)
        {
            additionalOffset = mouseOffset;
        }

        if (playerPivoTransform.localScale.x == -1)
        {
            additionalOffset = -mouseOffset;
        }
        Vector3 targetPos = new Vector3(player.transform.position.x + offset + additionalOffset, player.transform.position.y, camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, camMoveSpeed * Time.deltaTime);


    }
}