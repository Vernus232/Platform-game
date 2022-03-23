using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Text text;

    private Rigidbody2D rb;


    private void Start()
    {
        GameObject playerObj = FindObjectOfType<Player>().gameObject;
        rb = playerObj.GetComponent<Rigidbody2D>();
    }
    void Update()
    {        
        Vector2 v2Velocity = rb.velocity;
        float speed = rb.velocity.magnitude;
        text.text  = "Speed " + speed.ToString("00");        
    }
}
