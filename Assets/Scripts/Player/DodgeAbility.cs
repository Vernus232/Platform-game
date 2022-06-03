using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAbility : MonoBehaviour
{
    [SerializeField] private float refreshTime;
    [SerializeField] private float dodgeTime;

    private bool isReady = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isReady)
        {
            ActivateDodge();

            isReady = false;
            StartCoroutine(SetReady());
            IEnumerator SetReady()
            {
                yield return new WaitForSeconds(refreshTime);
                isReady = true;
            }
        }
    }

    private void ActivateDodge()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        StartCoroutine(CancellIgnoreLayerCollision());
        IEnumerator CancellIgnoreLayerCollision()
        {
            yield return new WaitForSeconds(dodgeTime);
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }
    }
}
