using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawner : MonoBehaviour
{
    public float spawnStep;
    [SerializeField] private float minDistFromPlayer = 5;

    [SerializeField] protected GameObject prefab;
    [SerializeField] protected GameObject particleSysPrefab;


    
    protected virtual void OnEnable()
    {
        StartCoroutine(SpawnTicker());
    }

    protected void OnDisable()
    {
        StopAllCoroutines(); // ������� ��� �������� � �������
    }

    protected IEnumerator SpawnTicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnStep);
            if (Player.main)
                Spawn();
        }        
    }

    protected void Spawn()
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
            if (collider.OverlapPoint(randomPoint)  &  Vector2.Distance(randomPoint, Player.main.transform.position) > minDistFromPlayer)
            {
                StartCoroutine(DoSpawnEntity(randomPoint));
                break;
            }
            else
            {
                randomPoint = create_randomPoint_in_bounds();
                fails++;
            }             
        }
    }

    [System.Obsolete]
    private IEnumerator DoSpawnEntity(Vector2 randomPoint)
    {
        if (GameOptions.Particles)
        {
            Instantiate(particleSysPrefab, randomPoint, particleSysPrefab.transform.rotation);
            float particleSystemLifetime = particleSysPrefab.GetComponent<ParticleSystem>().startLifetime;

            yield return new WaitForSeconds(particleSystemLifetime);
        }
        

        GameObject instantiatedObj = Instantiate(prefab, randomPoint, prefab.transform.rotation);
        instantiatedObj.SetActive(true);
    }
}
