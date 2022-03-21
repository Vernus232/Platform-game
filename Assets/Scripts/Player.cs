using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FragileEntity
{
    public HealthBar healthBar;
    public float xSpeedLimit;
    public float xMovementForce;
    public float xHoverMovementPenalty;

    [Space(10)]
    public float xJumpForce;
    public float yJumpForce;

    private Rigidbody2D rb;
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
            targetSpeed = -xSpeedLimit;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetSpeed = xSpeedLimit;
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
    }

    // Проверки на касание земли
    private void OnCollisionStay2D(Collision2D collision)
    {        
        foreach (ContactPoint2D contact in collision.contacts)
        {
            float angle = Vector2.Angle(contact.normal, Vector2.up);
            float maxGroundedAngle = 20;
            if (angle < maxGroundedAngle)
                isGrounded = true;
            else
                isGrounded = false;
        }
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
