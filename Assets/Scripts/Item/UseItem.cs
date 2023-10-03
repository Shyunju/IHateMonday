using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private const float CO_TIME = 0.1f;

    [SerializeField]
    private GameObject _onWeapon;
    //private �÷��̾� ���º�ȯ���� ����
    [SerializeField]
    private PlayerStats[] _playerStats;
    private PlayerStatsHandler _controller;
    private GameObject[] _bullet;
    private float _buffTime = 10.0f;

    private void Start()
    {
        _controller = GetComponent<PlayerStatsHandler>();
        _playerStats[0] = _controller.CurrentStats;
    }

    public void OnGuied()
    {
        //gun �߻��� Ÿ�� ã�� n�� ���󰡱�
    }

    public void OnDamageIncrease()
    {
        //n�ʰ� player damage ����
        StartCoroutine(COOnIncreaseDamage());
    }

    public void OnDestroyBullet()
    {
        _bullet = GameObject.FindGameObjectsWithTag("Bullet");
        
        foreach(GameObject currentAllBullet in _bullet)
        {
            Managers.Resource.Destroy(currentAllBullet);
        }
    }

    public void OnInvincibilite()
    {
        //n�ʰ� ����
        StartCoroutine(COOnInvincibility());
    }

    IEnumerator COOnInvincibility()
    {
        _controller.AddStatModifier(_playerStats[1]);
        while(_playerStats[1].invincibilityTime > 0)
        {
            _playerStats[1].invincibilityTime -= CO_TIME;
            yield return new WaitForSeconds(CO_TIME);
        }
        _controller.AddStatModifier(_playerStats[0]);
    }

    IEnumerator COOnIncreaseDamage()
    {
        _controller.AddStatModifier(_playerStats[2]);
        while (_buffTime > 0)
        {
            _buffTime -= CO_TIME;
            yield return new WaitForSeconds(CO_TIME);
        }
        _controller.AddStatModifier(_playerStats[0]);
    }
}
