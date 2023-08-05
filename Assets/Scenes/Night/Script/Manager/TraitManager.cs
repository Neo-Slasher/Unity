using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    TimerManager timerManager;

    [SerializeField]
    Character character;
    [SerializeField]
    Transform enemyCloneParent;


    //과거 임시 데이터들
    [SerializeField]
    TraitTrashWrapper traitTrashWrapper;
    [SerializeField]
    string traitJsonName;
    [SerializeField]
    int nowLevel;       //임시로 레벨을 설정함
    [SerializeField]
    List<int> traitIndexList;
    [SerializeField]
    List<TraitTrash> traitOldList;

    //최종 데이터매니저에서 가져오는 데이터들
    List<Trait> traitList;
    Trait nowTrait;

    [SerializeField]
    int testTraitIndex;
    

    private void Start()
    {
        //임시로 특성이 작동하는지 보기 위해 만든 임시 코드였습니다. 앞으로 쓸 일 없어요.
        //traitTrashWrapper = JsonManager.LoadJsonData<TraitTrashWrapper>(traitJsonName);
        //traitTrashWrapper.Parse();
        //SetTraitIndexListTemp();
        //TestActiveTrait(testTraitIndex);
        //SetTrait2();


        //실제로 구동되는 코드 입니다. 

    }

    //게임매니저에서 저장된 플레이어 데이터를 가져와 특성을 저장합니다.
    void SetTraitList()
    {
        //플레이어 데이터에서 있는 인덱스 traitIndexList에 옮김
        //traitList에 또 넣음
    }

    //임시로 각 레벨당 첫번째 특성을 가지도록 설정(만렙기준)
    void SetTraitIndexListTemp()
    {
        int nowLevel = 1;
        for (int i = 0; i < traitTrashWrapper.traitTrashArr.Length; i++)
        {
            if (traitTrashWrapper.traitTrashArr[i].traitReqLvl == nowLevel)
            {
                nowLevel++;
                traitIndexList.Add(traitTrashWrapper.traitTrashArr[i].traitIdx);
                traitOldList.Add(traitTrashWrapper.traitTrashArr[i]);
            }
        }
    }

    //특성을 테스트하기 위해서 만들어둔 함수
    void TestActiveTrait(int testIndex)
    {
        traitOldList.Add(traitTrashWrapper.traitTrashArr[testIndex]);
    }

    //선택한 특성을 실행하는 함수
    void SetTrait()
    {
        Trait nowTrait;
        for (int i = 0; i < traitOldList.Count; i++)
        {
            nowTrait = traitList[i];
            //패시브 특성 설정
            if (traitOldList[i].effectType1 != EffectType.active)
            {
                //SetPassiveTrait(nowTrait);
                //패시브 특성은 밤 씬으로 들어오기 이전에 적용되어 있을 예정
            }

            //액티브 특성 설정
            else
            {
                //SetActiveTrait(nowTrait);
            }
        }
    }

    //선택한 특성을 실행하는 함수(이젠 안씀)
    void SetOldTrait()
    {
        TraitTrash nowTrait;
        for(int i =0; i< traitOldList.Count; i++)
        {
            nowTrait = traitOldList[i];
            //패시브 특성 설정
            if (traitOldList[i].effectType1 != EffectType.active)
            {
                //SetPassiveTrait(nowTrait);
                //패시브 특성은 밤 씬으로 들어오기 이전에 적용되어 있을 예정
            }

            //액티브 특성 설정
            else
            {
                SetActiveTrait(nowTrait);
            }
        }
    }

    //각 특성에서 변화하는 값들을 대입하는 함수
    void SetPassiveTrait(TraitTrash getTrait)
    {
        SetPassiveCharacterData(getTrait.effectType1, getTrait.traitEffectValue1, getTrait.traitEffectMulti1);
        SetPassiveCharacterData(getTrait.effectType2, getTrait.traitEffectValue2, getTrait.traitEffectMulti2);
        SetPassiveCharacterData(getTrait.effectType3, getTrait.traitEffectValue3, getTrait.traitEffectMulti3);
        SetPassiveCharacterData(getTrait.effectType4, getTrait.traitEffectValue4, getTrait.traitEffectMulti4);
    }

    void SetPassiveCharacterData(EffectType getEffectType, float getEffectValue, bool getEffectMulti)
    {
        character.SetCharacterTrashData(getEffectType, getEffectValue, getEffectMulti);
    }

    void SetActiveTrait(TraitTrash getTrait)
    {
        //index = 28, 42, 43, 44, 45, 61, 62 총 7개의 액티브가 존재.
        ActiveTrait nowActive;
        nowActive = (ActiveTrait)getTrait.traitIdx;
        StartActive(nowActive, getTrait);
    }

    void StartActive(ActiveTrait getActiveTrait, TraitTrash getTrait)
    {
        switch (getActiveTrait)
        {
            case ActiveTrait.startShield:
                SetStartShield(getTrait);
                break;
            case ActiveTrait.dragEnemy:
                StartCoroutine(DragEnemyCoroutine(getTrait));
                break;
            case ActiveTrait.thrustEnemy:
                StartCoroutine(ThrustEnemyCoroutine(getTrait));
                break;
            case ActiveTrait.getMoveSpeed:
                StartCoroutine(GetMoveSpeed(getTrait));
                break;
            case ActiveTrait.getAttackPower:
                StartCoroutine(GetAttackPower(getTrait));
                break;
            case ActiveTrait.stopEnemy:
                StartCoroutine(StopEnemy(getTrait));
                break;
            case ActiveTrait.absorbDamage:
                AbsorbDamage(getTrait);
                break;
            default:
                break;
        }
    }

    //특성 인덱스 28번 게임 시작시 쉴드 생성 코드
    void SetStartShield(TraitTrash getTrait)
    {
        character.SetStartShieldPointData(getTrait.traitEffectValue1);
    }

    //특성 인덱스 42번 n초마다 주변 적 끌어당기기 코드
    IEnumerator DragEnemyCoroutine(TraitTrash getTrait)
    {
        while (!nightManager.isStageEnd)
        {
            yield return new WaitForSeconds(getTrait.traitEffectValue1);    //n초의 대기시간을 갖는 코드
            Debug.Log("DrugEnemy");
            Collider2D[] getCols =
                character.ReturnOverLapColliders(getTrait.traitEffectValue3 / 128, getTrait.traitEffectValue2 / 128);

            if (getCols.Length != 0)
                //이제 당겨
                for (int i =0; i< getCols.Length; i++)
                {
                    if (getCols[i].tag == "Normal" || getCols[i].tag == "Elite")
                        getCols[i].GetComponent<EnemyParent>().DrugEnemy();
                }
        }
    }
    public Collider2D[] arr;
    //특성 인덱스 43번 n초마다 주변 적 밀어내기 코드
    IEnumerator ThrustEnemyCoroutine(TraitTrash getTrait)
    {
        while (!nightManager.isStageEnd)
        {
            yield return new WaitForSeconds(getTrait.traitEffectValue1);    //n초의 대기시간을 갖는 코드
            Debug.Log("ThrustEnemy");
            Collider2D[] getCols =
                character.ReturnOverLapColliders(getTrait.traitEffectValue2 / 128);
            arr = getCols;
            if (getCols != null)
                //이제 당겨
                for (int i = 0; i < getCols.Length; i++)
                {
                    if (getCols[i].tag == "Normal" || getCols[i].tag == "Elite")
                    {
                        getCols[i].GetComponent<EnemyParent>().ThrustEnemy();
                        Debug.Log(getCols.Length);
                    }
                }
        }
    }

    IEnumerator GetMoveSpeed(TraitTrash getTrait)
    {
        Debug.Log("GetMoveSpeed");
        double defaultMoveSpeed = character.nowMoveSpeed;
        double upgradeMoveSpeed = character.nowMoveSpeed * (1 + getTrait.traitEffectValue3);

        //60~54초
        character.SetMoveSpeed(upgradeMoveSpeed);
        while (timerManager.timerCount >= getTrait.traitEffectValue2)
        {
            yield return new WaitForSeconds(0.5f);
        }

        character.SetMoveSpeed(defaultMoveSpeed);

        //6~0초@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        while (timerManager.timerCount >= getTrait.traitEffectValue1 * 5)
        {
            yield return new WaitForSeconds(0.5f);
        }
        character.SetMoveSpeed(upgradeMoveSpeed);
    }

    IEnumerator GetAttackPower(TraitTrash getTrait)
    {
        Debug.Log("GetAttackPower");
        double hitPointMax = character.ReturnCharacterHitPointMax();
        double hitPointGoal = hitPointMax * getTrait.traitEffectValue1;
        double attackPower = character.ReturnCharacterAttackPower();
        attackPower *= (1 + getTrait.traitEffectValue2);

        while (character.ReturnCharacterHitPoint() >= hitPointGoal)
            yield return new WaitForSeconds(0.1f);

        character.SetCharacterAttackPower(attackPower);
    }

    IEnumerator StopEnemy(TraitTrash getTrait)
    {
        Debug.Log("StopEnemy");

        while (!nightManager.isStageEnd)
        {
            //yield return new WaitForSeconds(getTrait.traitEffectValue1);
            yield return new WaitForSeconds(5);
            //적 오브젝트 정지
            Debug.Log("stop");
            for (int i = 0; i < enemyCloneParent.childCount; i++)
            {
                if (enemyCloneParent.childCount != 0)
                {
                    enemyCloneParent.GetChild(i).GetComponent<EnemyParent>().EnemyStop();
                }
            }

            yield return new WaitForSeconds(getTrait.traitEffectValue2);
            Debug.Log("move");
            //적 오브젝트 정지
            for (int i = 0; i < enemyCloneParent.childCount; i++)
            {
                if (enemyCloneParent.childCount != 0)
                {
                    if(enemyCloneParent.GetChild(i) != null)
                        enemyCloneParent.GetChild(i).GetComponent<EnemyParent>().EnemyMove();
                }
            }
        }
    }

    void AbsorbDamage(TraitTrash getTrait)
    {
        Debug.Log("AbsorbDamage");
        character.SetAbsorbAttackData(getTrait.traitEffectValue2 * 200);
        character.canChange = true;
    }
}
