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

            // ���� �����������
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
        if (!IsGrounded)
            penaltyMul = xHoverMovementPenalty;

        // �������� ����
        float xForce = xMovementForce * (targetSpeed - rb.velocity.x) * penaltyMul;
        rb.AddForce(new Vector3(xForce, 0, 0));
        #endregion

        #region ������ ������
        if (IsGrounded)
        {
            // ���� ������ ������  �  �� ������� �������
            if (Input.GetKey(KeyCode.Space)  &&  !firstJumpWasRecently)
            {
                // ��������� ���� ������
                Vector2 force = new Vector2(0, yJumpForce);

                // ���� ������ ��� � � ������� - ��������� ���� �� �
                if (Input.GetKey(KeyCode.A))
                    force += new Vector2(-xJumpForce, 0);
                if (Input.GetKey(KeyCode.D)) 
                    force += new Vector2( xJumpForce, 0);

                // ���� ������ ��� � ���� - �������� ���� �� jumpShiftBoost
                if (Input.GetKey(KeyCode.LeftShift))
                    force *= jumpShiftBoost;

                // ������� �������� ����
                rb.AddForce(force);

                // �����������, ��� �� ������� ������
                firstJumpWasRecently = true;
                Invoke("SetFalse_firstJumpWasRecently", 0.1f);
            }
        }
        #endregion

        #region ������ ������
        // ���� ����� � �����  �  ���� ������ ������  �  ������� �� ���� ������
        if (!IsGrounded && hasSecondJump && !firstJumpWasRecently)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // ��������� ���� ������
                Vector2 force = new Vector2(0, yJumpForce);

                // ������� �������� ����
                rb.AddForce(force);

                // ������� ������ ������
                hasSecondJump = false;
            }
        }
        #endregion

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
            IsGrounded = true;
        else
            IsGrounded = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contactCount == 0)
            IsGrounded = false;
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

    private void SetFalse_firstJumpWasRecently()
    {
        firstJumpWasRecently = false;
    }

}
