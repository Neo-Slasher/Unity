using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    EnemyStatus enemyStatus;

    public void SetEnemyStatus()
    {
        //�� json ������ �޾ƿ���
        SetLevelStatus(0);
        SetAssassinationStatus(0);
    }

    void SetLevelStatus(int level)
    {
        //������ ���̵��� ���� �������ͽ� ����
    }

    void SetAssassinationStatus(int assassinationLevel)
    {
        //������ �ϻ쿡 ���� �������ͽ� ����
    }
}
