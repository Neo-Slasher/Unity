using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager;
    [SerializeField]
    TimerManager timerManager;
    [SerializeField]
    GameObject character;
    KillData killdata;  //죽인 몬스터 마리수 카운트

    //UI
    [SerializeField]
    GameObject endPopup;

    //몬스터 출현 등에 사용할 예정
    [SerializeField]
    GameObject[] normalEnemyPrefabArr;
    [SerializeField]
    GameObject[] eliteEnemyPrefabArr;

    NormalEnemy[] normalEnemyArr;
    EliteEnemy[] eliteEnemyArr;
    int normalEnemyCount = 3;
    int eliteEnemyCount = 3;

    Vector3 nowCharPos;
    [SerializeField]
    Transform enemyCloneParent;

    //전투시 필요한 데이터
    public bool isStageEnd = false; //밤이 끝났는지 알아보는 변수

    //tempData
    public int nowLevel = 1;
    public int nowAssassination = 1;
    [SerializeField]
    LevelTrashData leveltrashData;
    [SerializeField]
    AssassinationTrashData assassinationTrashData;

    public int killCount = 0;

    private void Start()
    {
        leveltrashData = new LevelTrashData(nowLevel);
        assassinationTrashData = new AssassinationTrashData(nowAssassination);

        //적 배열 생성
        normalEnemyArr = new NormalEnemy[normalEnemyCount];
        eliteEnemyArr = new EliteEnemy[eliteEnemyCount];

        SetEnemyArrData();

        //몬스터 생성 함수
        //InstantiateEnemy();
        TestEnemy(); TestEnemy();
    }

    void SetEnemyArrData()
    {
        for(int i =0; i<normalEnemyCount; i++)
        {
            normalEnemyArr[i] = normalEnemyPrefabArr[i].GetComponent<NormalEnemy>();
            normalEnemyArr[i].SetCharacter(character);

            //임시로 박아둠
            normalEnemyArr[i].levelTrashData = leveltrashData;

            normalEnemyArr[i].SetNormalEnemyType(i);    //적들의 기본데이터 불러오기
            normalEnemyArr[i].SetEnemyStatus(nowLevel);
        }

        for (int i = 0; i < eliteEnemyCount; i++)
        {
            eliteEnemyArr[i] = eliteEnemyPrefabArr[i].GetComponent<EliteEnemy>();
            eliteEnemyArr[i].SetCharacter(character);

            //임시로 박아둠
            eliteEnemyArr[i].levelTrashData = leveltrashData;

            eliteEnemyArr[i].SetEliteEnemyType(i);  //적들의 기본데이터 불러오기
            eliteEnemyArr[i].SetEnemyStatus(nowLevel);
        }
    }

    //각 몬스터마다 소환 함수를 따로 두고 스폰율에 따라 차등 생성
    void InstantiateEnemy()
    {
        //노멀 적과 엘리트 적 소환하는 함수
        for(int normalEnemyDataIndex =0; normalEnemyDataIndex<normalEnemyCount; normalEnemyDataIndex++)
            StartCoroutine(InstantiateNormalEnemyCoroutine(normalEnemyDataIndex));
        for (int eliteEnemyDataIndex = 0; eliteEnemyDataIndex < eliteEnemyCount; eliteEnemyDataIndex++)
            StartCoroutine(InstantiateEliteEnemyCoroutine(eliteEnemyDataIndex));
    }

    void TestEnemy()
    {
        eliteEnemyArr[1].SetEnforceData(nowLevel, true);
        GameObject eliteEnemyClone = Instantiate(eliteEnemyPrefabArr[1], SetEnemyPos(), Quaternion.identity);
        eliteEnemyClone.transform.SetParent(enemyCloneParent);
        eliteEnemyClone.SetActive(true);
    }

    IEnumerator InstantiateNormalEnemyCoroutine(int nowEnemyIndex)
    {
        //스폰률이 0이면 스폰 x
        if (GetSpawn(false, nowEnemyIndex) != 0)
        {
            yield return new WaitForSeconds((float)SpawnTIme(false, nowEnemyIndex));
            while (!isStageEnd && timerManager.timerCount > 1)
            {
                //몬스터 스폰
                normalEnemyArr[nowEnemyIndex].SetEnforceData(nowLevel, false);
                GameObject normalEnemyClone = Instantiate(normalEnemyPrefabArr[nowEnemyIndex], SetEnemyPos(), Quaternion.identity);

                normalEnemyClone.transform.SetParent(enemyCloneParent);
                normalEnemyClone.SetActive(true);
                yield return new WaitForSeconds((float)SpawnTIme(false, nowEnemyIndex));
            }
        }
    }

    IEnumerator InstantiateEliteEnemyCoroutine(int nowEnemyIndex)
    {
        if (GetSpawn(true, nowEnemyIndex) != 0)
        {
            yield return new WaitForSeconds((float)SpawnTIme(true, nowEnemyIndex));
            while (!isStageEnd && timerManager.timerCount > 1)
            {
                //몬스터 스폰
                eliteEnemyArr[nowEnemyIndex].SetEnforceData(nowLevel, true);
                GameObject eliteEnemyClone = Instantiate(eliteEnemyPrefabArr[nowEnemyIndex], SetEnemyPos(), Quaternion.identity);
                eliteEnemyClone.transform.SetParent(enemyCloneParent);
                eliteEnemyClone.SetActive(true);
                yield return new WaitForSeconds((float)SpawnTIme(true, nowEnemyIndex));
            }
        }
    }

    double SpawnTIme(bool isElite, int nowEnemyIndex)
    {
        double nowSpawn = 0;
        double totalSpawn = 0;

        nowSpawn = GetSpawn(isElite, nowEnemyIndex);

        //누적 오브젝트 수를 구하고 60초 안에 모든 오브젝트가 나오도록 구현
        totalSpawn = nowSpawn * 60;
        return (double)(60f / (int)totalSpawn);
    }

    double GetSpawn(bool isElite, int getIndex)
    {
        switch(getIndex)
        {
            case 0:
                if (isElite)
                    return assassinationTrashData.elite1Spawn;
                else
                    return assassinationTrashData.normal1Spawn;
            case 1:
                if (isElite)
                    return assassinationTrashData.elite2Spawn;
                else
                    return assassinationTrashData.normal2Spawn;
            case 2:
                if (isElite)
                    return assassinationTrashData.elite3Spawn;
                else
                    return assassinationTrashData.normal3Spawn;
            default:
                return -1;
        }
    }

    //적이 스폰되는 벡터 구하기
    Vector3 SetEnemyPos()
    {
        nowCharPos = character.transform.position;

        float xPos = 0;
        float yPos = 0;
        Vector3 instantiatePos = new Vector3(xPos, yPos, 0);
        do
        {
            xPos = Random.Range(-11f, 11f);
            yPos = Random.Range(-11f, 11f);
            instantiatePos.x = xPos;
            instantiatePos.y = yPos;
        } while (!IsPosInGround(instantiatePos));

        return instantiatePos;
    }

    bool IsPosInGround(Vector3 getVector)
    {
        if (getVector.x < -10 || getVector.x > 10)
            return true;
        else if (getVector.y < -10 || getVector.y > 10)
            return true;
        else
            return false;
    }

    public void SetStageEnd()
    {
        isStageEnd = true;

        //플레이어 공격 정지
        character.GetComponent<Character>().StopAttack();
        character.GetComponent<Character>().EndMove();

        //조이스틱 사용 정지와는 nightManager.isStageEnd를 매번 받기 때문에 알아서 꺼짐

        //적 오브젝트 정지
        for (int i = 0; i < enemyCloneParent.childCount; i++)
        {
            //일반 적일때 
            if (enemyCloneParent.GetChild(i).tag == "Normal")
            {
                enemyCloneParent.GetChild(i).GetComponent<NormalEnemy>().isStageEnd = true;
                //해당 콜라이더 정지는 이후 오브젝트 형태에 따라 변경될 예정@@@@@@@@@@
                enemyCloneParent.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
            }

            //엘리트 적일때
            else if (enemyCloneParent.GetChild(i).tag == "Elite")
            {
                enemyCloneParent.GetChild(i).GetComponent<EliteEnemy>().isStageEnd = true;
                enemyCloneParent.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        endPopup.SetActive(true);
    }

    public void UpdateKillCount()
    {
        killCount++;
        itemManager.ChargingReaperGauge();  //차징 리퍼 쓰면 동작
    }
}
