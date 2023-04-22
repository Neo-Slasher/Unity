using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatus
{
    public int enemyHP;
    public int enemyMoveSpeed;
    public int enemyDamage;

    public bool isUpgrade;  //���� ��ȭ�Ǿ��� ��

    public EnemyStatus()
    {
        enemyHP = 0;
        enemyMoveSpeed = 0;
        enemyDamage = 0;

        isUpgrade = false;
    }
}
