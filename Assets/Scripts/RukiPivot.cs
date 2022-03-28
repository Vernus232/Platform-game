using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RukiPivot : MonoBehaviour
{
    private Player player;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        #region Наводка
        //int sideViewMul = 0;
        //if (!player.isLookingRight)
        //    sideViewMul = 1;
        //Debug.Log(sideViewMul);

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 normalizedScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= normalizedScreenPos.x;
        mouseScreenPos.y -= normalizedScreenPos.y;
        float angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;

        //float correctedAngle = angle;
        //if (Mathf.Abs(angle) <= 360)
        //     correctedAngle = angle + sideViewMul * 180f;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Mathf.Abs(angle) >= 90)
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x,  1, transform.localScale.z);

        #endregion
    }
}
