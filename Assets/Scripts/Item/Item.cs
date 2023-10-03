using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;  //�����۵����� �Ҵ�

    private void OnTriggerEnter2D(Collider2D other) //������ ȹ��
    {
        if(other.tag == "Player")
        {
            if(itemData.type == ItemType.Passive)   //�нú� ������
            {
                //�÷��̾� ���� ��ȭ
            }
            else
            {   // ��Ƽ�� ������
                if (itemData.maxStackAmount > itemData.stack)    //������ ����üũ
                {
                    Inventory.s_instance.AddItem(itemData);
                }
            }
        }
    }
}
