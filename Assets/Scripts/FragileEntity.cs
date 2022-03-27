using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{
    public float hp;

    // Метод получения урона (для "детей" может дополняться)
    public virtual void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0)
            Die();
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
