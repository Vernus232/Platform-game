using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public float betweenShotsTime;

    private float prevShootTime = 0;


    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            float currTime = Time.time;
            if ((currTime - prevShootTime) > betweenShotsTime)
            {
                InstantiateProjectile();
                prevShootTime = currTime;
            }
        }        
    }

    private void InstantiateProjectile()
    {
        GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(speed, 0, 0));
    }



}
