using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParent : MonoBehaviour
{
    [SerializeField]
    protected EnemyTrashData enemyTrashData;
    public LevelTrashData levelTrashData;

    //적 데이터
    public double nowHp;

    [SerializeField]
    GameObject character;

    Rigidbody2D enemyRigid;

    protected Vector3 moveDir;
    public bool isStageEnd = false;
    
    public bool isStop = false;         //오브젝트 움직임을 컨트롤하기 위해 만듦
    public bool isAttacked = false;     //공격을 했다면 2초간 true로 변환
    Coroutine moveCoroutine = null;

    public TextMeshPro tempEnemyName;


    //아이템
    public bool isSlow = false;

    //테스트용
    SpriteRenderer enemyRenderer;

    protected void Start()
    {
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
            enemyRigid.velocity = moveDir.normalized * SetMoveSpeed(enemyTrashData.moveSpeed);
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
        enemyTrashData.hitPointMax *= levelTrashData.diffStat;
        enemyTrashData.hitPoint *= levelTrashData.diffStat;
        enemyTrashData.attackPower *= levelTrashData.diffStat;
    }

    public void SetNormalEnemyType(int nowIndex)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch(nowIndex)
        {
            case 0:
                enemyTrashData = new EnemyTrashData(EnemyType.BlackSuitMan);
                tempEnemyName.text = "Black Suit";
                break;
            case 1:
                enemyTrashData = new EnemyTrashData(EnemyType.WhiteSuitMan);
                tempEnemyName.text = "White Suit";
                break;
            case 2:
                enemyTrashData = new EnemyTrashData(EnemyType.MachineArmorSoldier);
                tempEnemyName.text = "Machine Armor";
                break;
        }
    }
    public void SetEliteEnemyType(int nowIndex)
    {
        //선택한 암살에 따라 나오는 몬스터 타입 변경
        switch (nowIndex)
        {
            case 0:
                enemyTrashData = new EnemyTrashData(EnemyType.Red3LegRobot);
                tempEnemyName.text = "Red 3Leg";
                break;
            case 1:
                enemyTrashData = new EnemyTrashData(EnemyType.Blue3LegRobot);
                tempEnemyName.text = "Blue 3Leg";
                break;
            case 2:
                enemyTrashData = new EnemyTrashData(EnemyType.Big4LegRobot);
                tempEnemyName.text = "Big 4Leg";
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
        double getDamage = 0;
        if (collision.name == "HitBox")
        {
            getDamage = collision.gameObject.GetComponent<HitBox>().getAttackPower;
            collision.gameObject.GetComponent<HitBox>().isAttacked = true;
        }

        else if(collision.name == "CentryBallProjPrefab(Clone)")
        {
            getDamage = character.GetComponent<Character>().ReturnCharacterAttackPower();
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

            if (enemyTrashData.hitPoint > getDamage)
            {
                enemyTrashData.hitPoint -= getDamage;
            }
            else
            {
                character.GetComponent<Character>().UpdateKillCount();
                Destroy(this.gameObject);
            }
        }
    }


    public void EnemyDamaged(double getDamage)
    {
        if (getDamage > 0)
        {
            if (enemyTrashData.hitPoint > getDamage)
                enemyTrashData.hitPoint -= getDamage;
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
        while ((start - this.transform.position).magnitude <= 1f)
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
        return enemyTrashData.moveSpeed;
    }

    public void EnemyStop()
    {
        enemyRigid.velocity = Vector3.zero;
        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    public double ReturnEnemyHitPointMax()
    {
        return enemyTrashData.hitPointMax;
    }

    public void SetEnemyMoveSpeed(double getEnemySpeed)
    {
        enemyTrashData.moveSpeed = getEnemySpeed;
    }

    public void DebuggingFunc()
    {
        Debug.Log("HitPoint: " + enemyTrashData.hitPoint);
    }

    void EnemyMoveBack()
    {
        if (character.GetComponent<Character>().isMoveBackOn && !enemyTrashData.monResist)
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
