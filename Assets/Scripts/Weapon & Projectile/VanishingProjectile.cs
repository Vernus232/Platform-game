using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingProjectile : MonoBehaviour
{
    [Tooltip("��� ����� ���� �������� (���).")] [SerializeField] private float lifespan;



    // ������ ������ ������ ����
    private void Awake()
    {
        Destroy(gameObject, lifespan);
    }

    // ������� ���� � �����
    //private void FixedUpdate()
    //{
    //    Vector2 flightDir = GetComponent<Rigidbody2D>().velocity;
    //    float angle = Vector2.Angle(flightDir.normalized, Vector2.left);
    //    transform.rotation = Quaternion.Euler(0f, 0f, angle);
    //}



}
