using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Постоянные параметры
    [SerializeField] private string Name;
    [SerializeField] private float projectilesSpeed;
    [SerializeField] private float betweenShotsTime;

    [SerializeField] private int burstCount = 1;
    [SerializeField] private bool instantiateBurstMomentally = false;

    [SerializeField] private float recoilIncreaseWithShot;
    [SerializeField] private float recoilReductionWithTime;
    [SerializeField] private float minWeaponRecoil;
    [SerializeField] private float maxWeaponRecoil;
    [SerializeField] private float projMaxSpeedDifferenceMul;

    

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject projectileObj;


    private float prevShootTime = 0;
    private float recoil;

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
                if (instantiateBurstMomentally)
                    InstantiateProjectilesMomentally();
                else
                    StartCoroutine("InstantiateProjectiles");

                prevShootTime = currTime;
            }
        }
        #endregion

        #region Поворот
        float curAngle = gameObject.transform.rotation.z;
        
        if (Mathf.Abs(curAngle) >= 0.7)
            gameObject.GetComponent<SpriteRenderer>().flipY = true;

        if (Mathf.Abs(curAngle) < 0.7)
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        #endregion
    }

    private void FixedUpdate()
    {
        recoil -= recoilReductionWithTime;

        if (recoil < minWeaponRecoil)
            recoil = minWeaponRecoil;

        if (recoil > maxWeaponRecoil)
            recoil = maxWeaponRecoil;

        Debug.Log("======");
        Debug.Log("weapon recoil " + recoil.ToString());
        Debug.Log("player recoil " + player.recoilFromMovement.ToString());
        Debug.Log("======");
    }

    private IEnumerator InstantiateProjectiles()
    {
        for (int i = 0; i < burstCount; i++)
        {
            InstantiateProjectile();

            yield return new WaitForEndOfFrame();
        }
    }

    private void InstantiateProjectilesMomentally()
    {
        for (int i = 0; i < burstCount; i++)
        {
            InstantiateProjectile();
        }
    }

    private void InstantiateProjectile()
    {
        // Определили конечный разброс
        float totalRecoil = player.recoilFromMovement + recoil;

        // Определили вектор выстрела
        float zRotationChange = Random.Range(0f, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // Определили разность в скорости пуль
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // Заспавнили пули
        GameObject ball = Instantiate(projectileObj, shootingPoint.position, randomedRotation);
        ball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(projectilesSpeed * projSpeedDifferenceMul, 0, 0));
        ball.GetComponent<PlayerProjectile>().damage *= player.damageModifier;

        // Сообщили разброс оружию
        recoil += recoilIncreaseWithShot;
    }


}
