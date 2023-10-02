using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item" , menuName = "New Weapon")]
public class WeaponItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public Sprite icon;
    public string WeaponName;//�̰� �����̸��������� �������������� ��εǳ׿�
}

