using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoundSpawner : MonoBehaviour
{
    public float spawnStep;
    public float spawnRange;

    public GameObject enemyPrefab;



    private void Start()
    {
        StartCoroutine("SpawnTicker");
    }

    private IEnumerator SpawnTicker()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnStep);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos2D = Random.insideUnitCircle * spawnRange;
        Vector3 spawnPos = new Vector3(spawnPos2D.x, spawnPos2D.y, 0f);
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
    }


}
