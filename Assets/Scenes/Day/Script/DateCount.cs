using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class DateCount : MonoBehaviour
{
    public TMP_Text Day;
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.instance.player;
        Day.text = "D-" + (7 - Player.day).ToString() + "\n" +  Player.day.ToString() + "일차";
    }
    void Update()
    {
        
    }
    /*void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Update is called once per frame
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "DayScene")
        {
            if(Date < 7)
            {
                Date += 1;
                Debug.Log("Day"+Date);
                Day = GameObject.Find("Date").GetComponent<TMP_Text>();
                Day.text = (7-Date).ToString() + "\n" + Date.ToString() + "일차";
            }
            else
            {
                Date = 1;
                Day = GameObject.Find("Date").GetComponent<TMP_Text>();
                Day.text = (7-Date).ToString() + "\n" + Date.ToString() + "일차";
            }
        }
        
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/
}
