using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//특성
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
        moveSpeed = 10;     //10 = 1초당 250px, 이동속도 1당 30px + 유니티 1칸당 128px
        offensePower = 5;   
        attackSpeed = 10;   //10초당 10회
        attackRange = 10;   //10 = 높이 150px, 1당 15px, 너비 변화x

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
