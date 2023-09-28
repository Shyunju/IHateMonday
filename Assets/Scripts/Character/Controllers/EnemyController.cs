using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO GameManager �߰� �� ���� ���� -> Player�� GameManager�� ��� �ְ� �� ������
public class EnemyController : CharacterController
{
    //GameManager gamemanager;
    protected Transform ClosestTarget { get; private set; }

    [SerializeField] private SpriteRenderer _characterSpriteRenderer;

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

    // �����̴� ������ direction�� ���� Enemy�� Sprite�� ����������
    protected void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterSpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }
}
