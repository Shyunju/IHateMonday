using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoGun : Gun
{
    //�з��ϱ� ���ϱ����� �׳� �־����ϴ�
    protected virtual void Awake()
    {
        _gunType = GunType.Auto;
    }
}
