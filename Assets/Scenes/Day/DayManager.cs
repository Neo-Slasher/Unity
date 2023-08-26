using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public GameObject setting;

    public void OnClickSettingButton() {
        setting.SetActive(true);
    }

    public void OnClickSettingContinueButton() {
        setting.SetActive(false);
    }

    public void OnClickSettingExitButton() {
        GameManager.instance.SavePlayerData();
        SceneManager.LoadScene("MainScene");
    }
}
