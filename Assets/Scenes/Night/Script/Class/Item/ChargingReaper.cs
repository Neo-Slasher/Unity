using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingReaper : MonoBehaviour
{
    [SerializeField]
    GameObject reaperAfterImage;
    public bool isAttack = false;
    public int chargingGauge = 100;
    public double reaperAttackDamaege = 500; //�ӽ�

    public bool IsChargingGaugeFull()
    {
        if (chargingGauge < 100)
        {
            return false;
        }
        else
        {
            //StartCoroutine(ReaperAfterImageCoroutine());
            this.gameObject.SetActive(true);
            return true;
        }
    }

    public void ReaperUse()
    {
        this.gameObject.SetActive(false);
        //���� ���
    }

    public void ChargingGauge()
    {
        chargingGauge += 5;
    }

    IEnumerator ReaperAfterImageCoroutine()
    {
        reaperAfterImage.SetActive(false) ;
        yield return new WaitForSeconds(0.1f);
        reaperAfterImage.SetActive(true);
    }
}
