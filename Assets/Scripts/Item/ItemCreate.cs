using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _items; //�����۵�
    [SerializeField]
    private GameObject[] _weapons;   //�����
    private int _index;  //�����ε���
    private int _itemType;   // ������ or ���� ���� ������

    public void OnCreateItem()
    {
        _itemType = Random.Range(0, 3);

        switch (_itemType)
        {
            case 0:
                _index = Random.Range(0, _weapons.Length);    // �ѹ� ���� ����� ����
                Managers.Resource.Instantiate(_weapons[_index].name);
                break;
            default:
                _index = Random.Range(0, _items.Length);
                Managers.Resource.Instantiate(_items[_index].name);
                break;
        }
    }
}
