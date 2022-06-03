using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] [Range(1,100)] private int healthAddupPercent;


    protected override bool TryDoActionOnPlayer()
    {

        if (Player.main.Hp == Player.main.maxHp)
        {
            return false;
        }

        if (Player.main.Hp < Player.main.maxHp)
        {
            Player.main.Hp += ((float) healthAddupPercent / 100) * Player.main.maxHp;
            if (Player.main.Hp > Player.main.maxHp)
            {
                Player.main.Hp = Player.main.maxHp;
            }
            PlayerView.main.UpdateUI();
            return true;
        }
        
        return false;
    }
}
