using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    private int index = 1;
    private double width = 125.0;
    private double length = 348.0;

    private bool isElite = false;
    private bool isEnforce = false;
    private bool isResist = false;
    private bool canKnockback = false;
    private bool canProj = false;

    private int imageIndex = 1;
    private int soundIndex = 1;


    public int getIndex() {
        return index;
    }
    public double getWidth() {
        return width;
    }
    public double getLength() {
        return length;
    }
    public bool getIsElite() {
        return isElite;
    }
    public bool getIsEnforce() {
        return isEnforce;
    }
    public bool getIsResist() {
        return isResist;
    }
    public bool getCanKnockback() {
        return canKnockback;
    }
    public bool getCanProj() {
        return canProj;
    }
}
