using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    EnemyStatus enemyStatus;

    [SerializeField]
    GameObject character;

    Rigidbody enemyRigid;

    Vector3 moveDir;

    public void SetEnemyStatus()
    {
        //�� json ������ �޾ƿ���

        //���̵� �� �ϻ� �ܰ迡 ���� �������ͽ� ��ȭ
        SetLevelStatus(0);
        SetAssassinationStatus(0);

        //�����Ⱚ ����
        enemyStatus.enemyMoveSpeed = 1;
    }

    private void Start()
    {
        enemyRigid = GetComponent<Rigidbody>();
        EnemyMove();
    }

    //ĳ���� ��ġ�� ã�� ���ؼ� NightManager�� ���� character ������Ʈ�� �޾ƿ�
    public void SetCharacter(GameObject getCharacter)
    {
        character= getCharacter;
    }

    void EnemyMove()
    {
        StartCoroutine(EnemyMoveCoroutine());
    }

    IEnumerator EnemyMoveCoroutine()
    {
        Vector3 nowCharPos;
        while(true)
        {
            nowCharPos = character.transform.position;
            moveDir = nowCharPos - this.transform.position;
            enemyRigid.velocity = moveDir.normalized * enemyStatus.enemyMoveSpeed;
            yield return new WaitForSeconds(1);
        }
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
