using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemList {
    public List<Item> item;
}


[Serializable]
public class Item {
    public int itemIdx;
    public string name;
    public int rank;
    public int price;
    public string script;
    public int imgIdx;
    public int category;
    public bool attackPowerCalc;
    public double attackPowerValue;
    public bool attackSpeedCalc;
    public double attackSpeedValue;
    public bool attackRangeCalc;
    public double attackRangeValue;
}
