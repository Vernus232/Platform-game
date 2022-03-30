using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnStep;
    [SerializeField] private GameObject prefab;


     
    private void OnEnable()
    {
        StartCoroutine("SpawnTicker");
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // стопает все корутины в скрипте
    }

    private IEnumerator SpawnTicker()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(spawnStep);
        }
    }

    private void Spawn()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        
        Vector2 create_randomPoint_in_bounds()
        {
            return new Vector2( Random.Range(bounds.min.x, bounds.max.x),
                                Random.Range(bounds.min.y, bounds.max.y));
        }
        
        Vector2 randomPoint = create_randomPoint_in_bounds();

        int fails = 0;
        while (fails < 100)
        {
            if (collider.OverlapPoint(randomPoint))
            {
                GameObject instantiatedObj = Instantiate(prefab, randomPoint, prefab.transform.rotation);
                instantiatedObj.SetActive(true);
                break;
            }
            else
            {
                randomPoint = create_randomPoint_in_bounds();
                fails++;
            }
        }
    }

    


}
