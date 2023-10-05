using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EquipmentList
{
    public List<Equipment> equipment;
}



[Serializable]
public class Equipment
{
    public int index;
    public string name;
    public int rank;
    public int price;
    public string script;
    public int imageIndex;
    public int part;
    public int attackPower;
    public int attackSpeed;
    public int attackRange;
    public int moveSpeed;
}
