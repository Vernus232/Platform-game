using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] private Slider slider;



    private void Start()
    {
        SetViewHp(Player.main.maxHp);
    }

    public void SetViewHp(float health)
    {
        slider.value =  health / Player.main.maxHp * 100;
    }
    
    public void UpdateHealth()
    {
        SetViewHp(Player.main.hp);
    }



}
