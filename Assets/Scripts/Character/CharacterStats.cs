using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

[SerializeField]
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    public int MAX_HP = 8;  //�ִ� ���� ü��
    public const int MAX_SHIELD_COUNT = 2; //�ִ� ���� ����
    public const float MAX_ATTACK_POWER = 200f;  //�ִ� ���ݷ� ���� ���
    public const float MAX_ATTACK_SPEED = 200f;  //�ִ� ���ݼӵ� ���� ���
    public const float MAX_MOVE_SPEED = 200f;  //�ִ� �̵��ӵ� ���� ���
    
    public int currentHp = 5;  //���� ü��
    public int currentMaxHp = 5; //���� �ִ� ���� ü��
    public int shieldCount = 0;  // ���� ���尡�� Ƚ��
    public float attackPowerCoefficient = 100f;  //���� ���ݷ� ���� ���
    public float attackSpeedCoefiicient = 100f; // ���� ���ݼӵ� ���� ���
    public float moveSpeed = 5f;  //�̵��ӵ�
    public float moveSpeedCoefficient = 100f; //���� �̵��ӵ� ���� ���
    public bool isInvincible = false;  //���� ����
    public float invincibilityTime = 2f;  //���� ���� �ð�
}
