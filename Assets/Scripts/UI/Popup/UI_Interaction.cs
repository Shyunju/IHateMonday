using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interaction : UI_Popup
{
    public delegate bool InteractionDelegate();
    public Action OnEndInteraction;
    // �Լ� ����Ʈ�� �ֱ� ���� ����Ʈ
    private LinkedList<InteractionDelegate> _list = new LinkedList<InteractionDelegate>();


    private CharacterController _controller;
    [SerializeField] private GameObject _player;
    public override bool Init()
    {
        //������ü�� �̴��� �ϱ�Ⱦ �̷����س���� ��ũ�������彺���̽� ����ؼ�
        _init = true;
        return true;
    }
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _controller = _player.GetComponent<CharacterController>();
        _controller.OnInteractionEvent -= OnInteraction;
        _controller.OnInteractionEvent += OnInteraction;
    }
    public void Refresh(Vector3 position)
    {
        transform.position = position;
    }
    public void AddDelegate(InteractionDelegate func)
    {
        _list.AddLast(func);
    }
    public void AddEndResult(Action func)
    {
        OnEndInteraction += func;
    }
    public void OnDestroy()
    {
        //TODO ��Ʈ�ѷ��� OnInteraction(�ִٸ�) ���� �ڱ��ڽ��� ����.
        _controller.OnInteractionEvent -= OnInteraction;
    }
    public void OnInteraction()
    {
        LinkedListNode<InteractionDelegate> node = _list.First;
        while(node != null)
        {
            InteractionDelegate func = node.Value;
            node = node.Next;
            if (func())
            {
                _list.Remove(func);
            }
        }

        if (_list.Count == 0)
        {
            OnEndInteraction?.Invoke();
            Managers.Resource.Destroy(this);
        }

        return;
    }
}
