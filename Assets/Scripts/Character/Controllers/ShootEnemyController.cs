using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyController : EnemyController
{
    [SerializeField] private float followRange = 15f;
    [SerializeField] private float shootRange = 10f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        //IsAttacking = false;
        // target�� ���� ���� ���� �� ���󰡱�
        if (distance <= followRange)
        {
            // target�� ��� �Ÿ� ���� ���� ��
            if (distance <= shootRange)
            {
                //int layerMaskTarget = Stats.CurrentStats.attackSO.target;

                //RaycastHit2D hit = Physics.Raycast(transform.position, direction, 11f,
                //    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                //if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                //{
                //    CallLookEvent(direction);       // �ٶ󺸴� ���� �ٲٱ�
                //    CallMoveEvent(Vector2.zero);    // ���� �� ���� �������� ����
                //    IsAttacking = true;
                //}
                //else
                //{
                //    CallMoveEvent(direction);       // ��� �Ÿ� ���� target�� ���� ���� �� target�� ���� ������
                //    Rotate(direction);
                //}
            }
            else
            {
                CallMoveEvent(direction); // target�� ���� ���� ���� ���� �� target�� ���� ������
                Rotate(direction);
            }
        }
    }
}
