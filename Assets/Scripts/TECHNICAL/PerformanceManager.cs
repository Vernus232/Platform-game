using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PerformanceManager : MonoBehaviour
{
    public float updateTime;
    public int maxLights;
    public int maxItems;

    private void Start()
    {
        StartCoroutine(CheckCycle());
    }

    private IEnumerator CheckCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);

            Light2D[] lights = FindObjectsOfType<Light2D>();
            if (lights.Length > maxLights)
            {
                foreach (Light2D light in lights)
                {
                    if (light.GetComponentInParent<Item>())
                        Destroy(light);
                }
            }


            Item[] items = FindObjectsOfType<Item>();
            if (items.Length > maxItems)
            {
                foreach (Item item in items)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
