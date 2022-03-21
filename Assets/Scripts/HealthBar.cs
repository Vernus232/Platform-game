using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    
    
    public Slider slider;
    public Player player;
    private float maxHp;


    private void Start()
    {
        SetHealth(player.hp);
        maxHp = player.hp;
    }

    public void SetHealth(float health)
    {
        slider.value =  health / maxHp * 100;
    }
    
    public void UpdateHealth()
    {
        SetHealth(player.hp);
    }
}
