using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Player player;


    private void OnTriggerStay2D(Collider2D collision)
    {
        player.IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.IsGrounded = false;
    }

}
