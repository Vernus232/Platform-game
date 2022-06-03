using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbDetector : MonoBehaviour
{
    [SerializeField] private Transform climbTransform;
    [SerializeField] private bool isRightSide;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.main.OnClimbReached(climbTransform.position, isRightSide);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.main.isClimbPossible = false;

        }
    }
}
