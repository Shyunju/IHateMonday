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
        itemType = Random.Range(0, 2);
        index = Random.Range(0, items.Length);

        switch (itemType)
        {
            case 0:
                Managers.Instantiate(items[index]);
                break;
            case 1:
                Managers.Instantiate(weapons[index]);
                break;
        }
    }
}
