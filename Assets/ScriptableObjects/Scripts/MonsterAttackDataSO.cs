using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterAttackDataSO", order = 0)]
public class MonsterAttackDataSO : ScriptableObject
{
    [Header("Attack Info")]
    public float speed;         // �Ѿ� �߻� �ӵ�
    public float duration;      // �Ѿ� ���� �ð�
    public int power;           // ���� ������
    public float attackDelay;   // ���� ������
    public LayerMask target;    // ���� ���
    public GameObject projectilePrefab;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;

    public float spread;                    // ź ����
    public int numberOfProjectilesPerShot;  // �� ���� ��� �Ѿ� ��
    public float multipleProjectilesAngle;  // �Ѿ��� ����
}
