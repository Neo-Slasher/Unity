using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class AssacinationInfo : MonoBehaviour
{
    public int num = 0;
    public TMP_Text asscList;
    int normalReward;
    int eliteReward;
    int stageDropRank;
    string DropRank;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame

    void Update()
    {
        Print();
        /*stageNo = DataManager.instance.assassinationStageList.assassinationStage[num].stageNo;
        Debug.Log(stageNo);*/
    }

    void Print()
    {
        normalReward = DataManager.instance.assassinationStageList.assassinationStage[num].normalReward;
        eliteReward= DataManager.instance.assassinationStageList.assassinationStage[num].eliteReward;
        stageDropRank = DataManager.instance.assassinationStageList.assassinationStage[num].stageDropRank;

        if (stageDropRank <= 7)
            DropRank = "C";
        else if (stageDropRank >= 8 && stageDropRank <=16)
            DropRank = "B";
        else if (stageDropRank >= 17 && stageDropRank <= 30)
            DropRank = "A";
        else if (stageDropRank >= 31)
            DropRank = "S";

        asscList.text = "-일반 적 현상금 : " + normalReward.ToString() + "α\n" + "-정예 적 현상금 : " + eliteReward.ToString() + "α\n-" + DropRank + "등급 아이템 드롭";
    }


}
