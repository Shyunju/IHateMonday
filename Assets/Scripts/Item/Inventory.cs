using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSlot
{
    public ItemData item;
    public int quantify;
}

public class Inventory : MonoBehaviour
{
    //private Event Action _event;

    private InventoryUI _inventoryUI;
    private UseItem _useItem;
    [SerializeField]
    private List<Item> _itemsList;
    [SerializeField]
    private Transform _dropPosition;
    private ItemSlot _selectItem;
    private int _itemListIndex = 0;

    private Gun _handGun;
    private Gun _subGun;
    private PlayerInputController _controller;
    public void EquipWeapon(WeaponItemData data)
    {
        if(_handGun == null)
        {
            GameObject go = Managers.Resource.Instantiate($"Guns/{data.WeaponName}");
            _handGun = go.GetComponent<Gun>();
        }
        else
        {
            _controller.UnEquipWeapon(_handGun);
            if (_subGun != null)
            {
                Managers.Resource.Instantiate($"Items/{_subGun.name}Item" , _dropPosition.position);
                Managers.Resource.Destroy(_subGun);
            }

            _subGun = _handGun;
            GameObject go = Managers.Resource.Instantiate($"Guns/{data.WeaponName}");
            _handGun = go.GetComponent<Gun>();
        }

        _controller.EquipWeapon(_handGun);
    }

    public void SwapWeapon()
    {
        if (_handGun == null || _subGun == null)
            return;

        _controller.UnEquipWeapon(_handGun);
        Gun temp = _handGun;
        _handGun = _subGun;
        _subGun = temp;
        _controller.EquipWeapon(_handGun);
    }

    public static Inventory s_instance;

    private void Awake()
    {
        s_instance = this;
        _inventoryUI = GetComponent<InventoryUI>();
        _useItem = GetComponent<UseItem>();
        _controller = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.stack < itemData.maxStackAmount)
        {
            itemData.stack++;
            return;
        }
        else
            return;
    }

    public void ThrowItem(ItemData item)
    {
        Managers.Resource.Instantiate(item.dropPrefab, _dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void OnUse()
    {
        if(_selectItem.item.stack != 0)
        {
            _selectItem.item.stack--;
            if (_selectItem.item.consumables[0].type == ConsumableType.BulletGuide)  //����
            {
                _useItem.OnGuied();
            }
            else if(_selectItem.item.consumables[0].type == ConsumableType.IncreaseDamage)   //������ ����������
            {
                _useItem.OnDamageIncrease();
            }
            else if(_selectItem.item.consumables[0].type == ConsumableType.BulletDelete)     //����ź �Ҹ�����
            {
                _useItem.OnDestroyBullet();
            }
            else if(_selectItem.item.consumables[0].type == ConsumableType.Invincibility)    //������ ����
            {
                _useItem.OnInvincibilite();
            }

            return;
        }
        else
        {
            return;
        }
    } 

    public void UpdateInventoryUI()
    {
        //inventoryUI set ȣ��
        _inventoryUI.Set(_selectItem);
    }

    public void ChangeItem()
    {
        //������ ü���� Ű ��������
        //inventoryUI Set ȣ��
        //itemsList[itemListIndex] ��� -> index ���� listLength �̻� => 0����
        _itemListIndex = (_itemListIndex + 1) % _itemsList.Count;
        _selectItem.item = _itemsList[_itemListIndex].itemData;
        _inventoryUI.Set(_selectItem);
    }
}
