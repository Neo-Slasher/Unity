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
        //적 json 데이터 받아오기

        //난이도 및 암살 단계에 따른 스테이터스 변화
        SetLevelStatus(0);
        SetAssassinationStatus(0);

        //쓰레기값 넣음
        enemyStatus.enemyMoveSpeed = 1;
    }

    private void Start()
    {
        enemyRigid = GetComponent<Rigidbody>();
        EnemyMove();
    }

    //캐릭터 위치를 찾기 위해서 NightManager를 통해 character 오브젝트를 받아옴
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
        //선택한 난이도에 따라 스테이터스 변경
    }

    void SetAssassinationStatus(int assassinationLevel)
    {
        //선택한 암살에 따라 스테이터스 변경
    }
}
