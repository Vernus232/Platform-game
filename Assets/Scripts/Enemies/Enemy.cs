using UnityEngine;

public class Enemy : FragileEntity
{
    public MobEnum Type { get; set; }
    public float Difficulty { get; set; } = 1f;
    [SerializeField] private float scoreForKill;

    protected override void Die()
    {
        ScoreSystem.main.AddScoreForKill(scoreForKill);
        base.Die();
    }
}