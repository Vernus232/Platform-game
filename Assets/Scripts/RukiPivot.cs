using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RukiPivot : MonoBehaviour
{

    private void Update()
    {
        #region �������
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 normalizedScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= normalizedScreenPos.x;
        mouseScreenPos.y -= normalizedScreenPos.y;
        float angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Mathf.Abs(angle) >= 90)
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x,  1, transform.localScale.z);
        #endregion
    }
}