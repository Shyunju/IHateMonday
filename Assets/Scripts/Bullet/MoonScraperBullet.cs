using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScraperBullet : Bullet
{
    private Transform _parentTransform;
    private LineRenderer _lineRenderer;
    private int tagetLayer;
    [SerializeField]private int _smooth;

    private float _damageDelay = 0.1f;
    private float _damageTime;
    private bool _damageAble = true;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        tagetLayer = LayerMask.GetMask("Wall") | LayerMask.GetMask("Env") | LayerMask.GetMask("Enemy");
    }

    public void LaserInit(int lineSize, Transform parent)
    {
        _lineRenderer.positionCount = lineSize;
        if(_isGuided)
        {
            _lineRenderer.positionCount = ( _lineRenderer.positionCount - 1 ) * _smooth + 1;
            //50���� �� ������ ���� ���γ����� �ָ��ؼ�.. ����?
        }
        _parentTransform = parent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = _parentTransform.right;
        Vector2 pos = _parentTransform.position;
        float length = _bulletDistance;
        _lineRenderer.SetPosition(0, pos);
        _lineRenderer.startWidth = transform.localScale.x;
        _lineRenderer.endWidth = transform.localScale.x;
        //���̸� ��̴ϴ� ������ī��Ʈ��ŭ
        if (!_isGuided)
        {
            for (int i = 1 ; i < _lineRenderer.positionCount ; ++i)
            {

                RaycastHit2D hit = Physics2D.Raycast(pos , dir , length , tagetLayer);
                if (hit)
                {
                    length -= hit.distance;
                    dir = Vector2.Reflect(dir , hit.normal);
                    pos = hit.point + dir * 0.3f;
                    _lineRenderer.SetPosition(i , hit.point);


                    AddDamage(hit);
                }
                else
                {
                    _lineRenderer.SetPosition(i , pos + dir * length);
                    length = 0;
                }

            }
        }
        else
        {
            for (int i = 1 ; i <= _lineRenderer.positionCount / _smooth ; ++i)
            {
                dir.Normalize();
                _target = GetNearObjectInAngle(pos , dir);
                BeziarCurve b = new BeziarCurve();
                b.InputPosition(pos);
                if (_target == null || ((Vector2)_target.transform.position - pos).magnitude > length)
                {
                    RaycastHit2D hit = Physics2D.Raycast(pos , dir , length , tagetLayer);
                    if (hit)
                    {
                        b.InputPosition(hit.point * 1.05f);
                    }
                    else
                    {
                        b.InputPosition(pos + dir * length);
                    }
                }
                else
                {
                    Vector3 targetPos = _target.transform.position;
                    //���⼭ �߰��������� ���� ��ǥ�� ã�Ƽ� �� ���� ��������
                    //dot(Ÿ�ٱ�������.normalize, ������.normalize) -> cosA;
                    //Ÿ�ٱ�������.magnitude * cosA -> ���翵�� ����
                    Vector2 targetVector = ( (Vector2)_target.transform.position - pos );

                    b.InputPosition(pos + dir * Vector2.Dot(dir , targetVector.normalized) * targetVector.magnitude);
                    b.InputPosition(_target.transform.position);
                }

                Vector2 beforePos = pos;
                Vector2 nextPos = new Vector2();
                bool objectHit = false;
                for (int j  = 0 ; j < _smooth ; ++j)
                {
                    int index = ( i - 1 ) * _smooth + j + 1;// -> ùȸ���� 1~5 �ι�°�� 6~10
                    if (!objectHit)
                    {
                        nextPos = b.GetBeziarPosition(1.0f / _smooth * ( j + 1 ));

                        dir = nextPos - beforePos;
                       
                        RaycastHit2D hit = Physics2D.Raycast(beforePos , dir.normalized , dir.magnitude , tagetLayer);
                        if (hit)//������ ���� ������ �Ǿ����� �ȵ����� �ƹ��͵� �ȸ¾Ҵٴ¼Ҹ���
                        {
                            dir = Vector2.Reflect(dir.normalized , hit.normal);
                            nextPos = hit.point;
                            pos = hit.point + dir.normalized * 0.1f;
                            objectHit = true;
                            AddDamage(hit);
                        }
                    }
                    _lineRenderer.SetPosition(index , nextPos);
                    length -= dir.magnitude;
                    beforePos = nextPos;
                }
            }
        }
    }

    void AddDamage(RaycastHit2D hit)
    {
        if (!_damageAble)
            return;
        if ((1 << hit.transform.gameObject.layer) != _targetCollisionLayer)
            return;

        if (hit.transform.TryGetComponent<CharacterMovement>(out CharacterMovement movement))
        {
            movement.ApplyKnockback(hit.point , _knockBack , 0.05f);
        }
        if (hit.transform.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            health.ChangeHealth((int)(-_damage * _damageDelay));
            Camera.main.GetComponent<ShakeCamera>().Shake(ShakeType.Attack);
        }
    }

    private void LateUpdate()
    {
        if(_damageAble)
        {
            _damageAble = false;
            _damageTime = Time.time + _damageDelay;
        }
        else if (Time.time > _damageTime)
        {
            _damageAble = true;
        }

    }
}
