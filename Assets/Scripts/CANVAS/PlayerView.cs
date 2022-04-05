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



    private void Start()
    {
        player = FindObjectOfType<Player>();

        UpdateUI();
    }
    
    public void UpdateUI()
    {
        hpSlider.value = player.Hp / player.maxHp * 100;
        dmgMultiplierText.text = "Damage : x" + player.DamageModifier.ToString("0.0");
        hpText.text = player.Hp.ToString("000") + " / " + player.maxHp.ToString("000");
    }



}
