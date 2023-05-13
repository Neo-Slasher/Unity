using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    EnemyTrashData enemyTrashData;

    //적 데이터
    public int nowHp;

    [SerializeField]
    GameObject character;

    Rigidbody2D enemyRigid;

    Vector3 moveDir;
    public bool isStageEnd = false;

    public bool isAttacked = false; //공격을 했다면 2초간 true로 변환

    private void Start()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        EnemyMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDamaged(collision);
    }

    public void SetEnemyStatus()
    {
        //적 json 데이터 받아오기

        //난이도 및 암살 단계에 따른 스테이터스 변화
        SetLevelStatus(0);
        SetAssassinationStatus(0);

        nowHp = enemyTrashData.hitPoint;
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
        while(!isStageEnd)
        {
            nowCharPos = character.transform.position;
            moveDir = nowCharPos - this.transform.position;
            enemyRigid.velocity = moveDir.normalized * SetMoveSpeed(enemyTrashData.moveSpeed);
            yield return new WaitForSeconds(1);
        }
        enemyRigid.velocity = Vector3.zero;
    }

    float SetMoveSpeed(int getMoveSpeed)
    {
        float result = 0;
        result = ((float)getMoveSpeed * 25) / 128;

        return result;
    }

    void SetLevelStatus(int level)
    {
        //선택한 난이도에 따라 스테이터스 변경
    }

    void SetAssassinationStatus(int assassinationLevel)
    {
        //선택한 암살에 따라 스테이터스 변경
    }

    //캐릭터와 충돌할 경우 해당 적의 공격력을 반환하는 함수
    public int GetEnemyAttackPower()
    {
        if (isAttacked == false)
            return enemyTrashData.attackPower;
        else return 0;
    }

    //공격하고 2초동안 공격 무시
    public void SetIsAttacked()
    {
        isAttacked = true;
        StartCoroutine(SetIsAttackedCoroutine());
    }

    IEnumerator SetIsAttackedCoroutine()
    {
        yield return new WaitForSeconds(2f);
        isAttacked = false;
    }

    void EnemyDamaged(Collider2D collision)
    {
        if (collision.name == "HitBox")
        {
            int getDamage = collision.gameObject.GetComponent<HitBox>().getOffensePower;
            collision.gameObject.GetComponent<HitBox>().isAttacked = true;

            if (nowHp > getDamage)
                nowHp -= getDamage;
            else
                Destroy(this.gameObject);
        }
    }
}
