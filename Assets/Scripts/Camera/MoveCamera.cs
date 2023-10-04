using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject _target;           // ī�޶� ���� ���
    [SerializeField] private float _moveSpeed = 2f;             // ī�޶� ���� �ӵ�
    private Vector3 _targetPosition;     // ����� ���� ��ġ

    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    // ī�޶� �÷��̾ ����
    void Update()
    {
        if (_target.gameObject != null)
        {
            _targetPosition.Set(_target.transform.position.x, _target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
