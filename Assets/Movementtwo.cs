using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movementtwo : MonoBehaviour // - Вместо «PlayerMove» должно быть имя файла скрипта
{
    //------- Функция/метод, выполняемая при запуске игры ---------
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //-v- Для автоматического присваивания в переменную, радиуса коллайдера объекта «GroundCheck»
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- Функция/метод, выполняемая каждый кадр в игре ---------
    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
    }

    //------- Функция/метод для перемещения персонажа по горизонтали ---------
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    //------- Функция/метод для отражения персонажа по горизонтали ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- Функция/метод для прыжка ---------
    public int jumpForce = 10;
    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    //------- Функция/метод для обнаружения земли ---------
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