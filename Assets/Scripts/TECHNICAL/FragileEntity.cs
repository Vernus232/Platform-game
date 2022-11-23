using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ����������� ����� (��� �� ����� �� ���������, ������ ��� ������ �� ���� FragileEntity � ������� ���)
public abstract class FragileEntity : MonoBehaviour
{
    private DamagePopup damagePopup;
    public float maxHp;
    protected float hp;
    public float Hp
    
    {
        get => hp;
        set
        {
            hp = value;
            OnHpChanged();
        }
    }

    [Header("Fragile Entity Refs")]
    [SerializeField] private GameObject damageParticleSystemPrefab;
    [SerializeField] private GameObject deathParticleSystemPrefab;

    private bool isDead = false;


    private void Start()
    {
        Hp = maxHp;
        damagePopup = FindObjectOfType<DamagePopup>(true);
    }


    private void Try_SpawnParticleSystemWithTimer(GameObject particleSystem_predab)
    {
        if (particleSystem_predab)
            SpawnParticleSystemWithTimer(particleSystem_predab);
    }

    private void SpawnParticleSystemWithTimer(GameObject particleSystem_predab)
    {
        Vector2 pos2 = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject particleSystem_gameObject = Instantiate(particleSystem_predab, pos2, transform.rotation);
        Destroy(particleSystem_gameObject, 3);
    }

    // ����� ��������� ����� (��� "�����������" ����� �����������)
    public virtual void RecieveDamage(float amount)
    {
        Try_SpawnParticleSystemWithTimer(damageParticleSystemPrefab);

        damagePopup.CreateDamageText(transform, amount);

        Hp -= amount;        

        if (Hp <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }        
    }
    
    protected virtual void Die()
    {
        Try_SpawnParticleSystemWithTimer(deathParticleSystemPrefab);

        Destroy(gameObject);
    }

    public virtual void OnHpChanged()
    {

    }
    public virtual void OnMaxHpChanged()
    {
        PlayerView.main.UpdateUI();
    }


}
