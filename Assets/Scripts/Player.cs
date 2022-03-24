using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FragileEntity
{
    public float xWalkSpeedLimit;
    public float xRunSpeedLimit;
    public float xMovementForce;
    public float xHoverMovementPenalty;

    [Space(10)]

    public float xJumpForce;
    public float yJumpForce;
    [Range(1, 2)] public float jumpShiftBoost;
    public float maxGroundedAngle;

    [Space(10)]

    public PhysicsMaterial2D zeroFrictionMat;
    public PhysicsMaterial2D normFrictionMat;

    private Rigidbody2D rb;
    private HealthBar healthBar;

    private float targetSpeed;
    private bool firstJumpWasRecently = false;
    private bool isGrounded = true;
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            bool wasGrounded = isGrounded;
            isGrounded = value;

            // Если приземлился
            if (!wasGrounded && isGrounded)
            {
                hasSecondJump = true;
            }
        }
    }

    private bool hasSecondJump = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void FixedUpdate()
    {
        #region Движение по Х
        // Определяем целевую скорость        
        if (Input.GetKey(KeyCode.A))
        {
            targetSpeed = -xWalkSpeedLimit;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetSpeed = -xRunSpeedLimit;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetSpeed = xWalkSpeedLimit;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetSpeed = xRunSpeedLimit;
            }
        }

        else
        {
            targetSpeed = 0;
        }

        // Множитель нерфа движения в полёте
        float penaltyMul = 1;
        if (!IsGrounded)
            penaltyMul = xHoverMovementPenalty;

        // Сообщаем силу
        float xForce = xMovementForce * (targetSpeed - rb.velocity.x) * penaltyMul;
        rb.AddForce(new Vector3(xForce, 0, 0));
        #endregion

        #region Первый прыжок
        if (IsGrounded)
        {
            // Если нажали пробел  И  не прыгали недавно
            if (Input.GetKey(KeyCode.Space)  &&  !firstJumpWasRecently)
            {
                // Дефолтная сила прыжка
                Vector2 force = new Vector2(0, yJumpForce);

                // Если нажали ещё и в сторону - добавляем силу по х
                if (Input.GetKey(KeyCode.A))
                    force += new Vector2(-xJumpForce, 0);
                if (Input.GetKey(KeyCode.D)) 
                    force += new Vector2( xJumpForce, 0);

                // Если нажали ещё и шифт - умножаем силу на jumpShiftBoost
                if (Input.GetKey(KeyCode.LeftShift))
                    force *= jumpShiftBoost;

                // Наконец сообщаем силу
                rb.AddForce(force);

                // Отслеживаем, был ли недавно прыжок
                firstJumpWasRecently = true;
                Invoke("SetFalse_firstJumpWasRecently", 0.1f);
            }
        }
        #endregion

        #region Второй прыжок
        // Если плеер в полёте  И  есть второй прыжок  И  недавно не было прыжка
        if (!IsGrounded && hasSecondJump && !firstJumpWasRecently)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Дефолтная сила прыжка
                Vector2 force = new Vector2(0, yJumpForce);

                // Наконец сообщаем силу
                rb.AddForce(force);

                // Ресетим второй прыжок
                hasSecondJump = false;
            }
        }
        #endregion

        #region Контроль материала
        float Y_FRICTION_SPEED = 3f;
        // Если скорость по У низкая..
        if (Mathf.Abs(rb.velocity.y) < Y_FRICTION_SPEED)
        {
            // ..то юзаем стандартное трение
            gameObject.GetComponent<Collider2D>().sharedMaterial = normFrictionMat;
        }
        else
        {
            // ..иначе нет трения
            gameObject.GetComponent<Collider2D>().sharedMaterial = zeroFrictionMat;
        }
        #endregion
    }

    // Проверки на касание земли
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Число контактов со стороны земли 
        int groundContacts = 0;
        // Итерация по всем контактам
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Считаем угол между вертикальной осью и направлением контакта
            float angle = Vector2.Angle(contact.normal, Vector2.up);
            if (angle < maxGroundedAngle)
                groundContacts += 1;
        }

        if (groundContacts > 0)
            IsGrounded = true;
        else
            IsGrounded = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contactCount == 0)
            IsGrounded = false;
    }

    //При получении урона...
    // Переписываем абстрактный метод RecieveDamage
    // Т.к. обещали его реализовать в FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp == 0)
            Die();

        healthBar.UpdateHealth();
    }

    private void SetFalse_firstJumpWasRecently()
    {
        firstJumpWasRecently = false;
    }

}
