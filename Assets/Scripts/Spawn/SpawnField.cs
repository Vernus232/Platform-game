using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    [SerializeField] private float minDistFromPlayer = 5;



    public Vector2 RequestViableSpawnPoint()  // TODO Give object to spawn collider sizes, as an argument
    {
        Collider2D collider = GetComponent<Collider2D>();
        Bounds squareBounds = collider.bounds;
        
        Vector2 create_randomPoint_in_bounds()
        {
            return new Vector2( Random.Range(squareBounds.min.x, squareBounds.max.x),
                                Random.Range(squareBounds.min.y, squareBounds.max.y));
        }
        Vector2 randomPoint = create_randomPoint_in_bounds();

        int fails = 0;
        while (fails < 100)
        {
            if (collider.OverlapPoint(randomPoint)  &  Vector2.Distance(randomPoint, Player.main.transform.position) > minDistFromPlayer)
            {
                return randomPoint;
            }
            else
            {
                randomPoint = create_randomPoint_in_bounds();
                fails++;
            }             
        }

        return Vector2.negativeInfinity;  // If unsuccessfull to find a spawn point
    }
}
