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
            return new Vector3(roomRect.center.x, roomRect.center.y);
        }
    }
    public SpaceNode(Rect rect)
    {
        this.spaceRect = rect;
    }
}