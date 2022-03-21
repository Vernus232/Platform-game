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
    public new void RecieveDamage(float amount)
    {

        //Обновляем хп Entity
        hp = hp - amount;

        //При хп=0, умираем
        if (hp == 0)
            Die();

        //Обновляем хпбар, если игрок
        healthBar.UpdateHealth();

    }

}
