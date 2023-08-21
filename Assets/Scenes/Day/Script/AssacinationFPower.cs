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
    public int num;

    double stageRecCP;
    int currentCP;
    int cpGap;

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
         /*if(cpGap > 10)
            Bpanel.color = Color.green;
        else if(cpGap <= 10 && cpGap >= -10)
            Bpanel.color = Color.yellow;
        else if(cpGap < -10)
            Bpanel.color = Color.red;*/

        StageRecCP.text = stageRecCP.ToString();

        if(stageRecCP <= currentCP)
            CPGap.text = "<color=red>" + (currentCP-stageRecCP).ToString() + "</color>";
        else
            CPGap.text = "<color=green> " + (stageRecCP - currentCP).ToString() + "</color>";
    }

    void Comparison()
    {
        currentCP = FightingPower.currentCP;
        cpGap = (int)((currentCP - stageRecCP) / stageRecCP) * 100;
    }
}
