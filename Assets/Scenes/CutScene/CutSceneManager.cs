using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickSkipButton() {
        SceneManager.LoadScene("PreparationScene");
    }


    public void OnClickAutoButton()
    {
        Debug.Log(DataManager.instance.difficultyList.difficulty[0].goalMoney);
        Debug.Log(DataManager.instance.traitList.trait[0].index);
        Debug.Log(DataManager.instance.equipmentList.equipment[0].index);
        Debug.Log(DataManager.instance.assassinationStageList.assassinationStage[0].stageNo);

    }
}


