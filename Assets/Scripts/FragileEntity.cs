using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ����������� ����� (��� �� ����� �� ���������, ������ ��� ������ �� ���� FragileEntity � ������� ���)
public abstract class FragileEntity : MonoBehaviour
{
    public float hp;

    // ����������� ����� ��������� ����� (��� ������� �� "�����" ����)
    public abstract void RecieveDamage(float amount);
    
    public void Die()
    {
        Destroy(gameObject);
    }

}
