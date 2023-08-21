using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightingPower : MonoBehaviour
{
    public double hitPoint = 20;
    public double moveSpeed = 10;
    public double attackPower = 5;
    public double attackSpeed = 10;
    public double attackRange = 10;

    public int currentCP;
    public int selectNum;
    public Player Player;

    public TMP_Text hitpoint;
    public TMP_Text movespeed;
    public TMP_Text attackpower;
    public TMP_Text attackspeed;
    public TMP_Text attackrange;
    
    public TMP_Text startmoney;
    public TMP_Text earnmoney;

    public TMP_Text mdiff;
    public TMP_Text pdiff;
    public TMP_Text sdiff;
    public TMP_Text rdiff;

    public TMP_Text money;
    public TMP_Text currentcp;
    
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameManager.instance.player;
        //Player.maxHp = hitPoint;
        //Player.attackPower = attackPower;
        //Player.attackSpeed = attackSpeed;
        //Player.attackRange = attackRange;
        //Player.moveSpeed = moveSpeed;
        Printing();
    }

    public void ChangingPower()
    {
        hitPoint = Player.maxHp;
        attackPower = Player.attackPower;
        attackSpeed = Player.attackSpeed;
        attackRange = Player.attackRange;
        moveSpeed = Player.moveSpeed;

        attackPower += DataManager.instance.equipmentList.equipment[selectNum].attackPower;
        attackSpeed += DataManager.instance.equipmentList.equipment[selectNum].attackSpeed;
        attackRange += DataManager.instance.equipmentList.equipment[selectNum].attackRange;
        moveSpeed += DataManager.instance.equipmentList.equipment[selectNum].moveSpeed;

        
        SwitchInfo();
        CalculatePower();
    }

    public void Printing()
    {
        hitpoint.text = Player.maxHp.ToString();
        movespeed.text = Player.moveSpeed.ToString();
        attackspeed.text = Player.attackSpeed.ToString();
        attackpower.text = Player.attackPower.ToString();
        attackrange.text = Player.attackRange.ToString();
        
        startmoney.text = Player.startMoney.ToString();
        earnmoney.text = Player.earnMoney.ToString();

        money.text = Player.money.ToString();
        currentcp.text = currentCP.ToString();
    }
    public void PrintingDiff()
    {
        Printing();
        if(DataManager.instance.equipmentList.equipment[selectNum].attackPower > 0)
            pdiff.text = "+" + DataManager.instance.equipmentList.equipment[selectNum].attackPower.ToString();
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackPower < 0)
            pdiff.text = DataManager.instance.equipmentList.equipment[selectNum].attackPower.ToString();
        
        if(DataManager.instance.equipmentList.equipment[selectNum].attackSpeed > 0)
            sdiff.text = "+" + DataManager.instance.equipmentList.equipment[selectNum].attackSpeed.ToString();
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackSpeed < 0)
            sdiff.text = DataManager.instance.equipmentList.equipment[selectNum].attackSpeed.ToString();
        
        if(DataManager.instance.equipmentList.equipment[selectNum].attackRange > 0)
            rdiff.text = "+" + DataManager.instance.equipmentList.equipment[selectNum].attackRange.ToString();
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackRange < 0)
            rdiff.text = DataManager.instance.equipmentList.equipment[selectNum].attackRange.ToString();

        if(DataManager.instance.equipmentList.equipment[selectNum].moveSpeed > 0)
            mdiff.text = "+" + DataManager.instance.equipmentList.equipment[selectNum].moveSpeed.ToString();
        else if(DataManager.instance.equipmentList.equipment[selectNum].moveSpeed < 0)
            mdiff.text = DataManager.instance.equipmentList.equipment[selectNum].moveSpeed.ToString();
    }
    void CalculatePower()
    {
        hitPoint = Player.maxHp;
        attackPower = Player.attackPower;
        attackSpeed = Player.attackSpeed;
        attackRange = Player.attackRange;
        moveSpeed = Player.moveSpeed;
        currentCP = (int)((hitPoint * 1.5) + (moveSpeed * 3) + (attackPower * attackSpeed * attackRange * 0.02));
        Debug.Log("currentCP " + currentCP);
    }

    void SwitchInfo()
    {
        Player.maxHp = hitPoint;
        Player.attackPower = attackPower;
        Player.attackSpeed = attackSpeed;
        Player.attackRange = attackRange;
        Player.moveSpeed = moveSpeed;
    }

}
