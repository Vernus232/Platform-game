using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{

    public float maxHp;
    protected float hp;
    [SerializeField] private GameObject damageParticleSystemPrefab;
    [SerializeField] private GameObject deathParticleSystemPrefab;
    public float Hp
    
    {
        get => hp;
        set
        {
            hp = value;
            OnHpChanged();
        }
    }

    private bool isDead = false;


    private void Start()
    {
        Hp = maxHp;
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

    // Метод получения урона (для "наследников" может дополняться)
    public virtual void RecieveDamage(float amount)
    {
        Try_SpawnParticleSystemWithTimer(damageParticleSystemPrefab);

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
