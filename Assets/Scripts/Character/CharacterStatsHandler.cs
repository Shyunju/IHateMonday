using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using System;
//using System.Xml.Linq;
//using System.Reflection;
//using UnityEditor.PackageManager.UI;

public class CharacterStatsHandler : MonoBehaviour
{
    public CharacterStats _playerStat;
    //private CharacterStatsHandler _characterStatsHandler;
    //_characterStatsHandler = GetComponent<CharacterStatsHandler>();
    // ���Ⱥ����� ���ؼ� �� ������ �ʿ���
    //��� ��� : _characterStatsHandler._playerStat.Get or Set method(float ��ȭ��, int ���� Ÿ��);

    public void ChangeStat(float change, int type)
    {
        switch (type)
        {
            case 1: //���� ü��
                _playerStat.SetCurrentHp((int)change); break;
            case 2: //���� �ִ�ü��
                _playerStat.SetCurrentMaxHp((int)change); break;
            case 3: //���� ���� ����
                _playerStat.SetShieldCount((int)change); break;
            case 4: //���ݷ� ���� ���
                _playerStat.SetAttackPowerCoefficient(change); break;
            case 5: //���ݼӵ� ���� ���
                _playerStat.SetAttackSpeedCoefiicient(change); break;
            case 6: //�̵��ӵ�
                _playerStat.SetMoveSpeed(change); break;
            case 7: //�̵��ӵ� ���� ���
                _playerStat.SetMoveSpeedCoefficient(change); break;
        }
    }

    
    
    
}
