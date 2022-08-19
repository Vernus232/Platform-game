using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text dmgMultiplierText;
    [SerializeField] private Text AtkSpdMultiplierText;
    [SerializeField] private Text ReloadSpdMultiplierText;
    [SerializeField] private Text AccuracyMultiplierText;
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
            AtkSpdMultiplierText.text = "Attack Speed : x" + player.FireRateModifier.ToString("0.0");
            ReloadSpdMultiplierText.text = "Reload Speed : x" + player.ReloadSpeedModifier.ToString("0.0");
            AccuracyMultiplierText.text = "Accuracy : x" + player.AccuracyModifier.ToString("0.0");
            hpText.text = player.Hp.ToString("0") + " / " + player.maxHp.ToString("0");
        }
    }



}
