using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // wave �������� ���
    const string WAVE_PREFAB_PATH = "Prefabs/Waves/Wave";
    const string BOSS_STAGE_PREFAB_PATH = "Prefabs/Waves/BossStage";

    // ���� �������� ��ȣ
    const int MAX_STAGE_NUM = 3;

    private int _curStage;
    private Room _currentRoom;
    private Wave _curWave;
    private BossStage _curBossStage;

    // Player
    public GameObject player;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        _healthSystem = player.GetComponent<HealthSystem>();
        _healthSystem.OnDeath += GameOver;
    }

    void Start()
    {
        _curStage = 1;
        // ���̺� �׽�Ʈ�� �ڵ� 
        //_currentRoom = new Room(new Vector3(2f, 2f, 0), 5f, 7f, RoomType.Boss);
        //StartWave();
    }

    void Update()
    {
        
    }

    // ���� �Ŵ������� ���̺� ������ ��û�� �� ���
    // �÷��̾ �� ���� ������ Room Ÿ������ �Ѱ���� ��
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
            _curBossStage.InitRoomInfo(_currentRoom, _curStage);
        }
    }

    // ���� �������� Ŭ����� BossStage���� ȣ��
    public void StageClear()
    {
        if (_curStage == MAX_STAGE_NUM)
        {
            // ���� ���� Ŭ���� ���������� �Ѿ��
            Managers.Scene.ChangeScene(Define.Scene.EndingScene);
        }
        else
        {
            _curStage++;
        }
    }

    // �÷��̾ �׾��� ���� ���� ���� ó��
    private void GameOver()
    {
        StopAllCoroutines();
        _curStage = 1;
        // ���� ���������� �Ѿ��
        Managers.Scene.ChangeScene(Define.Scene.DeadEndScene);
    }
}
