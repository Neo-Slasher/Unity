using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // status
    private double maxHp = 20.0;
    private double hp = 20.0;
    private int money = 0;

    private double moveSpeed = 10.0;

    private bool dashable = false;
    private double dashFreq = 0.0;
    private double dashSpeed = 0.0;
    private double dashDuration = 0.0;

    private double attackPower = 5.0;
    private double attackSpeed = 10.0;
    private double attackRange = 10.0;
    private double shieldDuration = 0.0;
    private double shieldPoint = 0.0;

    private double immuneDuration = 0.0;
    private int immuneCount = 0;

    private double damageReduation = 0.0;
    private double dealOnHp = 0.0;
    private double healByHit = 0.0;
    private double hpRegen = 0.0;


    private int shopSlot = 1;
    private int shopMinRank = 0;
    private int shopMaxRank = 1;

    private int dropRank = 0;
    private double dropRate = 1.0;

    private int itemSlot = 1;
    

    private int level = 1;
    private int curExp = 0;
    private int reqExp = 2;



    public double getMaxHp() {
        return maxHp;    
    }

    public double getHp() {
        return hp;
    }

    public int getMoney() {
        return money;
    }   

    public double getMoveSpeed() {
        return moveSpeed;
    }

    public bool getDashable() {
        return dashable;
    }

    public double getDashFreq() {
        return dashFreq;
    }

    public double getDashSpeed() {
        return dashSpeed;
    }

    public double getDashDuration() {
        return dashDuration;
    }

    public double getAttackPower() {
        return attackPower;
    }

    public double getAttackSpeed() {
        return attackSpeed;
    }

    public double getAttackRange() {
        return attackRange;
    }

    public double getShieldDuration() {
        return shieldDuration;
    }

    public double getShieldPoint() {
        return shieldPoint;
    }

    public double getImmuneDuration() {
        return immuneDuration;
    }

    public int getImmuneCount() {
        return immuneCount;
    }

    public double getDamageReduation() {
        return damageReduation;
    }

    public double getDealOnHp() {
        return dealOnHp;
    }

    public double getHealByHit() {
        return healByHit;
    }

    public double getHpRegen() {
        return hpRegen;
    }

    public int getShopSlot() {
        return shopSlot;
    }

    public int getShopMinRank() {
        return shopMinRank;
    }

    public int getShopMaxRank() {
        return shopMaxRank;
    }

    public double getDropRank() {
        return dropRank;
    }

    public double getDropRate() {
        return dropRate;
    }

    public int getItemSlot() {
        return itemSlot;
    }

    public int getLevel() {
        return level;
    }

    public int getCurExp() {
        return curExp;
    }

    public int getReqExp() {
        return reqExp;
    }


}
