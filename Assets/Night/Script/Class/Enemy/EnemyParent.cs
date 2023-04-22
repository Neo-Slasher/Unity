using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    EnemyStatus enemyStatus;

    public void SetEnemyStatus()
    {
        //적 json 데이터 받아오기
        SetLevelStatus(0);
        SetAssassinationStatus(0);
    }

    void SetLevelStatus(int level)
    {
        //선택한 난이도에 따라 스테이터스 변경
    }

    void SetAssassinationStatus(int assassinationLevel)
    {
        //선택한 암살에 따라 스테이터스 변경
    }
}
