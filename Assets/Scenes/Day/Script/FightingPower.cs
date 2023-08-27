using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightingPower : MonoBehaviour
{
    public double hitPoint;
    public double moveSpeed;
    public double attackPower;
    public double attackSpeed;
    public double attackRange;

    public int currentCP;
    public int selectNum;
    public Player Player;

    public TMP_Text money;
    public TMP_Text currentcp;

    public TMP_Text stacks;
    public TMP_Text diffs;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameManager.instance.player;
        
        CalculatePower();
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
        PrintingDiff();
    }

    public void Printing()
    {
        CalculatePower();
        
        money.text = Player.money.ToString() + "a / " + DataManager.instance.difficultyList.difficulty[Player.difficulty].goalMoney + "a";
        currentcp.text = currentCP.ToString();

        stacks.text = Player.maxHp.ToString() + "\n";
        stacks.text += Player.moveSpeed.ToString() + "\n";
        stacks.text += Player.attackSpeed.ToString() + "\n";
        stacks.text += Player.attackPower.ToString() + "\n";
        stacks.text += Player.attackRange.ToString() + "\n";
        stacks.text += "\n" + Player.startMoney.ToString() + "\n";
        stacks.text += Player.earnMoney.ToString() + "\n";
        
    }
    public void PrintingDiff()
    {
        Printing();
       
        if(DataManager.instance.equipmentList.equipment[selectNum].attackPower > 0)
            diffs.text = "\n" + "+" +  DataManager.instance.equipmentList.equipment[selectNum].attackPower.ToString() + "\n";
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackPower < 0)
            diffs.text = "\n" + DataManager.instance.equipmentList.equipment[selectNum].attackPower.ToString() + "\n";
        
        if(DataManager.instance.equipmentList.equipment[selectNum].attackSpeed > 0)
            diffs.text += "+" + DataManager.instance.equipmentList.equipment[selectNum].attackSpeed.ToString() + "\n";
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackSpeed < 0)
            diffs.text += DataManager.instance.equipmentList.equipment[selectNum].attackSpeed.ToString() + "\n";
        
        if(DataManager.instance.equipmentList.equipment[selectNum].attackRange > 0)
            diffs.text += "+" + DataManager.instance.equipmentList.equipment[selectNum].attackRange.ToString() + "\n";
        else if(DataManager.instance.equipmentList.equipment[selectNum].attackRange < 0)
            diffs.text += DataManager.instance.equipmentList.equipment[selectNum].attackRange.ToString() + "\n";

        if(DataManager.instance.equipmentList.equipment[selectNum].moveSpeed > 0)
            diffs.text += "+" + DataManager.instance.equipmentList.equipment[selectNum].moveSpeed.ToString() + "\n";
        else if(DataManager.instance.equipmentList.equipment[selectNum].moveSpeed < 0)
            diffs.text += DataManager.instance.equipmentList.equipment[selectNum].moveSpeed.ToString() + "\n";
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
