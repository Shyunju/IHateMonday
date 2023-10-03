using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public enum RoomType
{
    Normal,         // �⺻��
    NoneMonster,    // ���Ͱ� ���� ��
    Wave,           // ���̺갡 �ִ� ��
    Box,            // ���ڰ� �ִ� ��
    Boss,           // ���� ��
    Altar,          // ���� ��
}

public class Room : MonoBehaviour
{
    public Vector3 center;      // ���� �߽� ��ǥ
    public float width;         // ���� ����    
    public float height;        // ���� ����
    public RoomType type;       // �� ����
    //public bool notDone = true;

    public Action OnBattleStart;
    public Action OnBattleEnd;
    public Room(Vector3 center, float width, float height, RoomType type)
    {
        this.center = center;
        this.width = width;
        this.height = height;
        this.type = type;
    }

    private void Awake()
    {
        center = this.transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"now entered {type} room");
        Debug.Log($"center({this.center.x}, {this.center.y})");

        if(type == RoomType.NoneMonster)
        {
            return;
        }
        else
        {
            Managers.Game.StartWave(this);
            /*if (notDone)
            {
                Managers.Game.StartWave(this);
                notDone = false;
            }*/
            //OnBattleStart?.Invoke();
        }
    }
}