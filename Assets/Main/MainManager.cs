using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject startPopup;
    public GameObject exitPopup;

    public Button continueButton;

    void Start() {
        if (GameManager.instance.hasSavedData == false) {
            continueButton.interactable = false;
        }

        startPopup.SetActive(false);
        exitPopup.SetActive(false);
    }


    public void OnClickStartButton() {
        if (GameManager.instance.hasSavedData == true) {
            startPopup.SetActive(true);
        } else {
            // init player data
            // File.WriteAllText(Application.dataPath + "/UserData.json", JsonUtility.ToJson(myData));


            SceneManager.LoadScene("IntroScene");   
        }
    }

    public void OnClickStartPopupYesButton() {
        // TODO: OnClickStartButton의 else {} 코드와 겹침
        Debug.Log("새 게임을 시작합니다.");

        // init player data
        // File.WriteAllText(Application.dataPath + "/UserData.json", JsonUtility.ToJson(myData));

        SceneManager.LoadScene("IntroScene");
    }

    public void OnClickStartPopupNoButton() {
        startPopup.SetActive(false);
    }

    public void OnClickContinueButton() {
        Debug.Log("게임을 이어합니다.");
        SceneManager.LoadScene("PreparationScene");
    }


    public void OnClickSettingButton() {
        SceneManager.LoadScene("SettingScene");
    }


    public void OnClickExitButton() {
        exitPopup.SetActive(true);        
    }

    public void OnClickExitPopupYesButton() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnClickExitPopupNoButton() {
        exitPopup.SetActive(false);
    }
}
