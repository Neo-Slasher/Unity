using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Monster
{
    public int index;
    public double width;
    public double length;

    public int maxHp;
    public int curHp;
    public int moveSpeed;
    public int attackPower;

    public bool dashAble;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;

    public bool isElite;
    public bool isEnforce;
    public bool isResist;
    public bool canKnockback;
    public bool canProj;

    public int imageIndex;
    public int soundIndex;



}
