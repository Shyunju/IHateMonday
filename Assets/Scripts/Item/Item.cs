using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData item;  //�����۵����� �Ҵ�

    private void OnTriggerEnter2D(Collider2D other) //������ ȹ��
    {
        if(other.tag == "Player")
        {
            if(item.type == ItemType.Passive)   //�нú� ������
            {
                //�÷��̾� ���� ��ȭ
            }
            else
            {   // ��Ƽ�� ������
                if (item.maxStackAmount > item.stack)    //������ ����üũ
                    item.stack++;
            }
        }
    }
}
