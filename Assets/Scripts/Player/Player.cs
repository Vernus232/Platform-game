using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        }
    }
    public float movementRecoil;
    [SerializeField] private float maxMovementRecoil;
    [SerializeField] private int addJumpsMax = 1;
    [Space(10)]


    [Header("Refs")]
    [SerializeField] private PhysicsMaterial2D zeroFrictionMat;
    [SerializeField] private PhysicsMaterial2D normFrictionMat;
    [SerializeField] private Transform playerSpritePivotTransform;
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
            isGrounded = value;

            if (isGrounded)
                addJumpsLeft = addJumpsMax;  // reset jumps left
        }
    }
    [HideInInspector] public bool isClimbPossible = false;
    private int addJumpsLeft;

    private DeathscreenView deathscreenView;
    private Rigidbody2D rb;
    private PlayerView playerView;
    [HideInInspector] public static Player main;




    //�� ������
    private void Start()
    {
        main = this;
        rb = GetComponent<Rigidbody2D>();
        playerView = FindObjectOfType<PlayerView>();
        deathscreenView = FindObjectOfType<DeathscreenView>(true);

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
        rb.AddForce(new Vector3(xForce, 0, 0));
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
        if (Input.GetKeyDown(KeyCode.Space)  &&  addJumpsLeft > 0)
        {
            int jumpDir = 0;
            if (Input.GetKey(KeyCode.A))
                jumpDir = -1;
            if (Input.GetKey(KeyCode.D))
                jumpDir = 1;

            rb.velocity += new Vector2(xJumpSpeed * jumpDir, 0);  // �� � - ����������
            rb.velocity =  new Vector2(rb.velocity.x, yJumpSpeed);  // �� � - ������������

            addJumpsLeft -= 1;
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
    public void OnClimbReached(Vector3 climbPos, bool isRightSide)
    {
        if (Input.GetKey(KeyCode.A)  &&  isRightSide)
            StartCoroutine("Climb", climbPos);

        if (Input.GetKey(KeyCode.D)  &&  !isRightSide)
            StartCoroutine("Climb", climbPos);
    }

    private IEnumerator Climb(Vector3 climbPos)
    {
        // ���������� �������� �� �, ����� ����� �������
        float prevVelocityX = rb.velocity.x;
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;

        float EPS = 0.1f;
        float SPEED = 6f;
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
        if (deathscreenView)
            deathscreenView.gameObject.SetActive(true);
    }

    public override void OnHpChanged()
    {
        base.OnHpChanged();

        playerView.UpdateUI();
    }
}
