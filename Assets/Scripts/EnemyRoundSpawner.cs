using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoundSpawner : MonoBehaviour
{
    public float spawnStep;
    public float dist;

    public GameObject enemyPrefab;

    private Vector2 resolutionProportion = new Vector2(16, 9);



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
            SpawnEnemy();

            yield return new WaitForSeconds(spawnStep);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 getAppropriatePos()
        {
            float x = Random.Range(-1.5f, 1.5f);
            float y = Random.Range(0f, 1.5f);

            while (Mathf.Abs(x) < 1  &&  y < 1)
            {
                x = Random.Range(-1.5f, 1.5f);
                y = Random.Range(0f, 1.5f);
            }

            return new Vector2(x, y);
        }

        Vector2 pos2D = resolutionProportion * dist * getAppropriatePos();
        Vector3 spawnPos = transform.position + new Vector3(pos2D.x, pos2D.y, 0f);

        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
    }


}
