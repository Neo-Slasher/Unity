using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightManager : MonoBehaviour
{
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
    GameObject normalEnemyPrefab;
    [SerializeField]
    GameObject eliteEnemyPrefab;

    NormalEnemy normalEnemy;
    EliteEnemy eliteEnemy;

    Vector3 nowCharPos;
    [SerializeField]
    Transform enemyCloneParent;

    //전투시 필요한 데이터
    public bool isStageEnd = false; //밤이 끝났는지 알아보는 변수

    //tempData
    int monsterCount;

    private void Start()
    {
        normalEnemy = normalEnemyPrefab.GetComponent<NormalEnemy>();
        eliteEnemy = eliteEnemyPrefab.GetComponent<EliteEnemy>();
        normalEnemy.SetCharacter(character);    
        eliteEnemy.SetCharacter(character);

        //몬스터 생성 함수 넣을 예정
        InstantiateEnemy();
    }

    
    //오브젝트 풀링으로 함수 변경합시다.
    void InstantiateEnemy()
    {
        //적 데이터 정립
        normalEnemy.SetEnemyStatus();
        eliteEnemy.SetEnemyStatus();
        //선택 난이도에 따라 개체수 변경
        StartCoroutine(InstantiateEnemyCoroutine());
    }

    IEnumerator InstantiateEnemyCoroutine()
    {
        while (!isStageEnd && timerManager.timerCount >1)
        {
            //몬스터 스폰
            GameObject normalEnemyClone = Instantiate(normalEnemyPrefab, SetEnemyPos(), Quaternion.identity);
            normalEnemyClone.transform.SetParent(enemyCloneParent);
            yield return new WaitForSeconds(5f);
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
        character.GetComponent<Character>().CharacterAttackStop();
        character.GetComponent<Character>().CharacterStop(Vector3.zero);

        //조이스틱 사용 정지와는 nightManager.isStageEnd를 매번 받기 때문에 알아서 꺼짐

        //적 오브젝트 정지
        for (int i = 0; i < enemyCloneParent.childCount; i++)
        {
            //일반 적일때 
            if (enemyCloneParent.GetChild(i).name == "NormalEnemyPrefab(Clone)")
            {
                enemyCloneParent.GetChild(i).GetComponent<NormalEnemy>().isStageEnd = true;
                //해당 콜라이더 정지는 이후 오브젝트 형태에 따라 변경될 예정@@@@@@@@@@
                enemyCloneParent.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
            }

            //엘리트 적일때
            else if (enemyCloneParent.GetChild(i).name == "EliteEnemyPrefab(Clone)")
            {
                enemyCloneParent.GetChild(i).GetComponent<EliteEnemy>().isStageEnd = true;
                enemyCloneParent.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        endPopup.SetActive(true);
    }
}
