using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerformanceManager : MonoBehaviour
{
    public float radiusOfDelete;
    public float updateTime;
    public int maxEnemies;
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

            if (!Player.main)
                break;
            
            UnityEngine.Rendering.Universal.Light2D[] lights = FindObjectsOfType<UnityEngine.Rendering.Universal.Light2D>();
            if (lights.Length > maxLights)
            {
                foreach (UnityEngine.Rendering.Universal.Light2D light in lights)
                {
                    if (light.GetComponentInParent<Item>())
                        Destroy(light);
                }
            }


            Item[] items = FindObjectsOfType<Item>();
            if (items.Length > maxItems)
                foreach (Item item in items)
                    Destroy(item.gameObject);

            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length > maxEnemies)
            {
                int enemiesToDelete = enemies.Length - maxEnemies;
                int e = 0;
                foreach (Enemy enemy in enemies)
                {
                    if (Vector2.Distance(enemy.transform.position, Player.main.transform.position) > radiusOfDelete)
                    {
                        Destroy(enemy.gameObject);
                        e += 1;
                        if (e >= enemiesToDelete)
                            break;
                    }
                }
            }
        }
    }
}
