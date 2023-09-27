using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items; //�����۵�
    [SerializeField]
    private GameObject[] weapons;   //�����
    private int index;  //�����ε���
    private int itemType;   // ������ or ���� ���� ������

    public void OnCreateItem()
    {
        itemType = Random.Range(0, 3);

        switch (itemType)
        {
            case 0:
                index = Random.Range(0, weapons.Length);    // �ѹ� ���� ����� ����
                Managers.Resource.Instantiate(weapons[index]);
                break;
            default:
                index = Random.Range(0, items.Length);
                Managers.Resource.Instantiate(items[index]);
                break;
        }
    }
}
