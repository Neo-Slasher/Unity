using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class DateCount : MonoBehaviour
{
    public static DateCount Instance;
    public int Date = 0;
    public TMP_Text Day;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnEnable()
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
                Day.text = "Day "  + Date.ToString() + "\n" + "D-"+ (7-Date).ToString();
            }
            else
            {
                Date = 1;
                Day = GameObject.Find("Date").GetComponent<TMP_Text>();
                Day.text = "Day "  + Date.ToString() + "\n" + "D-"+ (7-Date).ToString();
            }
        }
        
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
