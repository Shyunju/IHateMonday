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
    private int _maxDepth;                              // ������ �ڼ��ϰ� ������ ����(����)
    private int[] _roomCounts = new int[6];             // �ش� ���̵������� 6���� ���� ���� ����
    private List<int> _notGeneratedRoom = new List<int>(); // ���� ��������� ���� ��
    #endregion

    #region lineRenderer
    [SerializeField] private GameObject _line;          // �� ������ ��輱�� ǥ���ϱ� ���� line renderer
    [SerializeField] private GameObject _map;           // root ������ ǥ���ϱ� ���� line renderer
    [SerializeField] private GameObject _roomLine;      // ���� ǥ���ϱ� ���� line renderer
    #endregion
    TileMapPainter _mapPainter;
    

    void Awake()
    {
        _mapPainter = this.GetComponent<TileMapPainter>();
        Managers.Map.roomList.Clear();
        for (int i = 0; i < _roomCounts.Length; i++)
        {
            _roomCounts[i] = Managers.Map.roomCounts[Managers.Map.currentFloor,i];
            _notGeneratedRoom.Add(i);
        }
        switch(Managers.Map.currentFloor)
        {
            case 0:
                _maxDepth = 3;
                break;
            case 1:
                _maxDepth = 4;
                break;
            case 2:
                _maxDepth = 4;
                break;
        }
    }

    void Start()
    {
        _mapPainter.FillBackground(_mapSize);
        SpaceNode root = new SpaceNode(new Rect(0, 0, _mapSize.x, _mapSize.y));
        //DrawMap(0, 0);
        Divide(root, 0);
        GenerateRoom(root, 0);
        GenerateRoad(root, 0);
        _mapPainter.FillWall(_mapSize);
    }

    #region GenerateMap
    // ���� n��ŭ�� ���̷� ���� ����
    private void Divide(SpaceNode tree, int n)
    {
        if (n == _maxDepth) return;

        float maxLength = Mathf.Max(tree.spaceRect.width, tree.spaceRect.height);
        int split = Mathf.RoundToInt(Random.Range(maxLength * _minDevideRate, maxLength *_maxDevideRate));
        if(tree.spaceRect.width >= tree .spaceRect.height)
        {
            tree.leftSpace = new SpaceNode(new Rect(tree.spaceRect.x, tree.spaceRect.y, split, tree.spaceRect.height));
            tree.rightSpace = new SpaceNode(new Rect(tree.spaceRect.x + split, tree.spaceRect.y, tree.spaceRect.width - split, tree.spaceRect.height));
            //DrawLine(new Vector2(tree.spaceRect.x + split, tree.spaceRect.y), new Vector2(tree.spaceRect.x + split, tree.spaceRect.y + tree.spaceRect.height));
        }
        else
        {
            tree.leftSpace = new SpaceNode(new Rect(tree.spaceRect.x, tree.spaceRect.y, tree.spaceRect.width, split));
            tree.rightSpace = new SpaceNode(new Rect(tree.spaceRect.x, tree.spaceRect.y + split, tree.spaceRect.width, tree.spaceRect.height - split));
            //DrawLine(new Vector2(tree.spaceRect.x , tree.spaceRect.y+ split), new Vector2(tree.spaceRect.x + tree.spaceRect.width, tree.spaceRect.y  + split));
        }
        tree.leftSpace.parentSpace = tree;
        tree.rightSpace.parentSpace = tree;
        Divide(tree.leftSpace, n + 1);
        Divide(tree.rightSpace, n + 1);
    }

    // ���������� �ش� ���� ���� ���� �������� �� ����
    private Rect GenerateRoom(SpaceNode tree, int n)
    {
        Rect rect;
        if (n == _maxDepth)
        {
            rect = tree.spaceRect;
            float width = Random.Range(rect.width / 2, rect.width - 1);
            float height = Random.Range(rect.height / 2, rect.height - 1);

            float x = rect.x + Random.Range(1, rect.width - width);
            float y = rect.y + Random.Range(1, rect.height - height);
            rect = new Rect(x, y, width, height);
            
            Managers.Map.roomList.Add(new Room(new Vector3(x,y), width, height, GetRandomRoomType()));

            //DrawRectangle(rect);
            _mapPainter.FillRoom(_mapSize, rect);
        }
        else
        {
            tree.leftSpace.roomRect = GenerateRoom(tree.leftSpace, n + 1);
            tree.rightSpace.roomRect = GenerateRoom(tree.rightSpace, n + 1);
            rect = tree.leftSpace.roomRect;
        }
        return rect;
    }

    private RoomType GetRandomRoomType()
    {
        int typeNum = _notGeneratedRoom[Random.Range(0, _notGeneratedRoom.Count)];
        _roomCounts[typeNum]--;
        if (_roomCounts[typeNum] <= 0)
        {
            _notGeneratedRoom.Remove(typeNum);
        }
        return (RoomType)typeNum;
    }

    // �� ���� �̾��ִ� �� ����
    private void GenerateRoad(SpaceNode tree, int n)
    {
        if (n == _maxDepth)
            return;

        Vector3 leftNodeCenter = tree.leftSpace.center;
        Vector3 rightNodeCenter = tree.rightSpace.center;

        _mapPainter.FillRoad(leftNodeCenter, rightNodeCenter, _mapSize);
        //DrawLine(new Vector2(leftNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, leftNodeCenter.y));
        //DrawLine(new Vector2(rightNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, rightNodeCenter.y));

        GenerateRoad(tree.leftSpace, n + 1);
        GenerateRoad(tree.rightSpace, n + 1);
    }
    #endregion

    #region Draw
    // lineRender�� ������ ���� �׸��� �Լ�
    private void DrawMap(int x, int y)
    {
        LineRenderer lineRenderer = Instantiate(_map).GetComponent<LineRenderer>();
        lineRenderer.transform.SetParent(this.transform);
        lineRenderer.transform.SetParent(this.transform);
        lineRenderer.SetPosition(0, new Vector2(x, y) - _mapSize / 2);
        lineRenderer.SetPosition(1, new Vector2(x + _mapSize.x, y) - _mapSize / 2);
        lineRenderer.SetPosition(2, new Vector2(x + _mapSize.x, y + _mapSize.y) - _mapSize / 2);
        lineRenderer.SetPosition(3, new Vector2(x, y + _mapSize.y) - _mapSize / 2);
    }

    // lineRender�� �� ������ ��輱�� �׸��� �Լ�
    private void DrawLine(Vector2 from, Vector2 to)
    {
        LineRenderer lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        lineRenderer.transform.SetParent(this.transform);
        lineRenderer.SetPosition(0, from - _mapSize / 2);
        lineRenderer.SetPosition(1, to - _mapSize / 2);
    }
    
    // lineRender�� ���� �׸��� �Լ�
    private void DrawRectangle(Rect rect)
    {
        LineRenderer lineRenderer = Instantiate(_roomLine).GetComponent<LineRenderer>();
        lineRenderer.transform.SetParent(this.transform);
        lineRenderer.SetPosition(0, new Vector2(rect.x, rect.y) - _mapSize / 2);
        lineRenderer.SetPosition(1, new Vector2(rect.x + rect.width, rect.y) - _mapSize / 2);
        lineRenderer.SetPosition(2, new Vector2(rect.x + rect.width, rect.y + rect.height) - _mapSize / 2);
        lineRenderer.SetPosition(3, new Vector2(rect.x, rect.y + rect.height) - _mapSize / 2);
        lineRenderer.SetColors(Color.white, Color.white);
    }
    #endregion
}