using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAttackData
{
    [Header("Attack Info")]
    public float speed;         // �Ѿ� �߻� �ӵ�
    public float duration;      // �Ѿ� ���� �ð�
    public int power;         // ���� ������
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

public class ShootingAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask _levelCollisionLayer;

    [SerializeField] private MonsterAttackData _attackData;
    private float _currentDuration;     // ���� �ð�
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;  // ���� �ð� ����

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position);
        }

        // ������� �ʾҴٸ� �̵�
        _rigidbody.velocity = _direction * _attackData.speed;   // ����ü �ӵ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // ���� �ε����� ���� �������� ��������
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f);
        }
        else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // ���Ÿ� ������ �浹���� ��
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_attackData.power);
                if (_attackData.isOnKnockback)
                {
                    CharacterMovement movement = collision.GetComponent<CharacterMovement>();
                    if (movement != null)
                    {
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position));
        }
    }

    public void InitializeAttack(Vector2 direction)
    {
        _direction = direction;

        _trailRenderer.Clear();
        _currentDuration = 0;

        transform.right = _direction;
        _isReady = true;
    }

    private void DestroyProjectile(Vector3 position)
    {
        gameObject.SetActive(false);
    }
}
