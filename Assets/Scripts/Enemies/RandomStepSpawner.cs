using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStepSpawner : MonoBehaviour
{
    public float spawnStep;

    [SerializeField] private GameObject spawnedPrefab;
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;
    [SerializeField] private GameObject prefab3;
    [SerializeField] private GameObject prefab4;
    [SerializeField] private GameObject prefab5;



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
            yield return new WaitForSeconds(spawnStep);
            Spawn();
        }
    }

    private void Spawn()
    {
        {
            int x = Random.Range(1, 5+1);

            if (x == 1)
                spawnedPrefab = prefab1;
            else if (x == 2)
                spawnedPrefab = prefab2;
            else if (x == 3)
                spawnedPrefab = prefab3;
            else if (x == 4)
                spawnedPrefab = prefab4;
            else if (x == 5)
                spawnedPrefab = prefab5;
            
        }

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
                GameObject instantiatedObj = Instantiate(spawnedPrefab, randomPoint, spawnedPrefab.transform.rotation);
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
