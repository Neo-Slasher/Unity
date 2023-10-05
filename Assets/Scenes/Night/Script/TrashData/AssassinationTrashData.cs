using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssassinationTrashData
{
    //
    public int normalReward;        //일반 적 현상금
    public int eliteReward;         //정예 적 현상금
    public int normalDropRate;      //일반 드랍 확률
    public int eliteDropRate;       //정예 드랍 확률
    public double normal1Spawn;        //일반 1 스폰수
    public double normal2Spawn;        //일반 2 스폰수
    public double normal3Spawn;        //일반 3 스폰수
    public double elite1Spawn;         //정예 1 스폰수
    public double elite2Spawn;         //정예 2 스폰수
    public double elite3Spawn;         //정예 3 스폰수
    public int stageDropRank;       //암살 드랍 랭크
    public int stageRecCP;          //권장 전투력

    //public AssassinationTrashData(int nowAssassination = 1)
    //{
    //    SetAssassinationTrashData(nowAssassination);
    //}

    //void SetAssassinationTrashData(int nowAssassination = 1)
    //{
    //    switch(nowAssassination)
    //    {
    //        case 1:
    //            normalReward = 0;
    //            eliteReward = 0;
    //            normalDropRate = 0;
    //            eliteDropRate = 0;
    //            normal1Spawn = 1.5f;
    //            normal2Spawn = 0.3f;
    //            normal3Spawn = 0.0f;
    //            elite1Spawn = 0.5f;
    //            elite2Spawn = 0.1f;
    //            elite3Spawn = 0.0f;
    //            stageDropRank = 0;
    //            stageRecCP = 0;
    //            break;

    //        case 2:
    //            normalReward = 0;
    //            eliteReward = 0;
    //            normalDropRate = 0;
    //            eliteDropRate = 0;
    //            normal1Spawn = 1.5f;
    //            normal2Spawn = 0.3f;
    //            normal3Spawn = 0.0f;
    //            elite1Spawn = 0.5f;
    //            elite2Spawn = 0.1f;
    //            elite3Spawn = 0.0f;
    //            stageDropRank = 0;
    //            stageRecCP = 0;
    //            break;

    //        case 3:
    //            normalReward = 0;
    //            eliteReward = 0;
    //            normalDropRate = 0;
    //            eliteDropRate = 0;
    //            normal1Spawn = 1.5f;
    //            normal2Spawn = 0.3f;
    //            normal3Spawn = 0.0f;
    //            elite1Spawn = 0.5f;
    //            elite2Spawn = 0.1f;
    //            elite3Spawn = 0.0f;
    //            stageDropRank = 0;
    //            stageRecCP = 0;
    //            break;

    //        case 4:
    //            normalReward = 0;
    //            eliteReward = 0;
    //            normalDropRate = 0;
    //            eliteDropRate = 0;
    //            normal1Spawn = 1.5f;
    //            normal2Spawn = 0.3f;
    //            normal3Spawn = 0.0f;
    //            elite1Spawn = 0.5f;
    //            elite2Spawn = 0.1f;
    //            elite3Spawn = 0.0f;
    //            stageDropRank = 0;
    //            stageRecCP = 0;
    //            break;
    //    }
    //}
}
