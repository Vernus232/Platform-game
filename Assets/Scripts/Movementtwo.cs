using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movementtwo : MonoBehaviour // - ������ �PlayerMove� ������ ���� ��� ����� �������
{
    //------- �������/�����, ����������� ��� ������� ���� ---------
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //-v- ��� ��������������� ������������ � ����������, ������� ���������� ������� �GroundCheck�
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- �������/�����, ����������� ������ ���� � ���� ---------
    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
    }

    //------- �������/����� ��� ����������� ��������� �� ����������� ---------
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    //------- �������/����� ��� ��������� ��������� �� ����������� ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- �������/����� ��� ������ ---------
    public int jumpForce = 10;
    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    //------- �������/����� ��� ����������� ����� ---------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    //-----------------------------------------------------------------
}