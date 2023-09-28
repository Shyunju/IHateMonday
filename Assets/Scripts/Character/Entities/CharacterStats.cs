using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatsChangeType
{
    Add,        // ���ϱ�
    Multiple,   // ���ϱ�
    Override,   // �����
}

[Serializable]
public class CharacterStats
{    
    public StatsChangeType statsChangeType;         // ���� Ÿ��
    public int currentHp = 5;                       // ���� ü��
    public int currentMaxHp = 5;                    // ���� �ִ� ���� ü��
    public int shieldCount = 0;                     //  ���� ���尡�� Ƚ��
    public float attackPowerCoefficient = 100f;     // ���� ���ݷ� ���� ���
    public float attackSpeedCoefiicient = 100f;     //  ���� ���ݼӵ� ���� ���
    [Range(1f, 20f)] public float moveSpeed = 5f;                    // �̵��ӵ�
    public float moveSpeedCoefficient = 100f;       // ���� �̵��ӵ� ���� ���
    public bool isInvincible = false;               // ���� ����
    public float invincibilityTime = 2f;            // ���� ���� �ð�

    //public int GetCurrentHp()
    //{
    //    return currentHp;
    //}
    //public void SetCurrentHp(int change)
    //{
    //    currentHp += change;
    //}

    //public int GetCurrentMaxHp()
    //{
    //    return currentMaxHp;
    //}
    //public void SetCurrentMaxHp(int change)
    //{
    //    currentMaxHp += change;
    //}

    //public int GetShieldCount()
    //{
    //    return shieldCount;
    //}
    //public void SetShieldCount(int change)
    //{
    //    shieldCount += change;
    //}

    //public float GetAttackPowerCoefficient()
    //{
    //    return attackPowerCoefficient;
    //}
    //public void SetAttackPowerCoefficient(float change)
    //{
    //    attackPowerCoefficient += change;
    //}
    //public float GetAttackSpeedCoefiicient()
    //{
    //    return attackSpeedCoefiicient;
    //}
    //public void SetAttackSpeedCoefiicient(float change)
    //{
    //    attackPowerCoefficient += change;
    //}

    //public float GetMoveSpeed()
    //{
    //    return moveSpeed;
    //}
    //public void SetMoveSpeed(float change)
    //{
    //    moveSpeed += change;
    //}

    //public float GetMoveSpeedCoefficient()
    //{
    //    return moveSpeedCoefficient;
    //}
    //public void SetMoveSpeedCoefficient(float change)
    //{
    //    moveSpeedCoefficient += change;
    //}

    //public bool GetIsInvincible()
    //{
    //    return isInvincible;
    //}
    //public void SetIsInvincible()
    //{

    //}

    //public float GetInvincibilityTime()
    //{
    //    return invincibilityTime;
    //}
    //public void SetInvincibilityTime()
    //{

    //}
}
