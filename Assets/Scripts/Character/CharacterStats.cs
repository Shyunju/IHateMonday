using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//TODO
//���ѵ� ���� ���� �ʵ��� set�Լ� ���� ����

[Serializable]
public class CharacterStats
{
    [SerializeField] private int MAX_HP = 8;  //�ִ� ���� ü��
    [SerializeField] private const int MAX_SHIELD_COUNT = 2; //�ִ� ���� ����
    [SerializeField] private const float MAX_ATTACK_POWER = 200f;  //�ִ� ���ݷ� ���� ���
    [SerializeField] private const float MAX_ATTACK_SPEED = 200f;  //�ִ� ���ݼӵ� ���� ���
    [SerializeField] private const float MAX_MOVE_SPEED = 200f;  //�ִ� �̵��ӵ� ���� ���
    
    [SerializeField] private int currentHp = 5;  //���� ü��
    [SerializeField] private int currentMaxHp = 5; //���� �ִ� ���� ü��
    [SerializeField] private int shieldCount = 0;  // ���� ���尡�� Ƚ��
    [SerializeField] private float attackPowerCoefficient = 100f;  //���� ���ݷ� ���� ���
    [SerializeField] private float attackSpeedCoefiicient = 100f; // ���� ���ݼӵ� ���� ���
    [SerializeField] private float moveSpeed = 5f;  //�̵��ӵ�
    [SerializeField] private float moveSpeedCoefficient = 100f; //���� �̵��ӵ� ���� ���
    [SerializeField] private bool isInvincible = false;  //���� ����
    [SerializeField] private float invincibilityTime = 2f;  //���� ���� �ð�

    public int GetCurrentHp()
    {
        return currentHp;
    }
    public void SetCurrentHp(int change)
    {
        currentHp += change;
    }

    public int GetCurrentMaxHp()
    {
        return currentMaxHp;
    }
    public void SetCurrentMaxHp(int change)
    {
        currentMaxHp += change;
    }

    public int GetShieldCount()
    {
        return shieldCount;
    }
    public void SetShieldCount(int change)
    {
        shieldCount += change;
    }

    public float GetAttackPowerCoefficient()
    {
        return attackPowerCoefficient;
    }
    public void SetAttackPowerCoefficient(float change)
    {
        attackPowerCoefficient += change;
    }
    public float GetAttackSpeedCoefiicient()
    {
        return attackSpeedCoefiicient;
    }
    public void SetAttackSpeedCoefiicient(float change)
    {
        attackPowerCoefficient += change;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(float change)
    {
        moveSpeed += change;
    }

    public float GetMoveSpeedCoefficient()
    {
        return moveSpeedCoefficient;
    }
    public void SetMoveSpeedCoefficient(float change)
    {
        moveSpeedCoefficient += change;
    }

    public bool GetIsInvincible()
    {
        return isInvincible;
    }
    public void SetIsInvincible()
    {

    }

    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }
    public void SetInvincibilityTime()
    {

    }
}
