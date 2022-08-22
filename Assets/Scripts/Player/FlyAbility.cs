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

    [SerializeField] private ParticleSystem flyParticleSystem;
    [SerializeField] private WeaponView weaponView;

    private Player player;
    private Rigidbody2D playerRb;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerRb = player.GetComponent<Rigidbody2D>();

        charge = maxCharge;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)  &&  !player.IsGrounded  &&  charge > 0)
        {
            playerRb.AddForce(player.transform.up * force);

            charge -= consumptionSpeed;

            flyParticleSystem.Play();

        }

        if (charge < maxCharge && !player.IsGrounded)
            charge += passiveRechargeSpeed;
        if (charge < maxCharge && player.IsGrounded)
            charge += groundRechargeSpeed;

        weaponView.UpdateFlyUi();
    }



}
