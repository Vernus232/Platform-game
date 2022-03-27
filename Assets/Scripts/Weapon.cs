using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ���������� ���������
    [SerializeField] private string Name;

    [Space(10)]

    [Header("Speed")]

    [SerializeField] private float projectilesSpeed;
    [SerializeField] private float projMaxSpeedDifferenceMul;

    [Space(10)]

    [Header("Burst")]

    [SerializeField] private float FireRate;
    [SerializeField] private int burstCount = 1;
    [SerializeField] private bool instantiateBurstMomentally = false;

    [Space(10)]

    [Header("Recoil")]

    [SerializeField] private float maxWeaponRecoil;
    [SerializeField] private float minWeaponRecoil;
    [SerializeField] private float recoilIncreaseWithShot;
    [SerializeField] private float recoilReductionWithTime;
    [SerializeField] private float movementRecoilImportance;


    [Space(10)]

    [Header("Links")]

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
        #region �������
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = playerCamera.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        #endregion

        #region ������� � ��������
        if (Input.GetMouseButton(0))
        {
            float currTime = Time.time;
            if ((currTime - prevShootTime) > FireRate)
            {
                if (instantiateBurstMomentally)
                    InstantiateProjectilesMomentally();
                else
                    StartCoroutine("InstantiateProjectiles");

                prevShootTime = currTime;
            }
        }
        #endregion

        #region �������
        float curAngle = gameObject.transform.rotation.z;
        
        if (Mathf.Abs(curAngle) >= 0.7)
            gameObject.GetComponent<SpriteRenderer>().flipY = true;

        if (Mathf.Abs(curAngle) < 0.7)
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        #endregion
    }

    private void FixedUpdate()
    {
        #region �������� ������
        recoil -= recoilReductionWithTime;

        if (recoil < minWeaponRecoil)
            recoil = minWeaponRecoil;

        if (recoil > maxWeaponRecoil)
            recoil = maxWeaponRecoil;

        //Debug.Log("======");
        //Debug.Log("weapon recoil " + recoil.ToString());
        //Debug.Log("player recoil " + player.recoilFromMovement.ToString());
        //Debug.Log("======");
        #endregion
    }

    #region ������
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
    #endregion

    private void InstantiateProjectile()
    {
        // ���������� ��������� �������
        float totalRecoil = player.movementRecoil * movementRecoilImportance  +  recoil;

        // ���������� ������ ��������
        float zRotationChange = Random.Range(0f, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // ���������� �������� � �������� ����
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // ���������� ����
        GameObject ball = Instantiate(projectileObj, shootingPoint.position, randomedRotation);
        ball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(projectilesSpeed * projSpeedDifferenceMul, 0, 0));
        ball.GetComponent<PlayerProjectile>().damage *= player.damageModifier;

        // �������� ������� ������
        recoil += recoilIncreaseWithShot;
    }


}
