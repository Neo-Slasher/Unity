using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TraitList
{
    public List<Trait> trait;
}


[Serializable]
public class Trait
{
    public int index;
    public string name;
    public int reqireLv;
    public int rank;
    public int imageIndex;
    public string script;
    public int effectType1;
    public int effectValue1;
    public int effectMulti1;
    public int effectType2;
    public int effectValue2;
    public int effectMulti2;
    public int effectType3;
    public int effectValue3;
    public int effectMulti3;
    public int effectType4;
    public int effectValue4;
    public int effectMulti4;
}
