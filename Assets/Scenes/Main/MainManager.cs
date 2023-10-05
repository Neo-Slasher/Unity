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
        if (GameManager.instance.hasSavedData) {
            GameManager.instance.LoadPlayerData();
        }
        else {
            GameManager.instance.InitPlayerData();
        }
        if (GameManager.instance.player.day == 1) {
            continueButton.interactable = false;
        }
        
        startPopup.SetActive(false);
        exitPopup.SetActive(false);
    }

    public void StartNewGame() {
        ResetPlayer();
        SceneManager.LoadScene("CutScene"); 
    }

    public void ResetPlayer() {
        int prevLevel = GameManager.instance.player.level;
        int prevExp = GameManager.instance.player.curExp;
        GameManager.instance.InitPlayerData();
        GameManager.instance.player.level = prevLevel;
        GameManager.instance.player.curExp = prevExp;
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
