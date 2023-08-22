using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class AssacinationFPower : MonoBehaviour
{
    public FightingPower FightingPower;
    public SceneSwitch SceneSwitch;

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

        if(cpGap > 10)
            CPGap.text = "<color=green>" + (currentCP-stageRecCP).ToString() + "</color>";
        else if(cpGap <= 10 && cpGap >= -10)
        {
            if(cpGap < 0)
                CPGap.text = "<color=yellow>" + (-1 * (currentCP-stageRecCP)).ToString() + "</color>";
            else   
                CPGap.text = "<color=yellow>" + (currentCP-stageRecCP).ToString() + "</color>";
        }
            
        else if(cpGap < -10)
            CPGap.text = "<color=red>" + (-1 * (currentCP-stageRecCP)).ToString() + "</color>";

        StageRecCP.text = stageRecCP.ToString();
    }

    void Comparison()
    {
        currentCP = FightingPower.currentCP;
        cpGap = (int)((currentCP - stageRecCP) / stageRecCP) * 100;
    }

    public void SelectRank()
    {
        SceneSwitch.AsscinationNum = num;
    }
}
