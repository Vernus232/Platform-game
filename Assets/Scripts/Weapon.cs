using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Постоянные параметры
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
    [SerializeField] private GameObject prefabProjectile;
    private WeaponIndicator weaponIndicator;

    public float recoil;

    public bool isReloading;
    public int ammo;
    public int maxAmmo;
    public float reloadTime;

    private float prevShootTime = 0;

    private Player player;



    private void Start()
    {
        player = FindObjectOfType<Player>();
        weaponIndicator = FindObjectOfType<WeaponIndicator>();
    }

    private void OnEnable()
    {
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Update()
    {
        #region Выстрел и задержка, Ammo
        if (!isReloading  &&  Input.GetMouseButton(0))
        {
            // Проверка Fire rate
            float currTime = Time.time;
            if ((currTime - prevShootTime) > FireRate)
            {
                // Выстрел и задержка
                if (instantiateBurstMomentally)
                    InstantiateProjectilesMomentally();
                else
                    StartCoroutine("InstantiateProjectiles");

                prevShootTime = currTime;


                // Ammo
                ammo -= 1;

                if (ammo == 0)
                    StartCoroutine(Reload());

                weaponIndicator.OnShot();
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo)
            StartCoroutine(Reload());
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


        //Debug.Log("======");
        //Debug.Log("weapon recoil " + recoil.ToString());
        //Debug.Log("player recoil " + player.recoilFromMovement.ToString());
        //Debug.Log("======");

    }

    #region Бёрсты
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


    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        ammo = maxAmmo;

        isReloading = false;

        weaponIndicator.OnShot();
    }

    private void InstantiateProjectile()
    {
        // Определили суммарный разброс
        float totalRecoil = player.movementRecoil * movementRecoilImportance  +  recoil;

        // Определили вектор выстрела
        float zRotationChange = Random.Range(-totalRecoil, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // Определили разность в скорости пуль
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // Заспавнили пули
        GameObject instantiatedProjectile = Instantiate(prefabProjectile, shootingPoint.position, randomedRotation);
        instantiatedProjectile.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectilesSpeed * projSpeedDifferenceMul, 0));
        instantiatedProjectile.GetComponent<PlayerProjectile>().damage *= player.damageModifier;

        // Сообщили разброс оружию
        recoil += recoilIncreaseWithShot;

    }


}
