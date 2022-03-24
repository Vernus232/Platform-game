using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoundSpawner : MonoBehaviour
{
    public float spawnStep;
    public float dist;

    public GameObject enemyPrefab;

    private int resolutionProportionX = 16;
    private int resolutionProportionY = 9;


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
        float randomSign()
        {
            float randSign = Random.value < .5 ? 1 : -1;
            return randSign;
        }

        float xPos = resolutionProportionX * dist * randomSign() * Random.Range(1f, 1.5f);
        float yPos = resolutionProportionY * dist *                Random.Range(0f, 1.5f);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
    }


}
