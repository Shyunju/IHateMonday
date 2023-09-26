using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO CharacterController ��ӹ޵��� ����
// TODO GameManager �߰� �� ���� ���� -> Player�� GameManager�� ��� �ְ� �� ������
public class EnemyController : MonoBehaviour
{
    //GameManager gamemanager;
    protected Transform ClosestTarget { get; private set; }

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    protected virtual void Start()
    {
        //gameManager = GameManager.instance;
        //ClosestTarget = gameManager.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    // ���� Enemy�� ClosestTarget ���� �Ÿ��� ����
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    // ���� Enemy�� ClosestTarget�� �ٶ󺸴� ���� ���͸� ����
    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
