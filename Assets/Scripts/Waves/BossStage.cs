using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    private int _curStage;

    // Spawn Position�� ������ ���� ��ǥ (���� ��� ��ǥ)
    private Vector3 _centerPos;
    // ���� ��� ��ǥ ~ ������ ����
    private float _limit;

    private bool _isGameOver;

    private List<Vector3> _spawnPositions = new List<Vector3>();        // ���Ͱ� ������ spawn�� position ����Ʈ

    public List<GameObject> _bossPrefabs = new List<GameObject>();     // ������ ���� ���� ������ ����Ʈ

    void Start()
    {
        _isGameOver = false;
        InitSpawnPositions();
        StartBossWave();
    }

    void Update()
    {
        if (_isGameOver)
        {
            Managers.Game.StageClear();
        }
    }

    public void InitRoomInfo(Room room, int curStage)
    {
        _centerPos.x = room.center.x;
        _centerPos.y = room.center.y;
        _limit = (room.width <= room.height) ? room.width / 2 : room.height / 2;    // ���� ����, ���� ���� �� �� ª�� �� ������ ��
        _curStage = curStage;
    }

    // ���� ��ǥ�� limit�� ���� ������ spawn position ����Ʈ �ʱ�ȭ
    private void InitSpawnPositions()
    {
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j++)
            {
                float x = _centerPos.x + (j * _limit * 0.8f);
                float y = _centerPos.y + (i * _limit * 0.8f);
                _spawnPositions.Add(new Vector3(x, y));
            }
        }
    }

    private void StartBossWave()
    {
        int posIdx = Random.Range(0, _spawnPositions.Count);
        GameObject boss = Instantiate(_bossPrefabs[_curStage - 1], _spawnPositions[posIdx], Quaternion.identity);
        boss.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
    }

    // ������ OnDeath �̺�Ʈ�� �����
    private void OnEnemyDeath()
    {
        _isGameOver = true;
    }
}
