using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
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
    

    private void Start()
    {
        traitTrashWrapper = JsonManager.LoadJsonData<TraitTrashWrapper>(traitJsonName);
        traitTrashWrapper.Parse();

        SetTraitIndexListTemp();
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

    }
}
