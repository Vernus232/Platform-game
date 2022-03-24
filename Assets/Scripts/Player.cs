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
    public float maxGroundedAngle;

    [Space(10)]
    public PhysicsMaterial2D zeroFrictionMat;
    public PhysicsMaterial2D normFrictionMat;

    private Rigidbody2D rb;
    private HealthBar healthBar;

    private float targetSpeed;
    private bool isGrounded = true;




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
        if (!isGrounded)
            penaltyMul = xHoverMovementPenalty;

        // Сообщаем силу
        float xForce = (targetSpeed - rb.velocity.x) * penaltyMul;
        rb.AddForce(new Vector3(xForce, 0, 0));
        #endregion

        if (isGrounded)
        {
            #region Прыжок
            // Если нажали ещё и в сторону - добавляем силу по х
            int xDir = 0;
            if (Input.GetKey(KeyCode.A))
                xDir = -1;
            if (Input.GetKey(KeyCode.D))
                xDir = 1;
            
            // Сообщаем силу по х и у
            if (Input.GetKey(KeyCode.W))
                rb.AddForce(new Vector2(xDir*xJumpForce, yJumpForce));
            #endregion
        }

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
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contactCount == 0)
            isGrounded = false;
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


}
