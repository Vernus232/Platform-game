using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float closeToPlayerCoef;
    [Range(2, 10)] public float unzoomMultiplier;
    [Range(0, 1)] public float unzoomMinDist;

    private GameObject player;
    private Camera playerCamera;
    private float defaultOrthographicSize;


    private void Start()
    {
        playerCamera = Camera.main;
        player = FindObjectOfType<Player>().gameObject;
        defaultOrthographicSize = playerCamera.orthographicSize;
    }

    // ¬ызываетс€ после всех симмул€ций (как раз перед "снимком" кадра)
    void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        Vector3 camPos = Camera.main.transform.position;

        // ƒвигаем камеру в точку между положением плеера и курсора
        float a = closeToPlayerCoef;
        playerCamera.transform.position = new Vector3(  mousePos.x * (1-a) +  playerPos.x * a,
                                                        mousePos.y * (1-a) +  playerPos.y * a,
                                                        camPos.z);

        Vector2 vecFromCenter = new Vector2(Input.mousePosition.x / Screen.width - 0.5f,
                                            Input.mousePosition.y / Screen.height - 0.5f);
        vecFromCenter *= 2;  // до диапазона (-1;1)x(-1;1)
        float maxDistFromCenter = Mathf.Max(Mathf.Pow(vecFromCenter.x, 2) + 
                                            Mathf.Pow(vecFromCenter.y, 2));
        if (maxDistFromCenter > 1)
            maxDistFromCenter = 1;

        // ћен€ем зум, использу€ десмос        
        float GetZoom(float x, float d, int p, float m)
        {
            if (x <= d)
                return 1f;
            else
                return Mathf.Pow(x - d, p) * m  +  1;
        }
        float zoom = GetZoom(maxDistFromCenter, unzoomMinDist, 2, unzoomMultiplier);

        playerCamera.orthographicSize = defaultOrthographicSize * zoom;


    }
}
