using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    public bool firstEntered;

    public Action OnBattleStart;
    public Action OnBattleEnd;

    private void Start()
    {
        if(type == RoomType.Wave || type == RoomType.Boss)
        {
            Debug.Log(this.name);
            center = this.transform.GetChild(2).transform.position;
        }
        firstEntered = true;
    }
    public Room(Vector3 center, float width, float height, RoomType type)
    {
        this.center = center;
        this.width = width;
        this.height = height;
        this.type = type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == RoomType.Wave || type == RoomType.Boss)
        {
            if(firstEntered)
            {
                Debug.Log($"first started {this.name}");
                Managers.Game.StartWave(this.transform.GetChild(2).transform.position, this);
                this.OnBattleStart?.Invoke();
                firstEntered = false;
            }
        }
    }
}