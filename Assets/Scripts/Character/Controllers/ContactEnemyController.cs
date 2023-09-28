using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Player �ڵ� �ϼ��Ǹ� HealthSystem ���� �ϼ��ؼ� �ּ� Ǯ��
public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool _isCollidingWithTarget;

    [SerializeField] private SpriteRenderer _characterSpriteRenderer;

    // private HealthSystem _healthSystem;
    // private HealthSystem _collidingTargetHealthSystem;
    // private CharacterMovement _collidingMovement;

    protected override void Start()
    {
        base.Start();
        // _healthSystem = GetComponent<HealthSystem>();
        // _healthSystem.OnDamage += Damage;
    }

    // �������� �޾��� ��
    private void OnDamage()
    {
        // followRange�� ũ�� �����Ͽ� target�� ������ ���󰡵��� ����
        followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // target�� �ε����ٸ� (bool ������ OnTrigger �Լ����� ����)
        if (_isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange)
        {   
            // target�� ���� ���� ������ target�� ����
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);    // direction �������� �����̵��� MoveEvent ȣ��
        Rotate(direction);
    }

    // �ε��� ����� ü���� ��ȭ��Ŵ
    private void ApplyHealthChange()
    {
        // AttackSO attackSO = Stats.CurrentStats.attackSO;
        //_collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
        //      // �˹� ȿ���� �ִ� ������ ������ �ε��� ����� CharacterMovement ������Ʈ�� ���� ���(������ �� �ִ� ����� ���)
        //if (attackSO.isOnKnockback && _collidingMovement != null)
        //{
        //      // �ε��� ��� �˹� ȿ�� ����
        //      _collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
        //}
    }

    // �����̴� ������ direction�� ���� Enemy�� Sprite�� ����������
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterSpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        // �ε��� ����� target(Player)�� �ƴ϶�� return
        if (!receiver.CompareTag(targetTag)) return;

        // �ε��� ����� HealthSystem�� ������
        // _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        // �ε��� ����� HealthSystem�� ���� ���(ü���� �ִ� ������Ʈ�� ���)
        // if (_collidingTargetHealthSystem != null) 
        // {
        //      // ���� �ε������� ǥ��
        //      _isCollidingWithTarget = true;
        // }

        // _collidingMovement = receiver.GetComponent<CharacterMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        // ���� �ε��� ǥ�ø� ����
        // _isCollidingWithTarget = false;
    }
}