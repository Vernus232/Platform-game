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
            SetScale(Mathf.RoundToInt(damageAmount) / 4);
            SetColor(damageAmount / 4);
        }
    }
    private float damageAmount;
    public Gradient damageGradient;
    [SerializeField] private Text damageText;


    public void SetLifetime(float time){
        Destroy(gameObject, time);
    }

    public void SetScale(int scaleValue)
    {
        damageText.fontSize = 20 + scaleValue;
    }

    public void SetColor(float value)
    {
        damageText.color = damageGradient.Evaluate(value / 50);
    }
}
