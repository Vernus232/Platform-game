using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private float damagePopupLifetime;

    [HideInInspector] public static PopupView main;
    Dictionary<FragileEntity, GameObject> entityPopupDict = new Dictionary<FragileEntity, GameObject>();
    private Canvas canvas;


    private void Start() {
        main = this;
        canvas = FindObjectOfType<Canvas>();
    }
    

    public void Try_CreateDamagePopup(FragileEntity entity, float amount)
    {
        if (entityPopupDict.ContainsKey(entity)  &&  entityPopupDict[entity] != null)
        {
            GameObject oldPopupGameObj = entityPopupDict[entity];

            // Getting summarized amount
            float oldAmount = oldPopupGameObj.GetComponent<DamagePopup>().DamageAmount;
            float summarizedAmount = amount + oldAmount;
            
            // Operations on popupGameObjects
            Destroy(oldPopupGameObj);
            GameObject newPopup = CreateDamagePopup(entity.transform.position, summarizedAmount);

            // Operations on dict
            entityPopupDict[entity] = newPopup;
        }
        if (entityPopupDict.ContainsKey(entity)  &&  entityPopupDict[entity] == null)
        {
            GameObject newPopup = CreateDamagePopup(entity.transform.position, amount);
            entityPopupDict[entity] = newPopup;
        }
        if (!entityPopupDict.ContainsKey(entity))
        {
            GameObject newPopup = CreateDamagePopup(entity.transform.position, amount);
            entityPopupDict.Add(entity, newPopup);
        }
    }

    private GameObject CreateDamagePopup(Vector3 position, float amount)
    {
        Vector2 WorldToScreenPos(Vector3 worldPos)
        {
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 screenPosition = new Vector2(
                (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
            );

            return screenPosition;
        }

        // Create popup
        GameObject damagePopupGameObj = Instantiate(damagePopupPrefab);
        damagePopupGameObj.transform.SetParent(canvas.transform);

        // Set popup parameters
        damagePopupGameObj.GetComponent<RectTransform>().anchoredPosition = WorldToScreenPos(position);
        // damagePopupGameObj.GetComponent<DamagePopup>().Lifetime = damagePopupLifetime;
        damagePopupGameObj.GetComponent<DamagePopup>().DamageAmount = amount;
        

        return damagePopupGameObj;
    }

    


}
