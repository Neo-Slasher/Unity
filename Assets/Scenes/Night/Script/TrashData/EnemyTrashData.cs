using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//임시로 만들어진 적 종류 넣어뒀음
public enum EnemyType
{
    None, BlackSuitMan, WhiteSuitMan, MachineArmorSoldier
        , Red3LegRobot, Blue3LegRobot, Big4LegRobot
}

[System.Serializable]
public class EnemyTrashData
{
    public int monIdx;

    public double monWidth;     //적 히트박스 가로
    public double monLength;    //적 히트박스 세로

    //public int monImage;
    //public int monSound;

    public double hitPointMax;
    public double hitPoint;
    public double moveSpeed;
    public double attackPower;
    public bool dashAble;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;

    public bool monElite;       //엘리트 적인가?
    public bool monEnforce;     //강화되었는가?
    public bool monResist;      //본인이 밀려나지 않는가?
    public bool monKnockback;   //플레이어를 넉백하는가?
    public bool monProj;        //투사체 발사여부


    public EnemyTrashData(EnemyType getEnemyType = EnemyType.None)
    {
        SetEnemyTypeData(getEnemyType);
    }

    public void SetEnemyTypeData(EnemyType getEnemyType)
    {
        switch(getEnemyType)
        {
            case EnemyType.None:
                monIdx = -1;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 0;
                hitPoint = 0;
                moveSpeed = 0;
                attackPower = 0;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = false;        //투사체 발사여부
                break;

            case EnemyType.BlackSuitMan:
                monIdx = 1;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 8;
                hitPoint = 8;
                moveSpeed = 5;
                attackPower = 2;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = false;        //투사체 발사여부
                break;
            case EnemyType.WhiteSuitMan:
                monIdx = 2;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 18;
                hitPoint = 18;
                moveSpeed = 7;
                attackPower = 4;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = false;        //투사체 발사여부
                break;

            case EnemyType.MachineArmorSoldier:
                monIdx = 3;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 40;
                hitPoint = 40;
                moveSpeed = 10;
                attackPower = 7;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = false;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = true;      //본인이 밀려나지 않는가?
                monKnockback = true;   //플레이어를 넉백하는가?
                monProj = false;        //투사체 발사여부
                break;

            case EnemyType.Red3LegRobot:
                monIdx = 4;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 20;
                hitPoint = 20;
                moveSpeed = 8;
                attackPower = 5;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = false;        //투사체 발사여부
                break;

            case EnemyType.Blue3LegRobot:
                monIdx = 5;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 45;
                hitPoint = 45;
                moveSpeed = 10;
                attackPower = 9;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = true;        //투사체 발사여부
                break;

            case EnemyType.Big4LegRobot:
                monIdx = 6;
                monWidth = 125;     //적 히트박스 가로
                monLength = 348;    //적 히트박스 세로

                //monImage;
                //monSound;

                hitPointMax = 100;
                hitPoint = 100;
                moveSpeed = 12;
                attackPower = 15;
                dashAble = false;
                dashFreq = 0;
                dashSpeed = 0;
                dashDuration = 0;

                monElite = true;       //엘리트 적인가?
                monEnforce = false;     //강화되었는가?
                monResist = false;      //본인이 밀려나지 않는가?
                monKnockback = false;   //플레이어를 넉백하는가?
                monProj = true;        //투사체 발사여부
                break;
        }
    }
}
