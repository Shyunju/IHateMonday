using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    #region GenerateMapSetting
    [SerializeField] private Vector2Int _mapSize;       // ������ �� ũ��
    [SerializeField] private float _minDevideRate;      // ������ ������ �ּ� ����
    [SerializeField] private float _maxDevideRate;      // ������ ������ �ִ� ����
    [SerializeField] private int _maxDepth;             // ������ �ڼ��ϰ� ������ ����(����)
    #endregion

    #region lineRenderer
    [SerializeField] private GameObject _line;          // �� ������ ��輱�� ǥ���ϱ� ���� line renderer
    [SerializeField] private GameObject _map;           // root ������ ǥ���ϱ� ���� line renderer
    [SerializeField] private GameObject _roomLine;      // ���� ǥ���ϱ� ���� line renderer
    #endregion

    void Start()
    {
        SpaceNode root = new SpaceNode(new RectInt(0, 0, _mapSize.x, _mapSize.y));
        DrawMap(0, 0);
        Divide(root, 0);
        GenerateRoom(root, 0);
    }

    #region GenerateMap
    // ���� n��ŭ�� ���̷� ���� ����
    private void Divide(SpaceNode tree, int n)
    {
        if (n == _maxDepth) return;

        int maxLength = Mathf.Max(tree.spaceRect.width, tree.spaceRect.height);
        int split = Mathf.RoundToInt(Random.Range(maxLength * _minDevideRate, maxLength *_maxDevideRate));
        if(tree.spaceRect.width >= tree .spaceRect.height)
        {
            tree.leftSpace = new SpaceNode(new RectInt(tree.spaceRect.x, tree.spaceRect.y, split, tree.spaceRect.height));
            tree.rightSpace = new SpaceNode(new RectInt(tree.spaceRect.x + split, tree.spaceRect.y, tree.spaceRect.width - split, tree.spaceRect.height));
            DrawLine(new Vector2(tree.spaceRect.x + split, tree.spaceRect.y), new Vector2(tree.spaceRect.x + split, tree.spaceRect.y + tree.spaceRect.height));
        }
        else
        {
            tree.leftSpace = new SpaceNode(new RectInt(tree.spaceRect.x, tree.spaceRect.y, tree.spaceRect.width, split));
            tree.rightSpace = new SpaceNode(new RectInt(tree.spaceRect.x, tree.spaceRect.y + split, tree.spaceRect.width, tree.spaceRect.height - split));
            DrawLine(new Vector2(tree.spaceRect.x , tree.spaceRect.y+ split), new Vector2(tree.spaceRect.x + tree.spaceRect.width, tree.spaceRect.y  + split));
        }
        tree.leftSpace.parentSpace = tree;
        tree.rightSpace.parentSpace = tree;
        Divide(tree.leftSpace, n + 1);
        Divide(tree.rightSpace, n + 1);
    }

    // ���������� �ش� ���� ���� ���� �������� �� ����
    private RectInt GenerateRoom(SpaceNode tree, int n)
    {
        RectInt rect;
        if (n == _maxDepth)
        {
            rect = tree.spaceRect;
            int width = Random.Range(rect.width / 2, rect.width - 1);
            int height = Random.Range(rect.height / 2, rect.height - 1);

            int x = rect.x + Random.Range(1, rect.width - width);
            int y = rect.y + Random.Range(1, rect.height - height);
            rect = new RectInt(x, y, width, height);
            DrawRectangle(rect);
        }
        else
        {
            tree.leftSpace.roomRect = GenerateRoom(tree.leftSpace, n + 1);
            tree.rightSpace.roomRect = GenerateRoom(tree.rightSpace, n + 1);
            rect = tree.leftSpace.roomRect;
        }
        return rect;
    }
    #endregion

    #region Draw
    // lineRender�� ������ ���� �׸��� �Լ�
    private void DrawMap(int x, int y)
    {
        LineRenderer lineRenderer = Instantiate(_map).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(x, y) - _mapSize / 2); //���� �ϴ�
        lineRenderer.SetPosition(1, new Vector2(x + _mapSize.x, y) - _mapSize / 2); //���� �ϴ�
        lineRenderer.SetPosition(2, new Vector2(x + _mapSize.x, y + _mapSize.y) - _mapSize / 2);//���� ���
        lineRenderer.SetPosition(3, new Vector2(x, y + _mapSize.y) - _mapSize / 2);
    }

    // lineRender�� �� ������ ��輱�� �׸��� �Լ�
    private void DrawLine(Vector2 from, Vector2 to)
    {
        LineRenderer lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, from - _mapSize / 2);
        lineRenderer.SetPosition(1, to - _mapSize / 2);
    }
    
    // lineRender�� ���� �׸��� �Լ�
    private void DrawRectangle(RectInt rect)
    {
        LineRenderer lineRenderer = Instantiate(_roomLine).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(rect.x, rect.y) - _mapSize / 2);
        lineRenderer.SetPosition(1, new Vector2(rect.x + rect.width, rect.y) - _mapSize / 2);
        lineRenderer.SetPosition(2, new Vector2(rect.x + rect.width, rect.y + rect.height) - _mapSize / 2);
        lineRenderer.SetPosition(3, new Vector2(rect.x, rect.y + rect.height) - _mapSize / 2);
        lineRenderer.SetColors(Color.white, Color.white);
    }
    #endregion
}