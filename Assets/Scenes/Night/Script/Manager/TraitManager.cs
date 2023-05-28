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
    int nowLevel;       //임시로 레벨을 설정함
    [SerializeField]
    List<int> traitIndexList;
    [SerializeField]
    List<TraitTrash> traitTrashList;
    

    private void Start()
    {
        traitTrashWrapper = JsonManager.LoadJsonData<TraitTrashWrapper>(traitJsonName);

        SetTraitIndexListTemp();
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
}
