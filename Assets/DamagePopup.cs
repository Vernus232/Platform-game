using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamagePopup : MonoBehaviour
{
    [SerializeField] private Text damageText;

    
    public void CreateDamageText(Transform transform, float damage)
    {
        
        Vector2 ViewportPosition = Cam.WorldToViewportPoint(WorldObject.transform.position);
        Vector2 WorldObject_ScreenPosition=new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));

        //now you can set the position of the ui element
        UI_Element.anchoredPosition = WorldObject_ScreenPosition;

        damageText.text = damage.ToString("-00");
        Instantiate(damageText, transform);
    }
}
