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
    int nowLevel;       //�ӽ÷� ������ ������
    [SerializeField]
    List<int> traitIndexList;
    [SerializeField]
    List<TraitTrash> traitTrashList;
    

    private void Start()
    {
        traitTrashWrapper = JsonManager.LoadJsonData<TraitTrashWrapper>(traitJsonName);

        SetTraitIndexListTemp();
    }

    //�ӽ÷� �� ������ ù��° Ư���� �������� ����(��������)
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
