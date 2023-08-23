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

    public GameObject setting;


    void Start() {
        if (GameManager.instance.hasSavedData == false) {
            continueButton.interactable = false;
        }

        startPopup.SetActive(false);
        exitPopup.SetActive(false);
    }

    public void StartNewGame() {
        GameManager.instance.InitPlayerData();
        SceneManager.LoadScene("CutScene"); 
    }

    public void OnClickStartButton() {
        if (GameManager.instance.hasSavedData == true) {
            startPopup.SetActive(true);
        } else {
            StartNewGame(); 
        }
    }

    public void OnClickStartPopupYesButton() {
        StartNewGame();
    }

    public void OnClickStartPopupNoButton() {
        startPopup.SetActive(false);
    }

    public void OnClickContinueButton() {
        GameManager.instance.LoadPlayerData();
        SceneManager.LoadScene("DayScene");
    }


    public void OnClickSettingButton() {
        setting.SetActive(true);
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

    public void OnClickSettingExitButton() {
        setting.SetActive(false);
    }
}
