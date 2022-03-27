using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ����������� ����� (��� �� ����� �� ���������, ������ ��� ������ �� ���� FragileEntity � ������� ���)
public abstract class FragileEntity : MonoBehaviour
{
    public float hp;

    // ����� ��������� ����� (��� "�����" ����� �����������)
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
