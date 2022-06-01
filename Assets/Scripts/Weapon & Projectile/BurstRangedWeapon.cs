using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRangedWeapon : Weapon
{
    [Header("Speed")]

    [SerializeField] private float projectilesSpeed;
    [Tooltip("Makes speed random in range [speed*(1-x), speed*x].")] [SerializeField] private float projMaxSpeedDifferenceMul;
    

    [Space(10)]
    [Header("Burst")]

    [Tooltip("In bursts-per-minute.")] [SerializeField] private float fireRate;
    [SerializeField] private int projectilesInBurstCount = 1;
    [Tooltip("Whether to .")] [SerializeField] private bool instantiateBurstMomentally = true;


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

    public float recoil;
    [HideInInspector] public bool isReloading;
    private int ammo = 1;
    [HideInInspector] public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;

            weaponView.OnAmmoChanged();
        }
    }
    private float prevShootTime = 0;
    private WeaponView weaponView;



    private void Start()
    {
        weaponView = FindObjectOfType<WeaponView>();

        Ammo = maxAmmo;
    }


    private void OnEnable()
    {
        if (Ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        isReloading = false;
    }

    void Update()
    {
        #region Выстрел и задержка, Ammo
        if (!isReloading  &&  Input.GetMouseButton(0))
        {
            // Проверка Fire rate
            float currTime = Time.time;
            float betweenShotsTime = 1 / (fireRate / 60);
            if ((currTime - prevShootTime) > betweenShotsTime)
            {
                // Выстрел и задержка
                if (instantiateBurstMomentally)
                    InstantiateProjectilesMomentally();
                else
                    StartCoroutine("InstantiateProjectiles");

                prevShootTime = currTime;


                // Ammo
                Ammo -= 1;

                if (Ammo == 0)
                    StartCoroutine(Reload());

                WeaponView.main.UpdateUI();
            }
        }
        #endregion

        // Перезарядка на R
        if (Input.GetKeyDown(KeyCode.R) && Ammo != maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private void FixedUpdate()
    {
        #region Снижение отдачи
        recoil -= recoilReductionWithTime;

        if (recoil < minWeaponRecoil)
            recoil = minWeaponRecoil;

        if (recoil > maxWeaponRecoil)
            recoil = maxWeaponRecoil;
        #endregion
    }

    #region Бёрсты
    private IEnumerator InstantiateProjectiles()
    {
        for (int i = 0; i < projectilesInBurstCount; i++)
        {
            InstantiateProjectile();

            yield return new WaitForEndOfFrame();
        }
    }

    private void InstantiateProjectilesMomentally()
    {
        for (int i = 0; i < projectilesInBurstCount; i++)
        {
            InstantiateProjectile();
        }
    }

    private void InstantiateProjectile()
    {
        // Определили суммарный разброс
        float totalRecoil = Player.main.movementRecoil * movementRecoilImportance + recoil;

        // Определили вектор выстрела
        float zRotationChange = Random.Range(-totalRecoil, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // Определили разность в скорости пуль
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // Заспавнили пули
        GameObject instantiatedProjectile = Instantiate(prefabProjectile, shootingPoint.position, randomedRotation);
        Rigidbody2D instantiatedProjectileRb = instantiatedProjectile.GetComponent<Rigidbody2D>();
        PlayerProjectile instantiatedPlayerProjectile = instantiatedProjectile.GetComponent<PlayerProjectile>();
        
        instantiatedProjectileRb.AddRelativeForce(new Vector2(projectilesSpeed * projSpeedDifferenceMul, 0));
        instantiatedPlayerProjectile.damage *= Player.main.DamageModifier;

        if (instantiatedPlayerProjectile.penetration > 0)
        {
            Debug.Log(projectilesSpeed * transform.right / 5);
            instantiatedPlayerProjectile.SetParametersOnSpawn(  projectilesSpeed * transform.right /5,
                                                                instantiatedProjectile.transform.position);
        }


        // Сообщили разброс оружию
        recoil += recoilIncreaseWithShot;
    }
    #endregion


    public IEnumerator Reload()
    {
        weaponView.OnWeaponReloadStarted(reloadTime);

        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        Ammo = maxAmmo;
        isReloading = false;
    }

    


}
