using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO GameManager �߰� �� ���� ���� -> Player�� GameManager�� ��� �ְ� �� ������
public class EnemyController : CharacterController
{
    //GameManager gamemanager;
    protected Transform ClosestTarget { get; private set; }

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    protected virtual void Start()
    {
        ClosestTarget = GameObject.FindWithTag("Player").transform;
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
