using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingTrash : MonoBehaviour
{
    [SerializeField]
    ChargingReaper chargingReaper;

    private void Start()
    {
        StartCoroutine(Temp());
    }

    public void ChargingGaugeFull()
    {
        chargingReaper.chargingGauge = 100;
    }

    IEnumerator Temp()
    {
        yield return new WaitForSeconds(0.1f);
        chargingReaper = GameObject.Find("ChargingReaperPrefab(Clone)").GetComponent<ChargingReaper>();
    }
}
