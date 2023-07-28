using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectItem : MonoBehaviour
{
    //아이템 슬롯 클릭시 정보 변경 

    public ItemSlot ItemSlot;

    public Image change_img;
    Image thisimg;

    public TMP_Text Name;
    public TMP_Text Rank;
    public TMP_Text Part;
    public TMP_Text Info;

    public string iname = "";
    public int rank = -1;
    public int part = -1;
    public int attackPower;
    public int attackRange;
    public int attackSpeed;

    string power = "";
    string range = "";
    string speed = "";

    public int buttonNum = 0;

    private Button thisbtn;

    void Start()
    {
        thisimg = GetComponent<Image>();
    }

    // Update is called once per frame
    public void ItemSelect()
    {
        /*for(int i = 0; i < ItemSlot.slotNum; i++)
        {
            if(ItemSlot.slots[i] == thisbtn)
                buttonNum = i;
        }
        Debug.Log(buttonNum);*/
        ChangeImage();
        ShowStack();
    }

    public void ChangeImage()
    {
        change_img.sprite = thisimg.sprite;
    }

    public void GetStack()
    {
        
        if(ItemSlot.shopslot[buttonNum].shopType == "equip")
        {
            iname = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].name;
            rank = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].rank;
            part = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].part;
            attackPower = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackPower;
            attackRange = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackRange;
            attackSpeed = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackSpeed;
        }
        else if(ItemSlot.shopslot[buttonNum].shopType == "item")
        {
            iname = "name";
            rank = -1;
            part = -1;
            attackPower = 0;
            attackRange = 0;
            attackSpeed = 0;
        }
        
        if(attackPower > 0)
            power = "공격력 +" + attackPower.ToString() + "\n";
        else if(attackPower < 0)
            power = "공격력 " + attackPower.ToString() + "\n";

        if(attackRange > 0)
            range = "공격범위 +" + attackRange.ToString() + "\n";
        else if(attackRange < 0)
            range = "공격범위 " + attackRange.ToString() + "\n";

        if(attackSpeed > 0)
            speed = "공격속도 +" + attackSpeed.ToString() + "\n";
        else if(attackSpeed < 0)
            speed = "공격속도 " + attackSpeed.ToString() + "\n";
        
    }

    public void ShowStack()
    {
        GetStack();

        Name.text = iname;
        if(rank == 0)
            Rank.text = "S등급";
        else if(rank == 1)
            Rank.text = "A등급";
        else if(rank == 2)
            Rank.text = "B등급";
        else if(rank == 3)
            Rank.text = "C등급";

        if(part == 0)
            Part.text = " / 엣지";
        else if(part == 1)
            Part.text = " / 바디";
        else if(part == 2)
            Part.text = " / 핸들";
        
        Info.text = power + range + speed;
    }
}
