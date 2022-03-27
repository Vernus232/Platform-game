using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FragileEntity
{
    [SerializeField] private float scoreForKill;

    private ScoreSystem scoreSystem;
    private void Awake()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    protected override void Die()
    {        
        scoreSystem.AddScoreForKill(scoreForKill);
    }
}
