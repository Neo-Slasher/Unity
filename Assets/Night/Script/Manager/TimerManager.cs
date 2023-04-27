using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;

    [SerializeField]
    GameObject timerParent;
    [SerializeField]
    Image timerImage;
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    int timerCount = 60;

    private void Start()
    {
        PlayTImer();
    }

    void PlayTImer()
    {
        StartCoroutine(PlayTimerCoroutine());
    }

    IEnumerator PlayTimerCoroutine()
    {
        while (timerCount >= 0)
        {
            Debug.Log(timerCount);
            timerText.text = timerCount.ToString();
            yield return new WaitForSeconds(1f);
            timerCount--;
        }

        nightManager.SetStageEnd();
    }
}
