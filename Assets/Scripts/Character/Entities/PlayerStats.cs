using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,        // ���ϱ�
    Multiple,   // ���ϱ�
    Override,   // �����
}

[Serializable]
public class PlayerStats: CharacterStats
{
    public StatsChangeType statsChangeType;         // ���� Ÿ��
    public int shieldCount = 0;                     //  ���� ���尡�� Ƚ��
    public float attackPowerCoefficient = 100f;     // ���� ���ݷ� ���� ���
    public float attackSpeedCoefiicient = 100f;     //  ���� ���ݼӵ� ���� ���
    public float moveSpeedCoefficient = 100f;       // ���� �̵��ӵ� ���� ���
    public bool isInvincible = false;               // ���� ����
    public float invincibilityTime = 2f;            // ���� ���� �ð�
}
