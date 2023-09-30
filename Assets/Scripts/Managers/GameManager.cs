using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // wave �������� ���
    const string WAVE_PREFAB_PATH = "Prefabs/Waves/Wave";
    const string BOSS_STAGE_PREFAB_PATH = "Prefabs/Waves/BossStage";

    private Room _currentRoom;
    private Wave _curWave;
    private BossStage _curBossStage;

    void Start()
    {
        // ���̺� �׽�Ʈ�� �ڵ� 
        //_currentRoom = new Room(new Vector3(2f, 2f, 0), 5f, 7f, RoomType.Boss);
        //StartWave();
    }

    void Update()
    {
        
    }

    public void StartWave(Room curRoom)
    {
        _currentRoom = curRoom;

        if (_currentRoom.type == RoomType.Wave)
        {
            _curWave = Instantiate(Resources.Load<GameObject>(WAVE_PREFAB_PATH)).GetComponent<Wave>();
            _curWave.InitRoomInfo(_currentRoom);
        }
        else
        {
            _curBossStage = Instantiate(Resources.Load<GameObject>(BOSS_STAGE_PREFAB_PATH)).GetComponent<BossStage>();
            _curBossStage.InitRoomInfo(_currentRoom, 1);
        }
    }
}
