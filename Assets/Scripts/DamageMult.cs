using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMult : MonoBehaviour
{
    public Text text;
    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateUI();
    }

    public void OnDamageModifierChanged()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        text.text = "Damage : x" + player.DamageModifier.ToString("0.0");
    }
}
