using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : Weapon
{
    [Header("Speed")]

    [SerializeField] private float projectilesSpeed;
    [SerializeField] private float projMaxSpeedDifferenceMul;


    [Space(10)]
    [Header("Burst")]

    [SerializeField] private float fireRate;
    [SerializeField] private int burstCount = 1;
    [SerializeField] private bool instantiateBurstMomentally = false;


    [Space(10)]
    [Header("Reload")]

    public int maxAmmo;
    public float reloadTime;


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
    [SerializeField] private GameObject prefabProjectile;



    [Space(10)]
    [Header("Debug")]

    [HideInInspector] public float recoil;

    [HideInInspector] public bool isReloading;
    [HideInInspector] public int ammo;

    private float prevShootTime = 0;




    private void OnEnable()
    {
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Update()
    {
        #region ������� � ��������, Ammo
        if (!isReloading  &&  Input.GetMouseButton(0))
        {
            // �������� Fire rate
            float currTime = Time.time;
            float betweenShotsTime = 1 / (fireRate / 60);
            if ((currTime - prevShootTime) > betweenShotsTime)
            {
                // ������� � ��������
                if (instantiateBurstMomentally)
                    InstantiateProjectilesMomentally();
                else
                    StartCoroutine("InstantiateProjectiles");

                prevShootTime = currTime;


                // Ammo
                ammo -= 1;

                if (ammo == 0)
                    StartCoroutine(Reload());

                WeaponIndicator.mainInstance.OnShot();
            }
        }
        #endregion

        // ����������� �� R
        if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo)
            StartCoroutine(Reload());
    }

    private void FixedUpdate()
    {
        #region �������� ������
        recoil -= recoilReductionWithTime;

        if (recoil < minWeaponRecoil)
            recoil = minWeaponRecoil;

        if (recoil > maxWeaponRecoil)
            recoil = maxWeaponRecoil;
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

    private void InstantiateProjectile()
    {
        // ���������� ��������� �������
        float totalRecoil = Player.mainPlayer.movementRecoil * movementRecoilImportance + recoil;

        // ���������� ������ ��������
        float zRotationChange = Random.Range(-totalRecoil, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // ���������� �������� � �������� ����
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // ���������� ����
        GameObject instantiatedProjectile = Instantiate(prefabProjectile, shootingPoint.position, randomedRotation);
        instantiatedProjectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectilesSpeed * projSpeedDifferenceMul, 0));
        instantiatedProjectile.GetComponent<PlayerProjectile>().damage *= Player.mainPlayer.damageModifier;

        // �������� ������� ������
        recoil += recoilIncreaseWithShot;
    }
    #endregion


    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        ammo = maxAmmo;

        isReloading = false;

        WeaponIndicator.mainInstance.OnShot();
    }

    


}
