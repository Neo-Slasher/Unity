using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTrashData
{
    public int diff;                    //난이도 
    public int diffRecLvl;              //권장레벨  
    public int diffExp;                 //보상 Exp
    public int diffGoal;                //목표 알파
    public double diffStat;             //적 스텟 배수
    public double diffPop;              //적 개체 배수
    public double diffNormalEnforce;    //일반 강화 확률
    public double diffEliteEnforce;     //엘리트 강화 확률
    public int diffDropRank;            //난이도 드랍 랭크
    public double diffRecCP;            //권장 CP 랭크

    public LevelTrashData(int getLevel = 1) 
    { 
        SetLevelData(getLevel);
    }

    void SetLevelData(int getLevel)
    {
        diff = getLevel;
        switch(getLevel)
        {
            case 1:
                diffRecLvl = 1;
                diffExp = 1;
                diffGoal = 1000;
                diffStat = 1.0f;
                diffPop = 1.0f;
                diffNormalEnforce = 0;
                diffEliteEnforce = 0;
                diffDropRank = 1;
                diffRecCP = 1.0;
                break;

            case 2:
                diffRecLvl = 3;
                diffExp = 3;
                diffGoal = 1500;
                diffStat = 1.1f;
                diffPop = 1.1f;
                diffNormalEnforce = 0;
                diffEliteEnforce = 0;
                diffDropRank = 2;
                diffRecCP = 1.2;
                break;

            case 3:
                diffRecLvl = 6;
                diffExp = 8;
                diffGoal = 2000;
                diffStat = 1.3f;
                diffPop = 1.3f;
                diffNormalEnforce = 0;
                diffEliteEnforce = 0.04f;
                diffDropRank = 4;
                diffRecCP = 1.6f;
                break;

            case 4:
                diffRecLvl = 9;
                diffExp = 20;
                diffGoal = 3000;
                diffStat = 1.6f;
                diffPop = 1.6f;
                diffNormalEnforce = 0;
                diffEliteEnforce = 0.08f;
                diffDropRank = 6;
                diffRecCP = 2.5f;
                break;

            case 5:
                diffRecLvl = 12;
                diffExp = 48;
                diffGoal = 4500;
                diffStat = 2.0f;
                diffPop = 2.0f;
                diffNormalEnforce = 0.4f;
                diffEliteEnforce = 0.14f;
                diffDropRank = 9;
                diffRecCP = 3.8f;
                break;

            case 6:
                diffRecLvl = 15;
                diffExp = 105;
                diffGoal = 6500;
                diffStat = 2.5f;
                diffPop = 2.5f;
                diffNormalEnforce = 0.08f;
                diffEliteEnforce = 0.2f;
                diffDropRank = 12;
                diffRecCP = 6.0f;
                break;

            case 7:
                diffRecLvl = 18;
                diffExp = 210;
                diffGoal = 10000;
                diffStat = 3.1f;
                diffPop = 3.1f;
                diffNormalEnforce = 0.14f;
                diffEliteEnforce = 0.3f;
                diffDropRank = 15;
                diffRecCP = 9.0;
                break;

            case 8:
                diffRecLvl = 20;
                diffExp = 9999;
                diffGoal = 15000;
                diffStat = 3.8f;
                diffPop = 3.8f;
                diffNormalEnforce = 0.2f;
                diffEliteEnforce = 0.4f;
                diffDropRank = 20;
                diffRecCP = 14.0f;
                break;
        }
    }
}
