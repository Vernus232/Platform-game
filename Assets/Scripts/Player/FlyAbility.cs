using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAbility : MonoBehaviour
{
    [SerializeField] private float force;

    public float maxCharge;
    public float charge;
    [SerializeField] private float consumptionSpeed;
    [SerializeField] private float passiveRechargeSpeed;
    [SerializeField] private float groundRechargeSpeed;
    [SerializeField] private float secondsToFly;
    [SerializeField] private bool isDelayPassed = false;

    [SerializeField] private ParticleSystem flyParticleSystem;
    [SerializeField] private WeaponView weaponView;

    private Player player;
    private Rigidbody2D playerRb;

    //На старте
    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerRb = player.GetComponent<Rigidbody2D>();

        charge = maxCharge;
    }

    //Обновляется в фпс физики (50 fps)
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && !player.IsGrounded && charge > 0 && isDelayPassed)
        {
            playerRb.velocity += new Vector2(0, force);

            charge -= consumptionSpeed;

            flyParticleSystem.Play();
            
        }

        if (charge < maxCharge && !player.IsGrounded)
            charge += passiveRechargeSpeed;
        if (charge < maxCharge && player.IsGrounded)
            charge += groundRechargeSpeed;

        weaponView.UpdateFlyUi();
    }

    // При прыжке.ююююю
    public void OnJump()
    {
        StopCoroutine(FlyDelay());
        StartCoroutine(FlyDelay());
    }

    // Задержка между прыжком и полётом
    private IEnumerator FlyDelay()
    {
        yield return new WaitForSeconds(secondsToFly);
        isDelayPassed = true;
    }

    public void OnPlayerLanded()
    {
        // Прошла ли задержка??? - Нет!
        isDelayPassed = false;
    }

}
