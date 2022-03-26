using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : FragileEntity
{
    // ������������ ����������� ����� RecieveDamage
    // �.�. ������� ��� ����������� � FragileEntity
    public override void RecieveDamage(float amount)
    {
        hp -= amount;

        if (hp <= 0)
            Die();
    }


}
