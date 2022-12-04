using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FragileEntity
{
    public MobEnum name;
    [SerializeField] private float scoreForKill;


    protected override void Die()
    {        
        ScoreSystem.main.AddScoreForKill(scoreForKill);

        base.Die();
    }
}
