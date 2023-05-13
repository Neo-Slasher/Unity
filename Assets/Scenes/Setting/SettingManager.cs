using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Button dataResetButton;

    public GameObject resetPopup;

    // Start is called before the first frame update



    void Start()
    {
        if (!File.Exists(Application.dataPath + "/UserData.json")) {
            dataResetButton.interactable = false;
        }

        resetPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDataResetButton() {
        resetPopup.SetActive(true);
    }

    public void OnClickDataResetPopupYesButton() {
        File.Delete(Application.dataPath + "/UserData.json");
        dataResetButton.interactable = false;
        resetPopup.SetActive(false);
    }

    public void OnClickDataResetPopupNoButton() {
        resetPopup.SetActive(false);
    }


    public void OnClickReturnButton() {
        SceneManager.LoadScene("MainScene");
    }
}
