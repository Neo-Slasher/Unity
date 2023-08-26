using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    protected EnemyTrashData enemyData;

    //적 데이터
    public double nowHp;

    [SerializeField]
    GameObject character;

    Rigidbody2D enemyRigid;

    protected Vector3 moveDir;
    public bool isStageEnd = false;

    int nowDifficulty;
    public bool isStop = false;         //오브젝트 움직임을 컨트롤하기 위해 만듦
    public bool isAttacked = false;     //공격을 했다면 2초간 true로 변환
    Coroutine moveCoroutine = null;


    //아이템
    public bool isSlow = false;

    //테스트용
    SpriteRenderer enemyRenderer;

    protected void Start()
    {
        nowDifficulty = GameManager.instance.player.difficulty;
        enemyRigid = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();

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
    }

    //캐릭터 위치를 찾기 위해서 NightManager를 통해 character 오브젝트를 받아옴
    public void SetCharacter(GameObject getCharacter)
    {
        character= getCharacter;
    }

    public void EnemyMove()
    {
        if(moveCoroutine == null)
            moveCoroutine = StartCoroutine(EnemyMoveCoroutine());
    }

    IEnumerator EnemyMoveCoroutine()
    {
        Vector3 nowCharPos;
        while(!isStageEnd)
        {
            while (isStop)
                yield return new WaitForSeconds(1);

            nowCharPos = character.transform.position;
            moveDir = nowCharPos - this.transform.position;

            if (moveDir.x >= 0)
                this.transform.localScale = new Vector3(-1, 1, 1);
            else
                this.transform.localScale = new Vector3(1, 1, 1);

            enemyRigid.velocity = moveDir.normalized * SetMoveSpeed(enemyData.moveSpeed);
            yield return new WaitForSeconds(1);
        }
        enemyRigid.velocity = Vector3.zero;
    }

    protected float SetMoveSpeed(double getMoveSpeed)
    {
        float result = 0;
        result = ((float)getMoveSpeed * 25) / 100;

        return result;
    }

    void SetLevelStatus(int level)
    {
        //선택한 난이도에 따라 스테이터스 변경
        enemyData.hitPointMax *= DataManager.instance.difficultyList.difficulty[nowDifficulty].enemyStatus;
        enemyData.hitPoint *= DataManager.instance.difficultyList.difficulty[nowDifficulty].enemyStatus;
        enemyData.attackPower *= DataManager.instance.difficultyList.difficulty[nowDifficulty].enemyStatus;
    }

    public void SetNormalEnemyType(int nowIndex)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch(nowIndex)
        {
            case 0:
                enemyData = new EnemyTrashData(EnemyType.BlackSuitMan);
                break;
            case 1:
                enemyData = new EnemyTrashData(EnemyType.WhiteSuitMan);
                break;
            case 2:
                enemyData = new EnemyTrashData(EnemyType.MachineArmorSoldier);
                break;
        }
    }
    public void SetEliteEnemyType(int nowIndex)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch (nowIndex)
        {
            case 0:
                enemyData = new EnemyTrashData(EnemyType.Red3LegRobot);
                break;
            case 1:
                enemyData = new EnemyTrashData(EnemyType.Blue3LegRobot);
                break;
            case 2:
                enemyData = new EnemyTrashData(EnemyType.Big4LegRobot);
                break;
        }
    }

    //몬스터가 강화되었는지
    public void SetEnforceData(int getLevel, bool isElite = false)
    {
        if(IsEnforce(getLevel,isElite))
        {
            enemyData.monEnforce = true;
            //강화되었으므로 스테이터스 변경
            enemyData.hitPointMax *= 2;
            enemyData.hitPoint *= 2;
            enemyData.attackPower *= 2;
        }
    }

    bool IsEnforce(int getLevel, bool isElite = false)
    {
        double nowProb;
        double randomProb = Random.value;

        if (!isElite)
            nowProb = DataManager.instance.difficultyList.difficulty[nowDifficulty].normalEnhance;
        else
            nowProb = DataManager.instance.difficultyList.difficulty[nowDifficulty].eliteEnhance;

        if (randomProb < nowProb)
            return true;
        else
            return false;
    }

    //캐릭터와 충돌할 경우 해당 적의 공격력을 반환하는 함수
    public double GetEnemyAttackPower()
    {
        if (isAttacked == false)
            return enemyData.attackPower;
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
        double getDamage = 0;
        if (collision.name == "HitBox")
        {
            getDamage = collision.gameObject.GetComponent<HitBox>().getAttackPower;
            collision.gameObject.GetComponent<HitBox>().isAttacked = true;
        }

        else if(collision.name == "CentryBallProjPrefab(Clone)")
        {
            getDamage = collision.GetComponent<Projectile>().projPower;
        }

        else if (collision.tag == "Item")
        {
            Debug.Log(collision.name);
            if(collision.name == "ChargingReaperImage")
            {
                getDamage = collision.transform.parent.GetComponent<ChargingReaper>().reaperAttackDamaege;
            }
        }

        if(getDamage > 0)
        {
            //피흡 있으면 여기서 회복
            character.GetComponent<Character>().AbsorbAttack();
            EnemyMoveBack();

            if (enemyData.hitPoint > getDamage)
            {
                enemyData.hitPoint -= getDamage;
            }
            else
            {
                character.GetComponent<Character>().UpdateKillCount();

                if(this.gameObject.tag == "Normal")
                    character.GetComponent<Character>().UpdateKillNormalCount();
                else if (this.gameObject.tag == "Elite")
                    character.GetComponent<Character>().UpdateKillEliteCount();

                Destroy(this.gameObject);
            }
        }
    }


    public void EnemyDamaged(double getDamage)
    {
        if (getDamage > 0)
        {
            if (enemyData.hitPoint > getDamage)
                enemyData.hitPoint -= getDamage;
            else
            {
                character.GetComponent<Character>().UpdateKillCount();
                Destroy(this.gameObject);
            }
        }
    }

    public Vector3 GetMoveDir()
    {
        return moveDir;
    }

    public void DrugEnemy()
    {
        Vector3 start;
        start = this.transform.position;
        StartCoroutine(DrugEnemyCoroutine(start));
    }

    IEnumerator DrugEnemyCoroutine(Vector3 start)
    {
        Vector3 nowVelocity = enemyRigid.velocity;
        //캐릭터 위치 기준으로 반경 256px안까지 끌어당김
        //while((character.transform.position - this.transform.position).magnitude >= 2f)
        //{
        //    enemyRigid.AddForceAtPosition(moveDir, this.transform.position);
        //    yield return null;
        //}

        //본인 초기 위치 기준 128px 앞으로 당겨짐
        while ((start - this.transform.position).magnitude <= 1.5f)
        {
            enemyRigid.AddForceAtPosition(moveDir, this.transform.position);
            yield return null;
        }
        enemyRigid.velocity = nowVelocity;
    }

    public void ThrustEnemy()
    {
        isStop = true;
        Vector3 start;
        start = this.transform.position;
        StartCoroutine(ThrustEnemyCoroutine(start));
    }

    IEnumerator ThrustEnemyCoroutine(Vector3 start)
    {
        Vector3 nowVelocity = enemyRigid.velocity;
        moveDir = start - character.transform.position;
        enemyRigid.velocity = moveDir.normalized * 5;

        //투명도로 확인하려고 임시로 만들어둠
        Color nowColor = enemyRenderer.color;
        nowColor.a = 0.5f;
        enemyRenderer.color = nowColor;

        //적 초기 위치 기준 256px 튕김
        while ((character.transform.position - this.transform.position).magnitude <= 2f)
        {
            //enemyRigid.AddForceAtPosition(moveDir, this.transform.position);
            yield return null;
        }
        nowColor.a = 1f;
        enemyRenderer.color = nowColor;
        enemyRigid.velocity = nowVelocity;
        isStop = false;
    }

    public double ReturnEnemyMoveSpeed()
    {
        return enemyData.moveSpeed;
    }

    public void EnemyStop()
    {
        enemyRigid.velocity = Vector3.zero;
        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    public double ReturnEnemyHitPointMax()
    {
        return enemyData.hitPointMax;
    }

    public void SetEnemyMoveSpeed(double getEnemySpeed)
    {
        enemyData.moveSpeed = getEnemySpeed;
    }

    public void DebuggingFunc()
    {
        Debug.Log("HitPoint: " + enemyData.hitPoint);
    }

    void EnemyMoveBack()
    {
        if (character.GetComponent<Character>().isMoveBackOn && !enemyData.monResist)
        {
            isStop = true;
            Vector3 start;
            start = this.transform.position;
            StartCoroutine(EnemyMoveBackCoroutine(start));
        }
    }

    IEnumerator EnemyMoveBackCoroutine(Vector3 start)
    {
        Vector3 nowVelocity = enemyRigid.velocity;
        moveDir = start - character.transform.position;
        enemyRigid.velocity = moveDir.normalized * 5; Debug.Log(moveDir + " " + enemyRigid.velocity);

        //투명도로 확인하려고 임시로 만들어둠
        Color nowColor = enemyRenderer.color;
        nowColor.a = 0.5f;
        enemyRenderer.color = nowColor;

        //적 초기 위치 기준 150px 튕김
        while ((start - this.transform.position).magnitude <= 1.5f)
        {
            //enemyRigid.AddForceAtPosition(moveDir, this.transform.position);
            yield return null;
        }Debug.Log("end");
        nowColor.a = 1f;
        enemyRenderer.color = nowColor;
        enemyRigid.velocity = nowVelocity;
        isStop = false; 
    }
}
