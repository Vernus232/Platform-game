using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text dmgMultiplierText;
    [SerializeField] private Text hpText;

    private Player player;
    [HideInInspector] public static PlayerView main;



    private void Start()
    {
        main = this;
        player = FindObjectOfType<Player>();

        UpdateUI();
    }
    
    public void UpdateUI()
    {
        if (player)
        {
            hpSlider.value = player.Hp / player.maxHp * 100;
            dmgMultiplierText.text = "Damage : x" + player.DamageModifier.ToString("0.0");
            hpText.text = player.Hp.ToString("0") + " / " + player.maxHp.ToString("0");
        }
    }



}
