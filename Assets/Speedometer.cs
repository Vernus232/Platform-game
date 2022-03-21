using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Text text;
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        Vector2 v2Velocity = rb.velocity;
        float speed = rb.velocity.magnitude;
        text.text  = "Speed " + speed.ToString("00");
        
    }
}
