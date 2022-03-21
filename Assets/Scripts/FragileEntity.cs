using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileEntity : MonoBehaviour
{
    public float hp;


    public void RecieveDamage(float amount)
    {
        hp = hp - amount;

        if (hp == 0)
            Die();

    }

    public void Die()
    {
        Destroy(gameObject);
    }



}
