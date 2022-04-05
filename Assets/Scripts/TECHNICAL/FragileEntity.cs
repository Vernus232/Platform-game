using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{
    public float maxHp;

    [SerializeField] protected float hp;
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;

            OnHpChanged();
        }
    }

    private bool isDead = false;


    //private void Start()
    //{
    //    Hp = maxHp; // не работает, почему?
    //}

    // Метод получения урона (для "наследников" может дополняться)
    public virtual void RecieveDamage(float amount)
    {
        Hp -= amount;

        if (Hp <= 0 && isDead == false)
        {
            Die();
            isDead = true;
        }        
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }


    public virtual void OnHpChanged()
    {

    }
}
