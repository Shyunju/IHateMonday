using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapPainter : MonoBehaviour
{
    #region Tiles
    [SerializeField] private Tilemap _tileMap;      // Ÿ���� �׷����� ��
    [SerializeField] private Tile roomTile;         // ���� ǥ���� Ÿ��
    [SerializeField] private Tile roadTile;         // ���� ǥ���� Ÿ��
    [SerializeField] private Tile wallTile;         // ���� ǥ���� Ÿ��
    [SerializeField] private Tile outTile;          // �ܰ�����(�����)�� ǥ���� Ÿ��
    #endregion
    
    //����� ä��� �Լ�
    public void FillBackground(Vector2Int mapSize)
    {
        for (int i = -10; i < mapSize.x + 10; i++)
        {
            for (int j = -10; j < mapSize.y + 10; j++)
            {
                _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), outTile);
            }
        }
    }

    //�� Ÿ�ϰ� �ٱ� Ÿ���� ������ �κ�
    public void FillWall(Vector2Int mapSize)
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                if (_tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0)) == outTile)
                {
                    //�ٱ�Ÿ�� �� ���
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            if (_tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == roomTile)
                            {
                                _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    //���� Rect������ �޾Ƽ� tile�� �����ϴ� �Լ�
    public void FillRoom(Vector2Int mapSize, Rect rect)
    {
        int x = (int)System.Math.Round(rect.x);
        int y = (int)System.Math.Round(rect.y);
        int width = (int)System.Math.Round(rect.width);
        int height = (int)System.Math.Round(rect.height);

        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
            }
        }
    }

    public void FillRoad(Vector3 leftNodeCenter, Vector3 rightNodeCenter, Vector2Int mapSize)
    {
        int startX = (int)System.Math.Round(System.Math.Min(leftNodeCenter.x, rightNodeCenter.x));
        int startY = (int)System.Math.Round(System.Math.Min(leftNodeCenter.y, rightNodeCenter.y));

        int endX = (int)System.Math.Round(System.Math.Max(leftNodeCenter.x, rightNodeCenter.x));
        int endY = (int)System.Math.Round(System.Math.Max(leftNodeCenter.y, rightNodeCenter.y));

        for (int i = startX; i <= endX; i++)
        {
            _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, (int)System.Math.Round(leftNodeCenter.y) - mapSize.y / 2 - 1, 0), roadTile);
            _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, (int)System.Math.Round(leftNodeCenter.y) - mapSize.y / 2, 0), roadTile);
            _tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, (int)System.Math.Round(leftNodeCenter.y) - mapSize.y / 2 + 1, 0), roadTile);
        }

        for (int j = startY; j <= endY; j++)
        {
            _tileMap.SetTile(new Vector3Int((int)System.Math.Round(rightNodeCenter.x) - mapSize.x / 2 - 1, j - mapSize.y / 2, 0), roadTile);
            _tileMap.SetTile(new Vector3Int((int)System.Math.Round(rightNodeCenter.x) - mapSize.x / 2, j - mapSize.y / 2, 0), roadTile);
            _tileMap.SetTile(new Vector3Int((int)System.Math.Round(rightNodeCenter.x) - mapSize.x / 2 + 1, j - mapSize.y / 2, 0), roadTile);
        }
    }
}