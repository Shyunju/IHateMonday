using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����
//��ġ�� �޾Ƽ� ����

public class CoralBox : MonoBehaviour, IBox
{
    private ItemCreate _create;
    private bool _needKey = true;
    private int _random;


    private void Awake()
    {
        _create = GetComponent<ItemCreate>();
        _random = Random.Range(0, 100);
        if(_random <= 30)    //Ű �ʿ� �ڽ����� ����
            _needKey = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (_needKey)
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
        _create.OnCreateItem();
    }
}
