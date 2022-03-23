using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_kinematic : FragileEntity
{
    public float xSpeed;
    public float xHoverMovementPenalty;

    [Space(10)]
    public float xJumpSpeed;
    public float yJumpSpeed;
    public float maxGroundedAngle;

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
        #region �������� �� �
        // ���������� ������� ��������        
        if (Input.GetKey(KeyCode.A))
        {
            targetSpeed = -xSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetSpeed = xSpeed;
        }
        else
        {
            targetSpeed = 0;
        }

        // ��������� ����� �������� � �����
        float penaltyMul = 1;
        if (!isGrounded)
            penaltyMul = xHoverMovementPenalty;

        // �������� ��������
        rb.velocity = new Vector2(targetSpeed * penaltyMul, rb.velocity.y);
        #endregion

        if (isGrounded)
        {
            #region ������
            // ���� ������ ��� � � ������� - ��������� ���� �� �
            int xDir = 0;
            if (Input.GetKey(KeyCode.A))
                xDir = -1;
            if (Input.GetKey(KeyCode.D))
                xDir = 1;
            
            // �������� ���� �� � � �
            if (Input.GetKey(KeyCode.W))
                rb.velocity = new Vector2(xDir*xJumpSpeed, yJumpSpeed);
            #endregion
        }
    }

    // �������� �� ������� �����
    private void OnCollisionStay2D(Collision2D collision)
    {
        // ����� ��������� �� ������� ����� 
        int groundContacts = 0;
        // �������� �� ���� ���������
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // ������� ���� ����� ������������ ���� � ������������ ��������
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

    //��� ��������� �����...
    // ������������ ����������� ����� RecieveDamage
    // �.�. ������� ��� ����������� � FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp == 0)
            Die();

        healthBar.UpdateHealth();
    }


}