using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    KillData killdata;  //죽인 몬스터 마리수 카운트

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
    bool isStageEnd = false; //밤이 끝났는지 알아보는 변수

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
        while (!isStageEnd)
        {
            //몬스터 스폰
            GameObject normalEnemyClone = Instantiate(normalEnemyPrefab, SetEnemyPos(), Quaternion.identity);
            normalEnemyClone.transform.SetParent(enemyCloneParent);
            yield return new WaitForSeconds(1f);
        }
    }

    Vector3 SetEnemyPos()
    {
        nowCharPos = character.transform.position;

        float xPos = 0;
        float yPos = 0;
        Vector3 instantiatePos = new Vector3(xPos, yPos, 0);
        do
        {
            xPos = Random.Range(-9f, 9f);
            yPos = Random.Range(-9f, 9f);
            instantiatePos.x = xPos;
            instantiatePos.y = yPos;
        } while ((instantiatePos - nowCharPos).magnitude < 5);

        return instantiatePos;
    }
}
