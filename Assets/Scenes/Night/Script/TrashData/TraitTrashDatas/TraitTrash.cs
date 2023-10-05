using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EffectType
{
    none, hp, moveSpeed, attackPower, attackSpeed, 
    attackRange, startMoney, earnMoney, shopSlot, 
    itemSlot, shopMinRank, shopMaxRank, dropRank, 
    dropRate, healByHit, hpRegen, dealOnMax, dealOnHp, 
    active
}

public enum ActiveTrait
{
    none = 0,
    startShield = 28,       //전투 시작시 최대체력 25%의 반 영구 보호막 획득
    dragEnemy = 42,         //매 6초마다 적 끌어당김
    thrustEnemy = 43,       //매 8초마다 적 밀쳐냄
    getMoveSpeed = 44,      //전투 시작 6초 끝나기전 6초 이속 증가
    getAttackPower = 45,    //체력이 일정 이하되면 공격력 증가
    stopEnemy = 61,         //20초마다 2초 적 이동속도 0
    absorbDamage = 62       //일반 공격 적중당 체력 회복 -> 풀피면 보호막 만듬
}

[System.Serializable]
public class TraitTrash
{
    public int traitIdx;            //특성 인덱스
    public string traitName;        //특성 이름
    public int traitReqLvl;         //특성 요구 레벨
    public int traitRank;           //특성 랭크
    public int traitImgIdx;         //이미지 인덱스
    public string traitScript;      //특성 설명문

    public int traitEffectType1;    //특성1 효과 타입
    public EffectType effectType1;
    public float traitEffectValue1;   //특성1 효과 수치
    public bool traitEffectMulti1;   //효과1 곱연산

    public int traitEffectType2;    //특성2 효과 타입
    public EffectType effectType2;
    public float traitEffectValue2;   //특성2 효과 수치
    public bool traitEffectMulti2;   //효과2 곱연산

    public int traitEffectType3;    //특성3 효과 타입
    public EffectType effectType3;
    public float traitEffectValue3;   //특성3 효과 수치
    public bool traitEffectMulti3;   //효과3 곱연산

    public int traitEffectType4;    //특성4 효과 타입
    public EffectType effectType4;
    public float traitEffectValue4;   //특성4 효과 수치
    public bool traitEffectMulti4;   //효과4 곱연산

    public void Parse()
    {
        if (traitEffectType1 != 0)
            effectType1 = (EffectType)traitEffectType1;
        if (traitEffectType2 != 0)
            effectType2 = (EffectType)traitEffectType2;
        if (traitEffectType3 != 0)
            effectType3 = (EffectType)traitEffectType3;
        if (traitEffectType4 != 0)
            effectType4 = (EffectType)traitEffectType4;
    }
}
