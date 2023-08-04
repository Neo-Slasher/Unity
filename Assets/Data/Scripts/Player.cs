using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    // defalut
    public int level;
    public int reqExp;
    public int curExp;
    public int money;

    // status
    public double maxHp;
    public double curHp;
    public double attackPower;
    public double attackSpeed;
    public double attackRange;
    public double moveSpeed;
    public double shieldDuration;
    public double shieldPoint;
    public double immuneDuration;
    public int immuneCount;
    public bool dashable;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;
    public double damageReductionRate;
    public double dealOnMaxHp;
    public double dealOnCurHp;
    public double healByHit;
    public double hpRegen;

    // trait
    public bool[] trait = new bool[63];

    // equipment
     
}
