using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneManager : MonoBehaviour
{


    public TextMeshProUGUI typingText;
    public string message;
    public float speed = 0.07f;

    // Start is called before the first frame update
    void Start()
    {
        message = "당신은 돈을 모아 성공적으로 인공 신체 시술을 받을 수 있었습니다. 그러나 당신은 지금까지 '화타'가 불안정한 인공 신체를 고의로 제공하고 있었다는 사실을 밝혀내었습니다.";
        StartCoroutine(Typing(typingText, message, speed));
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

    IEnumerator Typing(TextMeshProUGUI typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }
}


