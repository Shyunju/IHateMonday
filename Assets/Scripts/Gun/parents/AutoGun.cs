using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoGun : Gun
{
    //�з��ϱ� ���ϱ����� �׳� �־����ϴ�
    protected override void Awake()
    {
        base.Awake();
        _gunType = GunType.Auto;
    }
}
