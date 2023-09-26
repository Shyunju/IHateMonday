using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        transform.position += transform.right * GetComponent<BoxCollider2D>().size.x * 0.5f;
    }

    private void LateUpdate()
    {
        if (_bulletDistance < _nowMoveDistance)
        {
            Managers.Resource.Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _bulletSpeed;
        _nowMoveDistance += (_rigidbody.velocity * Time.fixedDeltaTime).magnitude;
    }
    
    private void OnDisable()
    {
        //���⼭ ����Ʈȿ��
        if(_deadSpawnObject)
        {
            Managers.Resource.Instantiate(_deadSpawnObject , transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(0 != ( _wallCollisionLayer.value & (1 << collision.gameObject.layer)))
        {
            Managers.Resource.Destroy(this);
        }
        else if(0 != ( _targetCollisionLayer.value & ( 1 << collision.gameObject.layer ) ))
        {
            //IDamageAble.GetDamage(_damage);

            /*
            if (collision.TryGetComponent<TopDownMovement>(out TopDownMovement movement))
            {
                movement.ApplyKnockback(transform , _attackData.konckbackPower , _attackData.knockbackTime);
            }
            */
            Managers.Resource.Destroy(this);
        }
        else if(0 != (_envCollisionLayer.value & ( 1 << collision.gameObject.layer ) ))
        {
            //Env������Ʈ�� �޾ƿͼ� �׿� �´� ȿ�� (å�� �ѿ� ������ �� ����������)
            Managers.Resource.Destroy(this);
        }
    }
}
