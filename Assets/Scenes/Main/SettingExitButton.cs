using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingExitButton : MonoBehaviour
{
    public GameObject setting;


    public void onClickExitButton() {
        setting.SetActive(false);
    }
}
