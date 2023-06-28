using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DifficultyList
{
    public List<Difficulty> difficulties;
}

[Serializable]
public class Difficulty
{
    public int recommandLv;
    public int rewardExp;
    public int goalMoney;
    public double enemyStatus;
    public double enemyRespawn;
    public double normalEnhance;
    public double eliteEnhance;
    public int dropRank;
    public double recommandCP;
}
