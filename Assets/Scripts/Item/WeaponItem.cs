using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{

    public WeaponItemData itemData;  //�����۵����� �Ҵ�
    private UI_Interaction _go;
    protected LayerMask _playerLayerMask;
    private SpriteRenderer _spriteRenderer;
    protected void Awake()
    {
        _playerLayerMask = LayerMask.GetMask("Player");
        _spriteRenderer =   GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = itemData.icon;
    }
    public bool GetItemCheck()
    {
        //���⼭ ������ ȹ���Ҷ� ������ üũ�� ������ ����
        return true;
    }
    public void GetItem()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.EquipWeapon(itemData);
        Managers.Resource.Destroy(this);
    }
    private void OnTriggerEnter2D(Collider2D collision) //������ ȹ��
    {
        if (0 != ( _playerLayerMask.value & ( 1 << collision.gameObject.layer ) ))
        {
            _go = Managers.UI.ShowPopupUI<UI_Interaction>();
            _go.Refresh(transform.position);
            _go.AddDelegate(GetItemCheck);
            _go.OnEndInteraction += GetItem;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Managers.Resource.Destroy(_go);
        _go = null;
    }
}
