using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool _isCollidingWithTarget;

    private HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    //private CharacterMovement _collidingMovement; // TODO ī�޶� ����ŷ �����Ǹ� ����

    protected override void Start()
    {
        base.Start();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDamage += OnDamage;
    }

    // Player���� ���� ������ Player�� ���󰡵���
    private void OnDamage()
    {
        followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange)
        {   
            // ���� ���� ������ Player�� ���󰡵���
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction); 
        Rotate(direction);
    }

    // Player���� �������� ��
    private void ApplyHealthChange()
    {
        _collidingTargetHealthSystem.ChangeHealth(-1);
        //      // �˹� ȿ���� �ִ� ������ ������ �ε��� ����� CharacterMovement ������Ʈ�� ���� ���(������ �� �ִ� ����� ���)
        //if (attackSO.isOnKnockback && _collidingMovement != null)
        //{
        //      // �ε��� ��� �˹� ȿ�� ����
        //      _collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        // �ε��� ����� target(Player)�� �ƴ϶�� return
        if (!receiver.CompareTag(targetTag)) return;

        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        if (_collidingTargetHealthSystem != null)
        {
            // ���� �ε������� ǥ��
            _isCollidingWithTarget = true;
        }

        //_collidingMovement = receiver.GetComponent<CharacterMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        // ���� �ε��� ǥ�ø� ����
        _isCollidingWithTarget = false;
    }
}