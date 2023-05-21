using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    EnemyTrashData enemyTrashData;
    public LevelTrashData levelTrashData;

    //적 데이터
    public double nowHp;

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

    public void SetEnemyStatus(int getLevel = 1)
    {
        //난이도에 따른 스테이터스 변화
        SetLevelStatus(getLevel);

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

    float SetMoveSpeed(double getMoveSpeed)
    {
        float result = 0;
        result = ((float)getMoveSpeed * 25) / 128;

        return result;
    }

    void SetLevelStatus(int level)
    {
        //선택한 난이도에 따라 스테이터스 변경
        enemyTrashData.hitPointMax *= levelTrashData.diffStat;
        enemyTrashData.hitPoint *= levelTrashData.diffStat;
        enemyTrashData.attackPower *= levelTrashData.diffStat;
    }

    public void SetNormalAssassinationType(int assassinationLevel)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch(assassinationLevel)
        {
            case 1:
                enemyTrashData = new EnemyTrashData(EnemyType.BlackSuitMan);
                break;
            case 2:
                enemyTrashData = new EnemyTrashData(EnemyType.WhiteSuitMan);
                break;
            case 3:
                enemyTrashData = new EnemyTrashData(EnemyType.MachineArmorSoldier);
                break;
        }
    }
    public void SetEliteAssassinationType(int assassinationLevel)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch (assassinationLevel)
        {
            case 1:
                enemyTrashData = new EnemyTrashData(EnemyType.Red3LegRobot);
                break;
            case 2:
                enemyTrashData = new EnemyTrashData(EnemyType.Blue3LegRobot);
                break;
            case 3:
                enemyTrashData = new EnemyTrashData(EnemyType.Big4LegRobot);
                break;
        }
    }

    //몬스터가 강화되었는지
    public void SetEnforceData(int getLevel, bool isElite = false)
    {
        if(IsEnforce(getLevel,isElite))
        {
            enemyTrashData.monEnforce = true;
            //강화되었으므로 스테이터스 변경
            enemyTrashData.hitPointMax *= 2;
            enemyTrashData.hitPoint *= 2;
            enemyTrashData.attackPower *= 2;
        }
    }

    bool IsEnforce(int getLevel, bool isElite = false)
    {
        double nowProb;
        double randomProb = Random.value;

        if (!isElite)
            nowProb = levelTrashData.diffNormalEnforce;
        else
            nowProb = levelTrashData.diffEliteEnforce;

        if (randomProb < nowProb)
            return true;
        else
            return false;
    }

    //캐릭터와 충돌할 경우 해당 적의 공격력을 반환하는 함수
    public double GetEnemyAttackPower()
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
