using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Repairment : MonoBehaviour
{
    public Button[] equipments;
    public Button[] items;

    public string Rank;
    public string RName;
    
    public List<Equipment> sortEquip;
    public void Start()
    {
        LoadItems();
    }

    public void LoadItems()
    {
        SortEquipment();
        for (int i = 0; i < GameManager.instance.player.equipment.Count; i++)
        {
            equipments[i].interactable = true;
            RName = GameManager.instance.player.equipment[i].name;
            int R = GameManager.instance.player.equipment[i].rank;
            if (R == 3)
                Rank = "S";
            else if (R == 2)
                Rank = "A";
            else if (R == 1)
                Rank = "B";
            else if (R == 0)
                Rank = "C";

            equipments[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Equip/" + RName) as Sprite;
            equipments[i].transform.Find("rank").gameObject.GetComponent<TMP_Text>().text = Rank;
            Debug.Log("equip"+RName);    
        }
        for(int i = GameManager.instance.player.equipment.Count; i < 3; i++)
        {
            equipments[i].interactable = false;
        }
        for (int i = 0; i < GameManager.instance.player.item.Count; i++)
        {
            items[i].interactable = true;
            RName = GameManager.instance.player.item[i].name;
            int R = DataManager.instance.itemList.item[i].rank;
            if (R == 3)
                Rank = "S";
            else if (R == 2)
                Rank = "A";
            else if (R == 1)
                Rank = "B";
            else if (R == 0)
                Rank = "C";

            items[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + RName) as Sprite;
            items[i].transform.Find("rank").gameObject.GetComponent<TMP_Text>().text = Rank;
            items[i].transform.Find("Image").gameObject.SetActive(false);
            Debug.Log("item"+RName);
        }
        for(int i = GameManager.instance.player.item.Count; i < 3; i++)
        {
            items[i].interactable = false;
            items[i].transform.Find("Image").gameObject.SetActive(true);
        }

    }
    public void SortEquipment()
    {
        if(GameManager.instance.player.equipment.Count > 1)
        {
            GameManager.instance.player.equipment = GameManager.instance.player.equipment.OrderBy(x => x.part).Reverse().ToList();
        }
    }
}
