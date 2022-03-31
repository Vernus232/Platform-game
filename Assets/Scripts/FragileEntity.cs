using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{
    public float maxHp;

    [HideInInspector] public float hp;

    private bool isDead = false;


    private void Start()
    {
        hp = maxHp;
    }

    // Метод получения урона (для "наследников" может дополняться)
    public virtual void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0 && isDead == false)
        {
            Die();
            isDead = true;
        }        
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
