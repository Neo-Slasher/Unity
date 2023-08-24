using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public GameObject setting;

    public void OnClickSettingButton() {
        setting.SetActive(true);
    }

    public void OnClickSettingExitButton() {
        setting.SetActive(false);
    }
}
