using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public int AsscinationNum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoNight()
    {
        GameManager.instance.player.assassinationCount = AsscinationNum;
        GameManager.instance.SavePlayerData();
        SceneManager.LoadScene("NightScene");
        Debug.Log(GameManager.instance.player.assassinationCount);
    }
    public void GoDay()
    {
        SceneManager.LoadScene("DayScene");
    }
}
