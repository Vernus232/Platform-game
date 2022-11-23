using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamagePopup : MonoBehaviour
{
    public float DamageAmount
    {
        get
        {
            return damageAmount;
        }
        set
        {
            damageAmount = value;
            damageText.text = damageAmount.ToString("-00");
        }
    }
    private float damageAmount;
    
    [SerializeField] private Text damageText;


    public void SetLifetime(float time){
        Destroy(gameObject, time);
    }

}
