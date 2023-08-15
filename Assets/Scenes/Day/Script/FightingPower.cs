using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameManager.instance.player;
        Player.maxHp = hitPoint;
        Player.attackPower = attackPower;
        Player.attackSpeed = attackSpeed;
        Player.attackRange = attackRange;
        Player.moveSpeed = moveSpeed;
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
