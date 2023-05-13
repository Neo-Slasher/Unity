using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTrashData
{
    public int hitPoint;    //enemyHP
    public int moveSpeed;
    public int attackPower;

    public EnemyTrashData()
    {
        hitPoint = 8;
        moveSpeed = 5;
        attackPower = 2;
    }
}
