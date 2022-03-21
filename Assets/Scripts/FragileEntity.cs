using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Это абстрактный класс (его на сцену не поставишь, потому что самого по себе FragileEntity в природе нет)
public abstract class FragileEntity : MonoBehaviour
{
    public float hp;

    // Абстрактный метод получения урона (для каждого из "детей" свой)
    public abstract void RecieveDamage(float amount);
    
    public void Die()
    {
        Destroy(gameObject);
    }

}
