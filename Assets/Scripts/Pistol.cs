using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject projectile;
    public float speed;
    public float betweenShotsTime;

    private float prevShootTime = 0;
    private Camera playerCamera;


    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        #region Наводка
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = playerCamera.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        #endregion

        #region Выстрел и задержка
        if (Input.GetMouseButton(0))
        {
            float currTime = Time.time;
            if ((currTime - prevShootTime) > betweenShotsTime)
            {
                InstantiateProjectile();
                prevShootTime = currTime;
            }
        }
        #endregion
    }

    private void InstantiateProjectile()
    {
        GameObject ball = Instantiate(projectile, shootingPoint.position, transform.rotation);
        ball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(speed, 0, 0));
    }


}
