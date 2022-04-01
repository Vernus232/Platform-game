using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private float refreshTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTimeLength;
    [SerializeField] private int maxCharges;

    [SerializeField] private GameObject trailObj;

    private int charges;
    private bool isReady = true;

    private Collider2D playerCollider;
    private Rigidbody2D playerRb;


    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        playerCollider = player.GetComponent<Collider2D>();
        playerRb = player.GetComponent<Rigidbody2D>();

        charges = maxCharges;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && WeaponChoose.main.currentWeapon.weaponName == "Katana" && isReady)
        {
            ActivateDash();

            charges -= 1;
            if (charges <= 0)
            {
                StartCoroutine(SetReady());
                IEnumerator SetReady()
                {
                    isReady = false;
                    yield return new WaitForSeconds(refreshTime);
                    isReady = true;
                    charges = maxCharges;
                }
            }
        }
    }

    private void ActivateDash()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 dashDirection = (mousePos - playerPos).normalized;

        Player.main.GetComponent<Rigidbody2D>().velocity = dashSpeed * dashDirection;

        StopCoroutine(NoClip());
        StartCoroutine(NoClip());
        IEnumerator NoClip()
        {
            playerCollider.enabled = false;
            float tmpGravScale = playerRb.gravityScale;
            playerRb.gravityScale = 0;
            trailObj.SetActive(true);

            yield return new WaitForSeconds(dashTimeLength);

            playerCollider.enabled = true;
            playerRb.gravityScale = tmpGravScale;
            trailObj.SetActive(false);
            playerRb.velocity = new Vector2(0, 0);
        }
    }


}
