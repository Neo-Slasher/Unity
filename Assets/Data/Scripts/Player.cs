using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour {
    // defalut
    public int level;
    public int reqExp;
    public int curExp;
    public int money;

    // status
    public int maxHp;
    public int curHp;
    public int attackPower;
    public int attackSpeed;
    public int attackRange;
    public int moveSpeed;
    public double shieldDuration;
    public int shieldPoint;
    public double immuneDuration;
    public int immuneCount;
    public bool dashable;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;
    public double damageReductionRate;
    public double dealOnMaxHp;
    public double dealOnCurHp;
    public int healByHit;
    public int hpRegen;

    // trait

}
