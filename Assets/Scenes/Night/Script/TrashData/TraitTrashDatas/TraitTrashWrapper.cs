using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TraitTrashWrapper
{
    public TraitTrash[] traitTrashArr;

    public void Parse()
    {
        for(int i =0; i<traitTrashArr.Length; i++)
        {
            traitTrashArr[i].Parse();
        }
    }

}
