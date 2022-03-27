using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ����������� ����� (��� �� ����� �� ���������, ������ ��� ������ �� ���� FragileEntity � ������� ���)
public abstract class FragileEntity : MonoBehaviour
{
    public float hp;
    private bool isDead = false;

    // ����� ��������� ����� (��� "�����" ����� �����������)
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
