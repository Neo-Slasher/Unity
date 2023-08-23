using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTrashData
{
    public int diff;                    //난이도 
    public int recommandLv;              //권장레벨  
    public int rewardExp;                 //보상 Exp
    public int goalMoney;                //목표 알파
    public double enemyStatus;             //적 스텟 배수
    public double enemyRespawn;              //적 개체 배수
    public double normalEnhance;    //일반 강화 확률
    public double eliteEnhance;     //엘리트 강화 확률
    public int dropRank;            //난이도 드랍 랭크
    public double recommandCP;            //권장 CP 랭크

    //public LevelTrashData(int getLevel = 1) 
    //{ 
    //    SetLevelData(getLevel);
    //}

    //void SetLevelData(int getLevel)
    //{
    //    diff = getLevel;
    //    switch(getLevel)
    //    {
    //        case 1:
    //            recommandLv = 1;
    //            rewardExp = 1;
    //            goalMoney = 1000;
    //            enemyStatus = 1.0f;
    //            enemyRespawn = 1.0f;
    //            normalEnhance = 0;
    //            eliteEnhance = 0;
    //            dropRank = 1;
    //            recommandCP = 1.0;
    //            break;

    //        case 2:
    //            recommandLv = 3;
    //            rewardExp = 3;
    //            goalMoney = 1500;
    //            enemyStatus = 1.1f;
    //            enemyRespawn = 1.1f;
    //            normalEnhance = 0;
    //            eliteEnhance = 0;
    //            dropRank = 2;
    //            recommandCP = 1.2;
    //            break;

    //        case 3:
    //            recommandLv = 6;
    //            rewardExp = 8;
    //            goalMoney = 2000;
    //            enemyStatus = 1.3f;
    //            enemyRespawn = 1.3f;
    //            normalEnhance = 0;
    //            eliteEnhance = 0.04f;
    //            dropRank = 4;
    //            recommandCP = 1.6f;
    //            break;

    //        case 4:
    //            recommandLv = 9;
    //            rewardExp = 20;
    //            goalMoney = 3000;
    //            enemyStatus = 1.6f;
    //            enemyRespawn = 1.6f;
    //            normalEnhance = 0;
    //            eliteEnhance = 0.08f;
    //            dropRank = 6;
    //            recommandCP = 2.5f;
    //            break;

    //        case 5:
    //            recommandLv = 12;
    //            rewardExp = 48;
    //            goalMoney = 4500;
    //            enemyStatus = 2.0f;
    //            enemyRespawn = 2.0f;
    //            normalEnhance = 0.4f;
    //            eliteEnhance = 0.14f;
    //            dropRank = 9;
    //            recommandCP = 3.8f;
    //            break;

    //        case 6:
    //            recommandLv = 15;
    //            rewardExp = 105;
    //            goalMoney = 6500;
    //            enemyStatus = 2.5f;
    //            enemyRespawn = 2.5f;
    //            normalEnhance = 0.08f;
    //            eliteEnhance = 0.2f;
    //            dropRank = 12;
    //            recommandCP = 6.0f;
    //            break;

    //        case 7:
    //            recommandLv = 18;
    //            rewardExp = 210;
    //            goalMoney = 10000;
    //            enemyStatus = 3.1f;
    //            enemyRespawn = 3.1f;
    //            normalEnhance = 0.14f;
    //            eliteEnhance = 0.3f;
    //            dropRank = 15;
    //            recommandCP = 9.0;
    //            break;

    //        case 8:
    //            recommandLv = 20;
    //            rewardExp = 9999;
    //            goalMoney = 15000;
    //            enemyStatus = 3.8f;
    //            enemyRespawn = 3.8f;
    //            normalEnhance = 0.2f;
    //            eliteEnhance = 0.4f;
    //            dropRank = 20;
    //            recommandCP = 14.0f;
    //            break;
    //    }
    //}
}
