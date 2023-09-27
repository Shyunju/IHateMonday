using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellfishBox : MonoBehaviour, IBox
{
    private ItemCreate create;
    private bool needKey = true;
    private int random;


    private void Awake()
    {
        create = GetComponent<ItemCreate>();
        random = Random.Range(0, 100);
        if (random < 30)    //Ű �ʿ� �ڽ����� ����
            needKey = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (needKey)
                OnKeyUsePopUpUI();
            else
                OnKeyUse();
        }

    }

    public void OnKeyUsePopUpUI()
    {
        //Ű��� �ڽ� �˾� ȣ�� , ��������
    }

    public void OnKeyUse()  //�ڽ� ����
    {
        Managers.Destroy(this);
        create.OnCreateItem();
    }
}
