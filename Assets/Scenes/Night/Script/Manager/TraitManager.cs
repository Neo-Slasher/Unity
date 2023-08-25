using System;
using System.Collections;
using System.Collections.Generic;
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

    //최종 데이터매니저에서 가져오는 데이터들
    [SerializeField]
    List<Trait> traitList;

    [SerializeField]
    int testTraitIndex;

    //이펙트 추가
    [SerializeField]
    GameObject[] traitEffectArr;    //0: 끌어오기, 1: 밀어내기

    private void Start()
    {
        //실제로 구동되는 코드 입니다. 
        FindActiveTrait();
        SetTrait();
    }

    //게임매니저에서 저장된 플레이어 데이터를 가져와 특성을 저장합니다.
    void FindActiveTrait()
    {
        int[] idxArr = { 28, 42, 43, 44, 45, 61, 62 };

        //index = 28, 42, 43, 44, 45, 61, 62 총 7개의 액티브가 존재.
        for(int i =0; i < idxArr.Length; i++)
        {
            if (GameManager.instance.player.trait[idxArr[i]])
            {
                traitIndexList.Add(idxArr[i]);
                traitList.Add(DataManager.instance.traitList.trait[idxArr[i] - 1]);
            }
        }
    }

    //선택한 특성을 실행하는 함수
    

    void SetTrait()
    {
        Trait nowTrait;
        for (int i = 0; i < traitList.Count; i++)
        {
            nowTrait = traitList[i];
            //액티브 특성 실행
            if (traitList[i].effectType1 == (int)EffectType.active)
            {
                SetActiveTrait(nowTrait);
            }
        }
    }

    //각 특성에서 변화하는 값들을 대입하는 함수(낮씬에서 이미 데이터 정리되어 들어와 이제 필요 없음)
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

    void SetActiveTrait(Trait getTrait)
    {
        //index = 28, 42, 43, 44, 45, 61, 62 총 7개의 액티브가 존재.
        ActiveTrait nowActive;
        nowActive = (ActiveTrait)getTrait.index;
        Debug.Log("액티브: " +nowActive);
        StartActive(nowActive, getTrait);
    }

    void StartActive(ActiveTrait getActiveTrait, Trait getTrait)
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
    void SetStartShield(Trait getTrait)
    {
        character.SetStartShieldPointData((float)getTrait.effectValue1);
    }

    IEnumerator SetAnimatorParameter(Animator getAnimator, string animationName)
    {
        while(getAnimator.GetBool("isPlay"))
        {
            if (getAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                getAnimator.SetBool("isPlay", false);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    //특성 인덱스 42번 n초마다 주변 적 끌어당기기 코드
    IEnumerator DragEnemyCoroutine(Trait getTrait)
    {
        GameObject drugEnemyEffect = Instantiate(traitEffectArr[0]);
        drugEnemyEffect.transform.localPosition = character.transform.position;
        drugEnemyEffect.transform.SetParent(character.transform);
        drugEnemyEffect.SetActive(false);
        Animator effectAnimator = drugEnemyEffect.GetComponent<Animator>();

        while (!nightManager.isStageEnd)
        {
            yield return new WaitForSeconds((float)getTrait.effectValue1);    //n초의 대기시간을 갖는 코드
            //이펙트 키기
            drugEnemyEffect.SetActive(true);
            effectAnimator.SetBool("isPlay", true);

            Debug.Log("DrugEnemy");
            Collider2D[] getCols =
                character.ReturnOverLapColliders((float)getTrait.effectValue3 / 100, (float)getTrait.effectValue2 / 100);

            if (getCols.Length != 0)
                //이제 당겨
                for (int i =0; i< getCols.Length; i++)
                {
                    if (getCols[i].tag == "Normal" || getCols[i].tag == "Elite")
                        getCols[i].GetComponent<EnemyParent>().DrugEnemy();
                }

            StartCoroutine(SetAnimatorParameter(effectAnimator, "DrugEnemyAnimation"));
        }
    }
    public Collider2D[] arr;
    //특성 인덱스 43번 n초마다 주변 적 밀어내기 코드
    IEnumerator ThrustEnemyCoroutine(Trait getTrait)
    {
        GameObject thrustEnemyEffect = Instantiate(traitEffectArr[0]);
        thrustEnemyEffect.transform.localPosition = character.transform.position;
        thrustEnemyEffect.transform.SetParent(character.transform);
        thrustEnemyEffect.SetActive(false);
        Animator effectAnimator = thrustEnemyEffect.GetComponent<Animator>();

        while (!nightManager.isStageEnd)
        {
            yield return new WaitForSeconds((float)getTrait.effectValue1);    //n초의 대기시간을 갖는 코드
            //이펙트
            thrustEnemyEffect.SetActive(true);
            effectAnimator.SetBool("isPlay", true);

            Debug.Log("ThrustEnemy");
            Collider2D[] getCols =
                character.ReturnOverLapColliders((float)getTrait.effectValue2 / 100);
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

            StartCoroutine(SetAnimatorParameter(effectAnimator, "ThrustEnemyAnimation"));
        }
    }

    IEnumerator GetMoveSpeed(Trait getTrait)
    {
        Debug.Log("GetMoveSpeed");
        double defaultMoveSpeed = character.nowMoveSpeed;
        double upgradeMoveSpeed = character.nowMoveSpeed * (1 + getTrait.effectValue3);

        //60~54초
        character.SetMoveSpeed(upgradeMoveSpeed);
        while (timerManager.timerCount >= getTrait.effectValue2)
        {
            yield return new WaitForSeconds(0.5f);
        }

        character.SetMoveSpeed(defaultMoveSpeed);

        //6~0초@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        while (timerManager.timerCount >= getTrait.effectValue1)
        {
            yield return new WaitForSeconds(0.5f);
        }
        character.SetMoveSpeed(upgradeMoveSpeed);
    }

    IEnumerator GetAttackPower(Trait getTrait)
    {
        Debug.Log("GetAttackPower");
        double hitPointMax = character.ReturnCharacterHitPointMax();
        double hitPointGoal = hitPointMax * getTrait.effectValue1;
        double attackPower = character.ReturnCharacterAttackPower();
        attackPower *= (1 + getTrait.effectValue2);

        while (character.ReturnCharacterHitPoint() >= hitPointGoal)
            yield return new WaitForSeconds(0.1f);

        character.SetCharacterAttackPower(attackPower);
    }

    IEnumerator StopEnemy(Trait getTrait)
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

            yield return new WaitForSeconds((float)getTrait.effectValue2);
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

    void AbsorbDamage(Trait getTrait)
    {
        Debug.Log("AbsorbDamage");
        character.SetAbsorbAttackData((float)getTrait.effectValue2);
        character.canChange = true;
    }
}
