using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _onWeapon;
    //private �÷��̾� ���º�ȯ���� ����
    private GameObject[] _bullet;

    public void OnGuied()
    {
        //gun �߻��� Ÿ�� ã�� n�� ���󰡱�
    }

    public void OnDamageIncrease()
    {
        //n�ʰ� player damage ����
    }

    public void OnDestroyBullet()
    {
        _bullet = GameObject.FindGameObjectsWithTag("Bullet");
        
        foreach(GameObject thisBullet in _bullet)
        {
            Managers.Resource.Destroy(thisBullet);
        }
    }

    public void OnInvincibilite()
    {
        //n�ʰ� ����
    }
}
