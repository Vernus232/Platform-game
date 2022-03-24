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
        #region �������� �� �
        // ���������� ������� ��������        
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

        // ��������� ����� �������� � �����
        float penaltyMul = 1;
        if (!isGrounded)
            penaltyMul = xHoverMovementPenalty;

        // �������� ����
        float xForce = (targetSpeed - rb.velocity.x) * penaltyMul;
        rb.AddForce(new Vector3(xForce, 0, 0));
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
                rb.AddForce(new Vector2(xDir*xJumpForce, yJumpForce));
            #endregion
        }

        #region �������� ���������
        float Y_FRICTION_SPEED = 3f;
        // ���� �������� �� � ������..
        if (Mathf.Abs(rb.velocity.y) < Y_FRICTION_SPEED)
        {
            // ..�� ����� ����������� ������
            gameObject.GetComponent<Collider2D>().sharedMaterial = normFrictionMat;
        }
        else
        {
            // ..����� ��� ������
            gameObject.GetComponent<Collider2D>().sharedMaterial = zeroFrictionMat;
        }
        #endregion

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
