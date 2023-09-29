using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Door�� ���̺곪 �濡 ���ӵǾ��־���� Why? ������Ȳ�̴� -> ������ٴ� ��°��� �Ǿ���ϴϱ�..
    public bool isLocked = false;
    public bool isOpened = false;
    private bool _isSide = false;
    //�κ��丮�� Ű�� �ִٴ� �����Ͽ� �۾����غ���
    private Inventory _inventory;
    private LayerMask _playerLayerMask;

    UI_Interaction go;
    private Animator _animator;
    private Sprite _sprite;
    private void Awake()
    {
        //if(isLocked)
        //_inventory = ??.GetComponent<Inventory>();
        _playerLayerMask = LayerMask.GetMask("Player");
        _animator = GetComponent<Animator>();
        _sprite = GetComponentInChildren<Sprite>();
    }
    private void OnEnable()
    {
        //Managers.Map.
    }

    public bool UnLock()
    {
        //if(_inventory.UseKey())
        {
            isLocked = false;

            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLocked)
            return;

        if (0 != ( _playerLayerMask.value & ( 1 << collision.gameObject.layer ) ))
        {
            go = Managers.UI.ShowPopupUI<UI_Interaction>();
            go.Refresh(transform.position);
            go.AddDelegate(UnLock);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Managers.Resource.Destroy(go);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (0 != ( _playerLayerMask.value & ( 1 << collision.gameObject.layer ) ))
        {

        }
    }
}
