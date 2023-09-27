using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // ���Ͱ� ������ ��ü Position�� ��
    const int TOTAL_SPAWN_COUNT = 6;

    // Spawn Position�� ������ ���� ��ǥ (���� ��� ��ǥ)
    private Vector3 _centerPos;
    // ���� ��� ��ǥ ~ ������ ����
    private float _limit;

    #region Wave variable
    private int _currentWaveIndex = 0;      // ���� ���̺� index
    private int _currentSpawnCount = 0;     // ���� ���Ͱ� �����Ǵ� spawn�� ���� (���Ͱ� ������ ���� -> ���̺� ���� üũ)
    private int _waveSpawnCount = 0;        // ���̺� �� ���� ��������� ������ ��
    private int _waveSpawnPosCount = 0;     // ���̺꿡�� ���Ͱ� �����Ǵ� spawn�� ��
    private float _spawnInterval = 0.5f;     // ���̺� ����
    #endregion

    public List<GameObject> _enemyPrefabs = new List<GameObject>();     // ������ ���� ������ ����Ʈ
    private List<Vector3> _spawnPositions = new List<Vector3>();        // ���Ͱ� ������ spawn�� position ����Ʈ

    private void Awake()
    {
        InitSpawnPositions();
    }

    void Start()
    {
        StartCoroutine("COPlayWave");
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

    public void SetSpawnPositionInfo(float x, float y, float limit)
    {
        _centerPos.x = x;
        _centerPos.y = y;
        _limit = limit;
    }

    // ���̺� ���� �ڷ�ƾ �Լ�
    IEnumerator COPlayWave()
    {
        while (_currentWaveIndex <= 10)
        {
            if (_currentSpawnCount == 0)
            {
                // ���Ͱ� �����Ǵ� ������ �� ����
                if (_currentWaveIndex % 5 == 0)
                {
                    _waveSpawnPosCount = _waveSpawnPosCount + 1 > _spawnPositions.Count ? _waveSpawnPosCount : _waveSpawnPosCount + 1;
                    _waveSpawnCount = 0;
                }

                // �� ���� ��������� ���� �� ����
                if (_currentWaveIndex % 2 == 0)
                {
                    _waveSpawnCount++;
                }

                for (int i = 0; i < _waveSpawnPosCount; i++)
                {
                    int posIdx = Random.Range(0, _spawnPositions.Count);
                    for (int j = 0; j < _waveSpawnCount; j++)
                    {
                        int prefabIndex = Random.Range(0, _enemyPrefabs.Count);
                        GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPositions[posIdx], Quaternion.identity);
                        // TODO Monster ���� ����
                        // TODO Monster Death ó�� �Լ� Event�� ����
                        _currentSpawnCount++;
                        yield return new WaitForSeconds(_spawnInterval);
                    }
                }
                _currentWaveIndex++;
            }
            yield return null;
        }
    }

    // TODO Monster Death ó�� �Լ� �߰�
}
