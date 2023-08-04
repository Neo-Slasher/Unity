using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MonsterList {
    public List<Monster> monster;
}


[Serializable]
public class Monster
{
    private int index;
    private double width;
    private double length;

    private int maxHp;
    private int curHp;
    private int moveSpeed;
    private int attackPower;

    private bool dashAble;
    private double dashFreq;
    private double dashSpeed;
    private double dashDuration;

    private bool isElite;
    private bool isEnforce;
    private bool isResist;
    private bool canKnockback;
    private bool canProj;

    private int imageIndex;
    private int soundIndex;



}
