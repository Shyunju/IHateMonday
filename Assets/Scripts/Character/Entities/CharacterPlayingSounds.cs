using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayingSounds : MonoBehaviour
{
    [SerializeField] private SoundSO soundSO;
    public SoundSO SoundSO { set { soundSO = value; } }

    private HealthSystem _healthSystem;


    private void Awake()
    {
    }
    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        
        _healthSystem.OnDamage += PlayingDamage;
        _healthSystem.OnDeath += PlayingDead;

        if ( soundSO.starting != "")   //ĳ���� ������ �÷���
        {
            Managers.Sound.Play(soundSO.starting);
        }
    }

    void PlayingDamage()
    {
        if (soundSO.damaging != "" && soundSO.isBoss) //����ĳ���Ͱ� �ǰݵ� ���
        {
            int result = Random.Range(0, 10);

            if( result== 0)    //���� 10������ Ȯ���� �ǰ� ���� �÷���
            {
                Managers.Sound.Play(soundSO.damaging, Define.Sound.Effect);
            }
        }else if(soundSO.damaging != null)  //¡¡��(�÷��̾�)�� �ǰݵ� ���
        {
            Managers.Sound.Play(soundSO.damaging, Define.Sound.Effect);
        }
    }

    void PlayingDead()
    {
        if(soundSO.dead != "" && soundSO.isBoss)    //������ ����� ���
        {
            Managers.Sound.Play(soundSO.dead, Define.Sound.Effect);  //���� ������� �Բ�
            Managers.Sound.Play(soundSO.victory, Define.Sound.Effect);  //¡¡�� �¸� ��� �÷���
        }
        else if(soundSO.dead != "")
        {
            Managers.Sound.Play(soundSO.dead, Define.Sound.Effect);
        }
    }
    
}
