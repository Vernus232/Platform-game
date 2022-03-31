using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private Player player;



    private void Start()
    {
        player = FindObjectOfType<Player>();

        SetViewHp(player.maxHp);
    }

    public void SetViewHp(float health)
    {
        slider.value =  health / player.maxHp * 100;
    }
    
    public void UpdateHealth()
    {
        SetViewHp(player.hp);
    }



}
