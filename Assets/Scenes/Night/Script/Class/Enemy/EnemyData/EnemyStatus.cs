using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatus
{
    public int enemyHP;
    public int enemyMoveSpeed;
    public int enemyDamage;

    public bool isUpgrade;  //적이 강화되었을 때


    public double hitPointMax;
    public double hitPoint;
    public double moveSpeed;
    public double attackPower;
    public bool dashAble;
    public double dashFreq;
    public double dashSpeed;
    public double dashDuration;

    public int monIdx;          
    public double monWidth;     //적 히트박스 가로
    public double monLength;    //적 히트박스 세로

    public bool monElite;       //엘리트 적인가?
    public bool monEnforce;     //강화되었는가?
    public bool monResist;      //본인이 밀려나지 않는가?
    public bool monKnockback;   //플레이어를 넉백하는가?
    public bool monProj;        //투사체 발사여부

    public int monImage;
    public int monSound;

    public EnemyStatus()
    {
        enemyHP = 0;
        enemyMoveSpeed = 0;
        enemyDamage = 0;

        isUpgrade = false;
    }
}
