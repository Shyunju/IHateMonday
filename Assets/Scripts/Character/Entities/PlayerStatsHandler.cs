using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsHandler : CharacterStatsHandler
{
    private int MAX_HP = 8;                         // �ִ� ���� ü��
    private const int MAX_SHIELD_COUNT = 2;         // �ִ� ���� ����
    private const float MAX_ATTACK_POWER = 200f;    // �ִ� ���ݷ� ���� ���
    private const float MAX_ATTACK_SPEED = 200f;    // �ִ� ���ݼӵ� ���� ���
    private const float MAX_MOVE_SPEED = 200f;      // �ִ� �̵��ӵ� ���� ���

    [SerializeField] private PlayerStats baseStats;

    public PlayerStats CurrentStats { get; private set; }

    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    public void AddStatModifier(PlayerStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }

    public void RemoveStatModifier(PlayerStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        CurrentStats = new PlayerStats();  // �����ϸ鼭 �ʱ�ȭ�� �߰�ȣ
        UpdateStats((a, b) => b, baseStats);    //a, b�� �޾Ƽ� ���ڸ� ��� -> CurrentStat�� baseStat�� �����

        foreach (PlayerStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if (modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }

        LimitAllStats();
    }

    private void UpdateStats(Func<float, float, float> operation, PlayerStats newModifier)
    {
        CurrentStats.currentMaxHp = (int)(operation(CurrentStats.currentMaxHp, newModifier.currentMaxHp));
        CurrentStats.currentHp = (int)(operation(CurrentStats.currentHp, newModifier.currentHp));
        CurrentStats.shieldCount = (int)operation(CurrentStats.shieldCount, newModifier.shieldCount);
        CurrentStats.attackPowerCoefficient = operation(CurrentStats.attackPowerCoefficient, newModifier.attackPowerCoefficient);
        CurrentStats.attackSpeedCoefiicient = operation(CurrentStats.moveSpeed, newModifier.attackSpeedCoefiicient);
        CurrentStats.moveSpeed = operation(CurrentStats.moveSpeed, newModifier.moveSpeed);
    }

    private void LimitAllStats()
    {
        LimitStats(ref CurrentStats.currentMaxHp, MAX_HP);
        LimitStats(ref CurrentStats.currentHp, CurrentStats.currentMaxHp);
        LimitStats(ref CurrentStats.shieldCount, MAX_SHIELD_COUNT);
        LimitStats(ref CurrentStats.attackPowerCoefficient, MAX_ATTACK_POWER);
        LimitStats(ref CurrentStats.attackSpeedCoefiicient, MAX_ATTACK_SPEED);
        LimitStats(ref CurrentStats.moveSpeed, MAX_MOVE_SPEED);
    }

    private void LimitStats(ref int stat, int minVal)
    {
        stat = Mathf.Min(stat, minVal);
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Min(stat, minVal);
    }
}
