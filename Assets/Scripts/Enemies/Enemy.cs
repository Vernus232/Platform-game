using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FragileEntity
{

    [SerializeField] private float scoreForKill;

    private void OnPlayerEnteredHitZone()
    {
        //StartCoroutine()
    }

    private IEnumerator BeforeHitProcess()
    {
        yield return new WaitForSeconds(1f);
    }

    protected override void Die()
    {        
        ScoreSystem.main.AddScoreForKill(scoreForKill);

        base.Die();
    }
}
