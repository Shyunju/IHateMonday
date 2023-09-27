using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScraperBullet : Bullet
{
    private Transform _parentTransform;
    private LineRenderer _lineRenderer;
    private int tagetLayer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        tagetLayer = LayerMask.GetMask("Wall") | LayerMask.GetMask("Env") | LayerMask.GetMask("Monster");
    }

    public void LaserInit(int lineSize, Transform parent)
    {
        _lineRenderer.positionCount = lineSize;
        _parentTransform = parent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = _parentTransform.right;
        Vector2 pos = _parentTransform.position;
        float length = _bulletDistance;
        _lineRenderer.SetPosition(0, pos);
        
        //���̸� ��̴ϴ� ������ī��Ʈ��ŭ
        for (int i = 1; i < _lineRenderer.positionCount; ++i)
        {

            RaycastHit2D hit = Physics2D.Raycast(pos, dir, length, tagetLayer);
            if(hit)
            {
                length -= hit.distance;
                dir = Vector2.Reflect(dir, hit.normal);
                pos = hit.point + dir * 0.3f;
                _lineRenderer.SetPosition(i, hit.point);


                //���⼭ �浹ü�� ������ó���� �̷��� �ؾߵ�
                //�ʴ� _damage����
            }
            else
            {
                _lineRenderer.SetPosition(i, pos + dir * length);
                length = 0;
            }

        }
    }
}
