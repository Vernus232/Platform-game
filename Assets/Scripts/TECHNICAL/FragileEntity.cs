using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{

    public float maxHp;
    protected float hp;
    [SerializeField] private AudioSource soundOnDamageReceived;
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

    // Метод получения урона (для "наследников" может дополняться)
    public virtual void RecieveDamage(float amount)
    {
        Hp -= amount;

        if (soundOnDamageReceived != null)
        {
            soundOnDamageReceived.Play();
        }
        

        if (Hp <= 0 && isDead == false)
        {
            Die();
            isDead = true;
        }        
    }
    
    protected virtual void Die()
    {
        GameObject deathParticleSystemGameObject = Instantiate(deathParticleSystemPrefab, transform);
        deathParticleSystemGameObject.GetComponent<ParticleSystem>().Play();
        Debug.Log(deathParticleSystemGameObject);
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
