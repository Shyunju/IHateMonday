using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyController : EnemyController
{
    [SerializeField] private float _followRange = 15f;
    [SerializeField] private float _shootRange = 10f;

    // ���������� ������ �ð�
    private float _timeSinceLastAttack = float.MaxValue;

    public MonsterAttackDataSO monsterAttackDataSO;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        //IsAttacking = false;
        // target�� ���� ���� ���� �� ���󰡱�
        if (distance <= _followRange)
        {
            // target�� ��� �Ÿ� ���� ���� ��
            if (distance <= _shootRange)
            {
                int layerMaskTarget = monsterAttackDataSO.target;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, 
                    (1 << LayerMask.NameToLayer("Wall")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    CallLookEvent(direction);       // �ٶ󺸴� ���� �ٲٱ�
                    CallMoveEvent(Vector2.zero);    // ���� �� ���� �������� ����
                    IsAttacking = true;
                    HandleAttackDelay();
                }
                else
                {
                    CallMoveEvent(direction);       // ��� �Ÿ� ���� target�� ���� ���� �� target�� ���� ������
                    Rotate(direction);
                }
            }
            else
            {
                CallMoveEvent(direction); // target�� ���� ���� ���� ���� �� target�� ���� ������
                Rotate(direction);
            }
        }
    }

    private void HandleAttackDelay()
    {
        if (_timeSinceLastAttack <= monsterAttackDataSO.attackDelay)
        {
            _timeSinceLastAttack += Time.fixedDeltaTime;
        }

        if (IsAttacking && _timeSinceLastAttack > monsterAttackDataSO.attackDelay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }
}
