using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    public bool playingGame; // 게임 중인지 
    // defalut
    public int day;     //정윤석: 날짜를 세이브하기 위해서 넣었습니다. 제이슨도 바꿨어요.
    public int difficulty;
    public int assassinationCount;

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
    public bool[] trait = new bool[63];

    public List<Equipment> equipment = new List<Equipment>();

    public List<Item> item = new List<Item>();

    // 세팅 관련 데이터
    // 사운드 데이터 저장
    public float bgm_volume;
    public float sfx_volume;
    public float joyStickSize;
}
