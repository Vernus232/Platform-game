using UnityEngine;

public abstract class FragileEntity : BaseEntity
{
    public float MaxHp { get; set; }
    protected float hp;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            OnHpChanged();
            if (Hp <= 0 && !IsDead)
            {
                Die();
                IsDead = true;
            }
        }
    }

    [SerializeField] private GameObject damageParticleSystemPrefab;
    [SerializeField] private GameObject deathParticleSystemPrefab;

    private bool IsDead { get; set; }

    protected virtual void Start()
    {
        Hp = MaxHp;
    }

    public virtual void ReceiveDamage(float amount)
    {
        SpawnWithTimer(damageParticleSystemPrefab, transform.position, transform.rotation);

        // Assuming GameOptions.damagePopupsOn is defined elsewhere in your codebase.
        if (GameOptions.damagePopupsOn)
        {
            PopupView.main.Try_CreateDamagePopup(this, amount);
        }

        Hp -= amount;
    }

    protected virtual void Die()
    {
        SpawnWithTimer(deathParticleSystemPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    protected virtual void OnHpChanged() { }
}