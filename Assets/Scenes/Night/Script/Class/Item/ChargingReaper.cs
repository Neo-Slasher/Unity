using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingReaper : MonoBehaviour
{
    public bool isAttack = false;
    public int chargingGauge = 100;
    public double reaperAttackDamaege = 500; //임시

    public bool IsChargingGaugeFull()
    {
        if (chargingGauge < 100)
        {
            return false;
        }
        else
        {
            this.gameObject.SetActive(true);
            return true;
        }
    }

    public void ReaperUse()
    {
        this.gameObject.SetActive(false);
        //리퍼 사용
    }

    public void ChargingGauge()
    {
        chargingGauge += 5;
    }
}
