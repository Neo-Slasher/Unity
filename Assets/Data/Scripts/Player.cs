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
    public int startMoney;
    public float earnMoney;

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

    public int shopSlot;
    public int shopMinRank;
    public int shopMaxRank;
    public int dropRank;
    public double dropRate;
    public int itemSlot;

    // 1~62까지 인덱스를 사용합니다.
    public bool[] trait;

    public Equipment[] equipment;

    public Item[] item;
}
