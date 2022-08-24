using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbDetector : MonoBehaviour
{
    [SerializeField] private Transform climbTransform;
    [SerializeField] private bool isRightSide;
    private static float BETWEEN_CLIMBS_TIME = 0.7f;

    private bool isActive = true;


    private IEnumerator Cooldown()
    {
        isActive = false;
        yield return new WaitForSeconds(BETWEEN_CLIMBS_TIME);
        isActive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && (Input.GetKey(KeyCode.A) && isRightSide || Input.GetKey(KeyCode.D) && !isRightSide))
        {
            StartCoroutine(Player.main.Climb(climbTransform.position));
            StartCoroutine(Cooldown());
        }
    }

}
