using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterStats
{    
    public int currentHp = 5;                       // ���� ü��
    public int currentMaxHp = 5;                    // ���� �ִ� ���� ü��
    [Range(1f, 20f)] public float moveSpeed = 5f;                    // �̵��ӵ�
}
