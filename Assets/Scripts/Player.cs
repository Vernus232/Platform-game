using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FragileEntity
{
    [Header("Movement")]

    public float xSpeedLimit;
    public float xMovementForce;
    public float xJumpForce;
    public float yJumpSpeed;

    [Space(10)]


    [Header("Stats")]

    public float damageModifier = 1;
    public float recoilFromMovement;

    [SerializeField] private int addJumpsMax = 1;

    [Space(10)]


    [Header("Refs")]

    [SerializeField] private PhysicsMaterial2D zeroFrictionMat;
    [SerializeField] private PhysicsMaterial2D normFrictionMat;

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

    [HideInInspector] public bool IsClimbPossible = false;

    private int addJumpsLeft;
    

    private Rigidbody2D rb;
    private HealthBar healthBar;






    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void FixedUpdate()
    {
        #region Ускорение по Х
        // Определяем целевую скорость
        float targetSpeed = 0;

        if (Input.GetKey(KeyCode.A))
            targetSpeed = -xSpeedLimit;

        if (Input.GetKey(KeyCode.D))
            targetSpeed =  xSpeedLimit;

        // Сообщаем силу
        float xForce = xMovementForce * (targetSpeed - rb.velocity.x);
        rb.AddForce(new Vector3(xForce, 0, 0));
        #endregion

        #region Разброс от скорости
        recoilFromMovement = rb.velocity.magnitude;
        #endregion

    }

    private void Update()
    {
        #region Прыжок
        if (Input.GetKeyDown(KeyCode.Space)  &&  addJumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, yJumpSpeed);

            addJumpsLeft -= 1;
        }
        #endregion

        #region Контроль материала
        // Если что-то жмём...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // ..то юзаем нет трение
            gameObject.GetComponent<Collider2D>().sharedMaterial = zeroFrictionMat;
        }
        else
        {
            // ..иначе стандартное трения
            gameObject.GetComponent<Collider2D>().sharedMaterial = normFrictionMat;
        }
        #endregion
    }


    #region Подтягивание на платформу
    public void OnClimbReached(Vector3 climbPos, bool isRightSide)
    {
        if (Input.GetKey(KeyCode.A)  &&  isRightSide)
            StartCoroutine("Climb", climbPos);

        if (Input.GetKey(KeyCode.D)  &&  !isRightSide)
            StartCoroutine("Climb", climbPos);
    }

    private IEnumerator Climb(Vector3 climbPos)
    {
        // Запоминаем скорость по х, чтобы потом вернуть
        float prevVelocityX = rb.velocity.x;
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;

        float EPS = 0.1f;
        float SPEED = 10;
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


    // При получении урона...
    // Переписываем абстрактный метод RecieveDamage
    // Т.к. обещали его реализовать в FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0)
            Die();

        healthBar.UpdateHealth();
    }


}
