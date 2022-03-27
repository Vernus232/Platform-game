using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FragileEntity
{
    public ScoreSystem score;
    public float scoreForKill;
    // Переписываем абстрактный метод RecieveDamage
    // Т.к. обещали его реализовать в FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0)
            Die();
    }
    private void OnDestroy()
    {
        Debug.Log("lolo");
        score.AddScoreForKill();
    }
}
