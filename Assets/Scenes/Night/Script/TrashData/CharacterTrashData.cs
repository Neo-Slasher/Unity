using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CharacterTrashData
{
    // defalut
    public int level;
    public int reqExp;
    public int curExp;
    public int money;

    public double maxHp;                    //최대 체력
    public double curHp;                    //현재 체력
    public double attackPower;              //공격력
    public double attackSpeed;
    public double attackRange;
    public double moveSpeed;                //이동 속도
    public double shieldDuration;           //방어막 지속시간
    public double shieldPoint;              //방어막 수치
    public double immuneDuration;           //피해 면역 시간
    public int immnuneCount;                //피해 면역 횟수
    public bool dashAble;                   //대시 여부
    public double dashFreq;                 //대시 주기
    public double dashSpeed;                //대시 이동속도
    public double dashDuration;             //대시 지속시간
    public double damageReduction;          //데미지 경감률

    public double dealOnMaxHp;                //최대 체력 적 추가피해
    public double dealOnCurHp;                 //적 체력 비례 피해

    public double healByHit;                //타격당 회복
    public double hpRegen;                  //자동 지속 회복


    public int currentMoney;                //보유 재화
    public int startMoney;                  //시작 재화
    public float earnMoney;                   //획득 재화

    public int shopSlot;                    //상점 슬롯
    public int shopMinRank;                 //상점 최소 랭크
    public int shopMaxRank;                 //상점 최대 랭크
    public int dropRank;                    //캐릭터 드랍 랭크 수준
    public double dropRate;                 //드랍 확률계수

    public int itemSlot;                    //아이템 장착 슬롯


    public CharacterTrashData(bool isCheat = false)
    {
        if (!isCheat)
        {
            // defalut
            level = 0;
            reqExp = 0;
            curExp = 0;
            money = 0;

            maxHp = 20.0;
            curHp = 20.0;
            moveSpeed = 10.0;               //10 = 1초당 250px, 이동속도 1당 30px + 유니티 1칸당 128px
            attackPower = 5.0;
            dashAble = false;
            dashFreq = 0.0;
            dashSpeed = 0.0;
            dashDuration = 0.0;
            attackSpeed = 10.0;             //10초당 10회
            attackRange = 10.0;             //10 = 높이 150px, 1당 15px, 너비 변화x
            shieldDuration = 0.0;
            shieldPoint = 0.0;
            immuneDuration = 0.0;
            immnuneCount = 0;
            damageReduction = 0.0;
            dealOnMaxHp = 0.0;
            dealOnCurHp = 0.0;
            healByHit = 0.0;
            hpRegen = 0.0;
            currentMoney = 0;
            startMoney = 0;
            earnMoney = 1;
            shopSlot = 1;
            shopMinRank = 0;
            shopMaxRank = 1;
            dropRank = 0;
            dropRate = 1.0;
            itemSlot = 1;
        }
        else
            CheatData();
    }

    void CheatData()
    {
        // defalut
        level = 0;
        reqExp = 0;
        curExp = 0;
        money = 0;

        maxHp = 2000;
        curHp = 2000;
        moveSpeed = 10.0;               //10 = 1초당 250px, 이동속도 1당 30px + 유니티 1칸당 128px
        attackPower = 500.0;
        dashAble = false;
        dashFreq = 0.0;
        dashSpeed = 0.0;
        dashDuration = 0.0;
        attackSpeed = 10.0;             //10초당 10회
        attackRange = 10.0;             //10 = 높이 150px, 1당 15px, 너비 변화x
        shieldDuration = 0.0;
        shieldPoint = 0.0;
        immuneDuration = 0.0;
        immnuneCount = 0;
        damageReduction = 0.0;
        dealOnMaxHp = 0.0;
        dealOnCurHp = 0.0;
        healByHit = 0.0;
        hpRegen = 0.0;
        currentMoney = 0;
        startMoney = 0;
        earnMoney = 1;
        shopSlot = 1;
        shopMinRank = 0;
        shopMaxRank = 1;
        dropRank = 0;
        dropRate = 1.0;
        itemSlot = 1;
    }
}
