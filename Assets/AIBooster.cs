using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIBooster : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float normSpeed;
    [SerializeField] private float speedChangeSensivity;

    [SerializeField] private Transform pivot;

    private Vector2 prevPos;
    private AIPath path;



    private void Start()
    {
        path = gameObject.GetComponent<AIPath>();
    }

    private void FixedUpdate()
    {
        Vector2 currPos = transform.position;
        Vector2 vel = (currPos - prevPos) *100;


        if (vel.x > 0)
        {
            pivot.localScale = new Vector2(1, 1);
        }
        if (vel.x < 0)
        {
            pivot.localScale = new Vector2(-1, 1);
        }


        if (vel.y > speedChangeSensivity)
        {
            path.maxSpeed = jumpSpeed;
        }
        if (Mathf.Abs(vel.y) <= speedChangeSensivity)
        {
            path.maxSpeed = normSpeed;
        }
        if (vel.y < -speedChangeSensivity)
        {
            path.maxSpeed = fallSpeed;
        }

        prevPos = currPos;
    }

}
