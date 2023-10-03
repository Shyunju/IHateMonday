using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
}