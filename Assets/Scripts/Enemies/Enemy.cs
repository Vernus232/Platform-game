using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FragileEntity
{
    public MobEnum type;
    public float difficulty = 1;
    [SerializeField] private float scoreForKill;


    protected override void Die()
    {        
        ScoreSystem.main.AddScoreForKill(scoreForKill);

        base.Die();
    }
}
