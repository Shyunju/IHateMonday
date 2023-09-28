using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using System;
using System.Linq;
//using System.Xml.Linq;
//using System.Reflection;
//using UnityEditor.PackageManager.UI;

public class CharacterStatsHandler : MonoBehaviour
{
    private int MAX_HP = 8;                         // �ִ� ���� ü��
    private const int MAX_SHIELD_COUNT = 2;         // �ִ� ���� ����
    private const float MAX_ATTACK_POWER = 200f;    // �ִ� ���ݷ� ���� ���
    private const float MAX_ATTACK_SPEED = 200f;    // �ִ� ���ݼӵ� ���� ���
    private const float MAX_MOVE_SPEED = 200f;      // �ִ� �̵��ӵ� ���� ���

    [SerializeField] private CharacterStats baseStats;

    public CharacterStats CurrentStats { get; private set; }

    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    //public CharacterStats _playerStat;
    //private CharacterStatsHandler _characterStatsHandler;
    //_characterStatsHandler = GetComponent<CharacterStatsHandler>();
    // ���Ⱥ����� ���ؼ� �� ������ �ʿ���
    //��� ��� : _characterStatsHandler._playerStat.Get or Set method(float ��ȭ��, int ���� Ÿ��);

    private void Awake()
    {
        UpdateCharacterStats();
    }

    public void AddStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }

    public void RemoveStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        CurrentStats = new CharacterStats();  // �����ϸ鼭 �ʱ�ȭ�� �߰�ȣ
        UpdateStats((a, b) => b, baseStats);    //a, b�� �޾Ƽ� ���ڸ� ��� -> CurrentStat�� baseStat�� �����

        foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
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

    private void UpdateStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        CurrentStats.currentMaxHp = (int)operation(CurrentStats.currentMaxHp, newModifier.currentMaxHp);
        CurrentStats.currentHp = (int)operation(CurrentStats.currentHp, newModifier.currentHp);
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

    //public void ChangeStat(float change, int type)
    //{
    //    switch (type)
    //    {
    //        case 1: //���� ü��
    //            _playerStat.SetCurrentHp((int)change); break;
    //        case 2: //���� �ִ�ü��
    //            _playerStat.SetCurrentMaxHp((int)change); break;
    //        case 3: //���� ���� ����
    //            _playerStat.SetShieldCount((int)change); break;
    //        case 4: //���ݷ� ���� ���
    //            _playerStat.SetAttackPowerCoefficient(change); break;
    //        case 5: //���ݼӵ� ���� ���
    //            _playerStat.SetAttackSpeedCoefiicient(change); break;
    //        case 6: //�̵��ӵ�
    //            _playerStat.SetMoveSpeed(change); break;
    //        case 7: //�̵��ӵ� ���� ���
    //            _playerStat.SetMoveSpeedCoefficient(change); break;
    //    }
    //}
}
