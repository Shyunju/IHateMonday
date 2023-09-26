using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType
{
    Normal,
    NoneMonster,
    Wave,
    Box,
    Boss,
    Altar,
}

public class SpaceNode
{
    public SpaceNode leftSpace;     // ���� ����
    public SpaceNode rightSpace;    // ������ ����
    public SpaceNode parentSpace;   // �θ� ����
    public RectInt spaceRect;       // �и��� ������ Rect ����
    public RectInt roomRect;        // �и��� ���� ������ ���� Rect����
    public Vector2Int center
    {
        get
        {
            return new Vector2Int(roomRect.x + roomRect.width/2, roomRect.y + roomRect.height/2);
        }
    }
    // TODO �� �뿡 Ÿ�� �����ϱ�
    //public RoomType roomType;

    public SpaceNode(RectInt rect)
    {
        this.spaceRect = rect;
    }
}
