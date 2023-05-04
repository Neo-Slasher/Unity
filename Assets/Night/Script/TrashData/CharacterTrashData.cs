using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Ư��
public enum Characteristic
{
    None, a,b,c,d,e,f
}

[System.Serializable]
public class CharacterTrashData
{
    public int level;

    public int Hp;
    public int moveSpeed;
    public int offensePower;
    public int attackSpeed;
    public int attackRange;

    public Characteristic[] characteristics;

    public CharacterTrashData()
    {
        level = 1;
        Hp = 20;
        moveSpeed = 10;     //10 = 1�ʴ� 250px, �̵��ӵ� 1�� 30px + ����Ƽ 1ĭ�� 128px
        offensePower = 5;   
        attackSpeed = 10;   //10�ʴ� 10ȸ
        attackRange = 10;   //10 = ���� 150px, 1�� 15px, �ʺ� ��ȭx

        SetCharacteristic();
    }

    void SetCharacteristic()
    {
        characteristics= new Characteristic[level];
        for(int i = 0; i<level; i++)
        {
            characteristics[i] = Characteristic.None;
        }
    }
}
