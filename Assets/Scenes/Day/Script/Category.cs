using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class Category : MonoBehaviour
{
    public ToggleGroup ToggleGroup;
    public Toggle Toggle;
    public GameObject[] Scenes;
    public TMP_Text SName;
    /* TMP_Text Day;
    public GameObject GameManager;

    int date;

    // Update is called once per frame
    void Start()
    {
        date = GameManager.GetComponent<DateCount>().Date;
        Day.text =  date.ToString();
        
    }*/
    void Update()
    {
        if(ToggleGroup.ActiveToggles().Any())
        {
            Toggle = ToggleGroup.ActiveToggles().FirstOrDefault();
            ChangeScene();
        }
    }
   
    public void ChangeScene()
    {
         if(Toggle.name.Equals("상점tog"))
            {
                Scenes[0].SetActive(true);
                Scenes[1].SetActive(false);
                Scenes[2].SetActive(false);
                SName.text = "상점";
            }
            if(Toggle.name.Equals("암살tog"))
            {
                Scenes[0].SetActive(false);
                Scenes[1].SetActive(true);
                Scenes[2].SetActive(false);
                SName.text = "암살";
            }
            if(Toggle.name.Equals("정비tog"))
            {
                Scenes[0].SetActive(false);
                Scenes[1].SetActive(false);
                Scenes[2].SetActive(true);
                SName.text = "정비";
            }
    }
}
