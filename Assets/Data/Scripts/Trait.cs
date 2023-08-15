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
    public int requireLv;
    public int rank;
    public int imageIndex;
    public string script;
    public int effectType1;
    public double effectValue1;
    public double effectMulti1;
    public int effectType2;
    public double effectValue2;
    public double effectMulti2;
    public int effectType3;
    public double effectValue3;
    public double effectMulti3;
    public int effectType4;
    public double effectValue4;
    public double effectMulti4;
}
