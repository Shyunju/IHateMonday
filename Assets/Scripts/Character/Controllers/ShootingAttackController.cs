using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask _levelCollisionLayer;
    
    private float _currentDuration;     // ���� �ð�
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;

    private MonsterAttackDataSO _monsterAttackDataSO;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;  // ���� �ð� ����

        if (_currentDuration > _monsterAttackDataSO.duration)
        {
            Destroy(gameObject);
        }

        // ������� �ʾҴٸ� �̵�
        _rigidbody.velocity = _direction * _monsterAttackDataSO.speed;   // ����ü �ӵ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // ���� �ε����� ���� �������� ��������
            Destroy(gameObject);
        }
        else if (_monsterAttackDataSO.target.value == (_monsterAttackDataSO.target.value | (1 << collision.gameObject.layer)))
        {
            // ���Ÿ� ������ �浹���� ��
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_monsterAttackDataSO.power);
                if (_monsterAttackDataSO.isOnKnockback)
                {
                    CharacterMovement movement = collision.GetComponent<CharacterMovement>();
                    if (movement != null)
                    {
                        movement.ApplyKnockback(transform, _monsterAttackDataSO.knockbackPower, _monsterAttackDataSO.knockbackTime);
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    public void InitializeAttack(Vector2 direction, MonsterAttackDataSO monsterAttackDataSO)
    {
        _monsterAttackDataSO = monsterAttackDataSO;
        _direction = direction;

        _currentDuration = 0;

        transform.right = _direction;
        _isReady = true;
    }
}
