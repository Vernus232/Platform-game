using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedView : MonoBehaviour
{
    [SerializeField] private Text text;

    private Rigidbody2D playerRb;




    private void Start()
    {
        playerRb = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Vector2 vel = playerRb.velocity;
        text.text = "Speed  X: " + Mathf.Abs(vel.x).ToString("00") + " Y: " + Mathf.Abs(vel.y).ToString("00");
    }



}
