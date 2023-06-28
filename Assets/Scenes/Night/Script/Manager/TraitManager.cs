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
    TraitTrashWrapper traitTrashWrapper;
    [SerializeField]
    string traitJsonName;

    [SerializeField]
    Character character;

    [SerializeField]
    int nowLevel;       //임시로 레벨을 설정함
    [SerializeField]
    List<int> traitIndexList;
    [SerializeField]
    List<TraitTrash> traitTrashList;

    [SerializeField]
    int testTraitIndex;
    

    private void Start()
    {
        traitTrashWrapper = JsonManager.LoadJsonData<TraitTrashWrapper>(traitJsonName);
        traitTrashWrapper.Parse();

        //SetTraitIndexListTemp();
        TestActiveTrait(testTraitIndex);
        SetTrait();
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
                traitTrashList.Add(traitTrashWrapper.traitTrashArr[i]);
            }
        }
    }

    //특성을 테스트하기 위해서 만들어둔 함수
    void TestActiveTrait(int testIndex)
    {
        traitTrashList.Add(traitTrashWrapper.traitTrashArr[testIndex]);
    }

    //선택한 특성을 실행하는 함수
    void SetTrait()
    {
        TraitTrash nowTrait;
        for(int i =0; i< traitTrashList.Count; i++)
        {
            nowTrait = traitTrashList[i];
            //패시브 특성 설정
            if (traitTrashList[i].effectType1 != EffectType.active)
            {
                SetPassiveTrait(nowTrait);
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
                break;
            case ActiveTrait.getAttackPower:
                break;
            case ActiveTrait.stopEnemy:
                break;
            case ActiveTrait.absorbDamage:
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

    //특성 인덱스 43번 n초마다 주변 적 밀어내기 코드
    IEnumerator ThrustEnemyCoroutine(TraitTrash getTrait)
    {
        while (!nightManager.isStageEnd)
        {
            yield return new WaitForSeconds(getTrait.traitEffectValue1);    //n초의 대기시간을 갖는 코드
            Debug.Log("ThrustEnemy");
            Collider2D[] getCols =
                character.ReturnOverLapColliders(getTrait.traitEffectValue2 / 128, 0);
            
            if (getCols.Length != 0)
                //이제 당겨
                for (int i = 0; i < getCols.Length; i++)
                {
                    if (getCols[i].tag == "Normal" || getCols[i].tag == "Elite")
                    {
                        getCols[i].GetComponent<EnemyParent>().ThrustEnemy();
                        Debug.Log(getCols[i].name);
                    }
                }
        }
    }
}
