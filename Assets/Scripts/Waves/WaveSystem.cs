using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // wave �������� ���
    const string WAVE_PREFAB_PATH = "Prefabs/Waves/Wave";

    private string _dataPath;   // ���� �������� ������ �޾ƿͼ� �ϼ��ϴ� ���� ������ ���
    private int _curStage;      // ���� ��������
    private GameObject _wavePrefab;

    private void Start()
    {
        _dataPath = WAVE_PREFAB_PATH + _curStage;
        _wavePrefab = Resources.Load<GameObject>(_dataPath);
    }

    public void StartWave(bool isBossRoom, float x, float y, float limit)
    {
        //if (isBossRoom)
        //{
        //      
        //} else
        //{
        //  _wavePrefab.GetComponent<Wave>().SetSpawnPositionInfo(x, y, limit);
        //  Instantiate(_wavePrefab);
        //}
    }
}
