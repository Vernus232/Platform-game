using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DragonBones;
using Transform = UnityEngine.Transform;


public class Player : FragileEntity
{

    [Header("Movement")]
    public float xSpeedLimit;
    public float xMovementForce;
    public float xJumpSpeed;
    public float yJumpSpeed;
    [Space(10)]


    [Header("Stats")]
    private float damageModifier = 1;
    public float DamageModifier
    {
        get
        {
            return damageModifier;
        }
        set
        {
            damageModifier = value;

            playerView.UpdateUI();
        }
    }

    private float fireRateModifier = 1;
    public float FireRateModifier
    {
        get
        {
            return fireRateModifier;
        }
        set
        {
            fireRateModifier = value;

            playerView.UpdateUI();
        }
    }
    private float accuracyModifier = 1;
    public float AccuracyModifier
    {
        get
        {
            return accuracyModifier;
        }
        set
        {
            accuracyModifier = value;

            playerView.UpdateUI();
        }
    }
    private float reloadSpeedModifier = 1;
    public float ReloadSpeedModifier
    {
        get
        {
            return reloadSpeedModifier;
        }
        set
        {
            reloadSpeedModifier = value;

            playerView.UpdateUI();

            // ��������
            MeleeWeapon[] meleeWeapons = FindObjectsOfType<MeleeWeapon>(true);
            foreach (MeleeWeapon meleeWeapon in meleeWeapons)
            {
                meleeWeapon.OnReloadSpeedChanged(reloadSpeedModifier);
            }
            
        }
    }
    public float movementRecoil;
    [SerializeField] private float maxMovementRecoil;
    [Space(10)]


    [Header("Refs")]
    [SerializeField] private PhysicsMaterial2D zeroFrictionMat;
    [SerializeField] private PhysicsMaterial2D normFrictionMat;
    [SerializeField] private Transform playerSpritePivotTransform;
    private FlyAbility flyAbility;
    [Space(10)]


    // Inside script
    private bool isGrounded = false;
    [HideInInspector] public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            if (value == true)
            {
                flyAbility.OnPlayerLanded();
            }
            isGrounded = value;
        }
    }

    private DeathscreenView deathscreenView;
    private Rigidbody2D rb;
    private PlayerView playerView;
    private UnityArmatureComponent movementArmatureComponent;
    [HideInInspector] public static Player main;




    //�� ������
    private void Start()
    {
        main = this;
        rb = GetComponent<Rigidbody2D>();
        playerView = FindObjectOfType<PlayerView>();
        deathscreenView = FindObjectOfType<DeathscreenView>(true);
        flyAbility = GetComponent<FlyAbility>();
        movementArmatureComponent = GetComponentInChildren<UnityArmatureComponent>();

        Hp = maxHp;
    }

    //����������� � ��� ������ (50 fps)
    private void FixedUpdate()
    {
        #region ��������� �� �
        // ���������� ������� ��������
        float targetSpeed = 0;

        if (Input.GetKey(KeyCode.A))
            targetSpeed = -xSpeedLimit;

        if (Input.GetKey(KeyCode.D))
            targetSpeed =  xSpeedLimit;

        // �������� ����
        float xForce = xMovementForce * (targetSpeed - rb.velocity.x);
        if (Mathf.Abs(xForce) > 0)
        {
            rb.AddForce(new Vector3(xForce, 0, 0));
            if (!movementArmatureComponent.animation.isPlaying)
            {
                movementArmatureComponent.animation.Play();
            }
            float DIVIDER = 3f;
            movementArmatureComponent.animation.timeScale = rb.velocity.x / DIVIDER;

        }
        else
        {
            movementArmatureComponent.animation.Stop();
        }
        #endregion

        #region ������� �� ��������
        movementRecoil = rb.velocity.magnitude;

        if (movementRecoil > maxMovementRecoil)
            movementRecoil = maxMovementRecoil;
        #endregion

        #region �������
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 normalizedScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= normalizedScreenPos.x;
        mouseScreenPos.y -= normalizedScreenPos.y;
        float angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) >= 90)
        {
                playerSpritePivotTransform.localScale = new Vector3(-Mathf.Abs(playerSpritePivotTransform.localScale.x),
                                                                playerSpritePivotTransform.localScale.y,
                                                                playerSpritePivotTransform.localScale.z);
        }

        if (Mathf.Abs(angle) < 90)
        {
            playerSpritePivotTransform.localScale = new Vector3(Mathf.Abs(playerSpritePivotTransform.localScale.x),
                                                            playerSpritePivotTransform.localScale.y,
                                                            playerSpritePivotTransform.localScale.z);
        }

        #endregion
    }

    //����������� � ��� ���������
    private void Update()
    {
        #region ������
        if (Input.GetKeyDown(KeyCode.Space)  &&  isGrounded)
        {
            int jumpDir = 0;
            if (Input.GetKey(KeyCode.A))
                jumpDir = -1;
            if (Input.GetKey(KeyCode.D))
                jumpDir = 1;

            rb.velocity += new Vector2(xJumpSpeed * jumpDir, 0);  // �� � - ����������
            rb.velocity =  new Vector2(rb.velocity.x, yJumpSpeed);  // �� � - ������������

            flyAbility.OnJump();
        }
        #endregion

        #region �������� ���������
        // ���� ���-�� ���...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // ..�� ����� ��� ������
            gameObject.GetComponent<Collider2D>().sharedMaterial = zeroFrictionMat;
        }
        else
        {
            // ..����� ����������� ������
            gameObject.GetComponent<Collider2D>().sharedMaterial = normFrictionMat;
        }
        #endregion
    }


    #region ������������ �� ���������
    

    public IEnumerator Climb(Vector3 climbPos)
    {
        // ���������� �������� �� �, ����� ����� �������
        float prevVelocityX = rb.velocity.x;
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;

        float EPS = 0.1f;
        float SPEED = 7f;
        while ((climbPos - transform.position).magnitude > EPS)
        {
            Vector3 step = (climbPos - transform.position).normalized * Time.deltaTime * SPEED;

            rb.MovePosition(transform.position + step);

            yield return new WaitForFixedUpdate();
        }
        rb.isKinematic = false;
        rb.velocity = new Vector2(prevVelocityX, 0);
    }
    #endregion

    private void OnDestroy()
    {
        if (deathscreenView.gameObject)
            deathscreenView.gameObject.SetActive(true);
    }

    public override void OnHpChanged()
    {
        base.OnHpChanged();

        playerView.UpdateUI();
    }
}
