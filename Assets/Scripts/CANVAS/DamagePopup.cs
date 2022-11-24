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
            OnDamageChanged();
        }
    }
    private float damageAmount;
    public Gradient damageGradient;
    [SerializeField] private Text damageText;


    private void OnDamageChanged()
    {
        damageText.text = damageAmount.ToString("0");
        
        int MAX_FS = 200;
        int initialFS = 20 + Mathf.RoundToInt(damageAmount / 4);            
        if (initialFS > MAX_FS)
            damageText.fontSize = MAX_FS;
        else
            damageText.fontSize = initialFS;

        damageText.color = damageGradient.Evaluate(damageAmount / 4 / 50);
        
        float s(float x)
        {
            float k = 0.00235f;
            float b = 0.3f;
            return k*x + b;
        }
        float lifetime = s(damageAmount);
        StartCoroutine(PopupLife(lifetime));  
    }

    private IEnumerator PopupLife(float lifetime)
    {
        float UPDATE_STEP = 0.05f;
        float P = 2;
        int initialFontSize = damageText.fontSize;
        float f(float x)
        {
            float m = Mathf.Pow(lifetime, -P);
            return 1 - m * Mathf.Pow(x, P);
        }

        float t = 0;
        while (t < lifetime)
        {
            damageText.fontSize = 1 + Mathf.RoundToInt(initialFontSize * f(t));
            
            yield return new WaitForSeconds(UPDATE_STEP);
            t += UPDATE_STEP;
        }
        
        Destroy(gameObject);
    }


}
