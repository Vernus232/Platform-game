using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Tooltip("Как сильно смещается камера к игроку (0; 1)")] 
        [SerializeField] private float closeToPlayerCoef;
    [Tooltip("Сила отдаления")]
        [Range(2, 10)]  [SerializeField] private float unzoomMultiplier;
    [Tooltip("С какой дистации от края экрана начинается анзум")]
        [Range(0, 1)]  [SerializeField] private float unzoomMinDist;

    private float defaultOrthographicSize;


    private void Start()
    {
        defaultOrthographicSize = Camera.main.orthographicSize;
    }

    // Вызывается после всех симмуляций (как раз перед "снимком" кадра)
    void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = Player.main.transform.position;
        Vector3 camPos = Camera.main.transform.position;

        // Двигаем камеру в точку между положением плеера и курсора
        float a = closeToPlayerCoef;
        Camera.main.transform.position = new Vector3(  mousePos.x * (1-a) +  playerPos.x * a,
                                                        mousePos.y * (1-a) +  playerPos.y * a,
                                                        camPos.z);

        Vector2 vecFromCenter = new Vector2(Input.mousePosition.x / Screen.width - 0.5f,
                                            Input.mousePosition.y / Screen.height - 0.5f);
        vecFromCenter *= 2;  // до диапазона (-1;1)x(-1;1)
        float maxDistFromCenter = Mathf.Max(Mathf.Pow(vecFromCenter.x, 2) + 
                                            Mathf.Pow(vecFromCenter.y, 2));
        if (maxDistFromCenter > 1)
            maxDistFromCenter = 1;

        // Меняем зум, используя десмос        
        float GetZoom(float x, float d, int p, float m)
        {
            if (x <= d)
                return 1f;
            else
                return Mathf.Pow(x - d, p) * m  +  1;
        }
        float zoom = GetZoom(maxDistFromCenter, unzoomMinDist, 2, unzoomMultiplier);

        Camera.main.orthographicSize = defaultOrthographicSize * zoom;
       
    }

    


}
