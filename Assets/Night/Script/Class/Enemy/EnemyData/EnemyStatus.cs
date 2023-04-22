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

    public EnemyStatus()
    {
        enemyHP = 0;
        enemyMoveSpeed = 0;
        enemyDamage = 0;

        isUpgrade = false;
    }
}
