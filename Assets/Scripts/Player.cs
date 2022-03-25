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
    public float yJumpForce;

    [Space(10)]


    [Header("Stats")]

    public float damageModifier = 1;
    public int jumpsMax = 2;

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
                jumpsLeft = jumpsMax;  // reset jumps left
        }
    }

    [HideInInspector] public bool IsClimbPossible = false;

    private int jumpsLeft;

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
    }

    private void Update()
    {
        #region Прыжок
        if (jumpsLeft > 0  &&  Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, yJumpForce));

            jumpsLeft -= 1;
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


    #region Подтягивание от стены
    public void OnClimbReached(Vector3 climbPos, bool isRightSide)
    {
        if (Input.GetKey(KeyCode.A)  &&  isRightSide)
            Climb(climbPos);

        if (Input.GetKey(KeyCode.D)  &&  !isRightSide)
            Climb(climbPos);
    }

    private void Climb(Vector3 climbPos)
    {
        transform.position = climbPos;
    }

    //public IEnumerator SmoothMovement(Vector2 end)
    //{
    //    float MOVE_TIME = 1f;

    //    yield return new WaitForFixedUpdate();

    //    rb.isKinematic = true;

    //    while (rb.position != end)
    //    {
    //        rb.MovePosition(rb.position, end, 1 / MOVE_TIME);

    //        yield return new WaitForFixedUpdate();
    //    }

    //    rb.position = end;
    //    rb.isKinematic = false;
    //}
    #endregion


    // При получении урона...
    // Переписываем абстрактный метод RecieveDamage
    // Т.к. обещали его реализовать в FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp == 0)
            Die();

        healthBar.UpdateHealth();
    }


}
