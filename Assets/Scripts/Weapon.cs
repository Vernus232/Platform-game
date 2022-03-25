using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject projectileObj;
    public float speed;
    public float betweenShotsTime;

    private float prevShootTime = 0;

    private Camera playerCamera;
    private Player player;


    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
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

        #region Поворот

        float curAngle = gameObject.transform.rotation.z;

        if (Mathf.Abs(curAngle) > 0.7)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            Debug.Log(curAngle);
        }

        if (Mathf.Abs(curAngle) < 0.7)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
            Debug.Log(curAngle);
        }

        #endregion
    }

    private void InstantiateProjectile()
    {
        GameObject ball = Instantiate(projectileObj, shootingPoint.position, transform.rotation);
        ball.GetComponent<PlayerProjectile>().damage *= player.damageModifier;
        ball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(speed, 0, 0));
    }


}
