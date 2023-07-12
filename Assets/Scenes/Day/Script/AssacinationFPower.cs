using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class AssacinationFPower : MonoBehaviour
{
    public FightingPower FightingPower;
    public TMP_Text StageRecCP;
    public TMP_Text CPGap;
    public Image Bpanel;
    public int num;

    double stageRecCP;
    int currentCP;
    double cpGap;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        BattlePower();
    }

    void BattlePower()
    {
        stageRecCP = DataManager.instance.assassinationStageList.assassinationStage[num].stageRecCP;
        Comparison();
        if(cpGap > 10)
            Bpanel.color = Color.green;
        else if(cpGap <= 10 && cpGap >= -10)
            Bpanel.color = Color.yellow;
        else if(cpGap < -10)
            Bpanel.color = Color.red;

        StageRecCP.text = stageRecCP.ToString();

        if(stageRecCP <= currentCP)
            CPGap.text = (currentCP-stageRecCP).ToString() + "▲";
        else
            CPGap.text = (stageRecCP - currentCP).ToString() + "▼";
    }

    void Comparison()
    {
        currentCP = FightingPower.currentCP;
        cpGap = ((currentCP - stageRecCP) / stageRecCP) * 100;
    }
}
