using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceNode
{
    public SpaceNode leftSpace;     // ���� ����
    public SpaceNode rightSpace;    // ������ ����
    public SpaceNode parentSpace;   // �θ� ����
    public Rect spaceRect;       // �и��� ������ Rect ����
    public Rect roomRect;        // �и��� ���� ������ ���� Rect����
    public Vector3 center
    {
        get
        {
            return new Vector3(roomRect.x + roomRect.width / 2, roomRect.y + roomRect.height / 2);
        }
    }
    public SpaceNode(Rect rect)
    {
        this.spaceRect = rect;
    }
}