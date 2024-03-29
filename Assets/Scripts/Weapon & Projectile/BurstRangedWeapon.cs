using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRangedWeapon : Weapon
{
    [Header("Speed")]
    [SerializeField] private float projectilesSpeed;
    [Tooltip("Makes speed random in range [speed*(1-x), speed*x].")] [SerializeField] private float projMaxSpeedDifferenceMul;
    [SerializeField] private bool ricochet;
    [Space(10)]

    [Header("Burst")]
    [Tooltip("In bursts-per-minute.")] [SerializeField] private float fireRate;
    public float FireRateMult;
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
    [SerializeField] private float cameraShakeMult = 1;
    [Space(10)]

    [Header("Links")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject prefabProjectile;
    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private AudioSource ReloadAudio;
    [SerializeField] private AnimatedTexture shotAnimation;
    [Space(10)]

    [Header("Debug")]
    public float recoil;
    [HideInInspector] public bool isReloading;
    private int ammo = 1;
    private Transform rukiPivotTransform;
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
    private LerpCamera lerpCamera;


    private void Start()
    {
        shotAnimation = GetComponentInChildren<AnimatedTexture>();
        weaponView = FindObjectOfType<WeaponView>();
        lerpCamera = FindObjectOfType<LerpCamera>();
        rukiPivotTransform = FindObjectOfType<RukiPivot>().transform;

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
        shotAnimation.gameObject.SetActive(false);
        isReloading = false;
    }

    void Update()
    {
        #region ������� � ��������, Ammo
        if (!isReloading  &&  Input.GetMouseButton(0))
        {
            // �������� Fire rate
            float currTime = Time.time;
            float betweenShotsTime = 1 / ((fireRate / 60) * Player.main.FireRateModifier);
            if ((currTime - prevShootTime) > betweenShotsTime)
            {
                // ������� � ��������
                if (instantiateBurstMomentally)
                    InstantiateProjectiles_momentally();
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

        // ����������� �� R
        if (Input.GetKeyDown(KeyCode.R) && Ammo != maxAmmo && isReloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private void FixedUpdate()
    {
        #region Recoil
        float ModifiedRecoilReductionWithTime = recoilReductionWithTime * Player.main.AccuracyModifier;
        recoil -= ModifiedRecoilReductionWithTime;
        
        if (recoil < minWeaponRecoil - Player.main.AccuracyModifier + 1)
            recoil = minWeaponRecoil - Player.main.AccuracyModifier + 1;

        if (recoil > maxWeaponRecoil)
            recoil = maxWeaponRecoil;
        #endregion
    }

    #region ������
    private IEnumerator InstantiateProjectiles_byFrame()
    {
        for (int i = 0; i < projectilesInBurstCount; i++)
        {
            InstantiateProjectile();

            yield return new WaitForEndOfFrame();
        }
        shotAnimation.gameObject.SetActive(true);
        StartCoroutine(shotAnimation.ShotAnimation());
    }

    private void InstantiateProjectiles_momentally()
    {
        for (int i = 0; i < projectilesInBurstCount; i++)
        {
            InstantiateProjectile();
        }
        shotAnimation.gameObject.SetActive(true);
        if (shotAnimation)
            StartCoroutine(shotAnimation.ShotAnimation());
    }

    private void InstantiateProjectile()
    {
        // ���������� ��������� �������
        float totalRecoil = Player.main.movementRecoil * movementRecoilImportance + recoil;

        // ���������� ������ ��������
        float zRotationChange = Random.Range(-totalRecoil, totalRecoil);
        Quaternion randomedRotation = transform.rotation * Quaternion.Euler(Vector3.forward * zRotationChange);

        // ���������� �������� � �������� ����
        float projSpeedDifferenceMul = Random.Range(1f, projMaxSpeedDifferenceMul);

        // ���������� ����
        GameObject instantiatedProjectile = Instantiate(prefabProjectile, shootingPoint.position, randomedRotation);
        Rigidbody2D instantiatedProjectileRb = instantiatedProjectile.GetComponent<Rigidbody2D>();
        instantiatedProjectileRb.AddRelativeForce(new Vector2(projectilesSpeed * projSpeedDifferenceMul, 0));

        // ���������� ��������� ����� � ��������)
        CommonProjectile instantiatedCommonProjectile = instantiatedProjectile.GetComponent<CommonProjectile>();
        if (instantiatedCommonProjectile)
        {
            instantiatedCommonProjectile.damage *= Player.main.DamageModifier;

            if (instantiatedCommonProjectile.penetration > 0)
            {
                instantiatedCommonProjectile.SetParametersOnSpawn(projectilesSpeed * transform.right / 5,
                                                                    instantiatedProjectile.transform.position);
            }
        }


        // �������� ������� ������
        recoil += recoilIncreaseWithShot;

        // ���������
        Vector2 vector2 = transform.right;
        lerpCamera.OnShot(cameraShakeMult, vector2);

        if (shotAudio != null)
        {
            shotAudio.Play();
        }

    }
    #endregion


    public IEnumerator Reload()
    {
        float modifiedReloadTime = reloadTime / Player.main.ReloadSpeedModifier;

        weaponView.OnWeaponReloadStarted(modifiedReloadTime);

        isReloading = true;

        ReloadAudio.Play();

        yield return new WaitForSeconds(modifiedReloadTime);

        Ammo = maxAmmo;
        isReloading = false;
    }

    


}
