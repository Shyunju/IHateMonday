using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayingSounds : MonoBehaviour
{
    [SerializeField] private SoundSO soundSO;
    public SoundSO SoundSO { set { soundSO = value; } }

    private HealthSystem _healthSystem;

    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        
        _healthSystem.OnDamage += PlayingDamage;
        _healthSystem.OnDeath += PlayingDead;

        if ( soundSO.starting != "" && soundSO.isPlayer)   //ĳ���� ������
        {
            Managers.Sound.Play(soundSO.starting, Define.Sound.Bgm);  //������� ���
        }else if( soundSO.starting != "")
        {
            Managers.Sound.Play(soundSO.starting);  //�÷��̾� ���� ĳ���� ������ ���
        }
    }

    void PlayingDamage()
    {
        if (soundSO.damaging != "" && soundSO.isBoss) //����ĳ���Ͱ� �ǰݵ� ���
        {
            int result = Random.Range(0, 10);

            if( result== 0)    //���� 10������ Ȯ���� �ǰ� ���� �÷���
            {
                Managers.Sound.Play(soundSO.damaging);
            }
        }else if(soundSO.damaging != "")  //¡¡��(�÷��̾�)�� �ǰݵ� ���
        {
            Managers.Sound.Play(soundSO.damaging);
        }
    }

    void PlayingDead()
    {
        if(soundSO.dead != "" && soundSO.isBoss)    //������ ����� ���
        {
            Managers.Sound.Play(soundSO.dead);  //���� ������� �Բ�
            Managers.Sound.Play(soundSO.victory);  //¡¡�� �¸� ��� �÷���
        }
        else if(soundSO.dead != "")
        {
            Managers.Sound.Play(soundSO.dead);
        }
    }
    
}
