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

    public GameObject[] checkMarks;
    public TMP_Text Name;
    public TMP_Text Rank;
    public TMP_Text Part;
    public TMP_Text Info;

    public string iname = "";
    public int rank = -1;
    public int part = -1;
    public double attackPower;
    public double attackRange;
    public double attackSpeed;

    string power = "";
    string range = "";
    string speed = "";
    string info = "";

    public int buttonNum = 0;

    public int selectNum;
    public string selectType;
    

    void Start()
    {
        thisimg = GetComponent<Image>();
    }

    // Update is called once per frame
    public void ItemSelect()
    {
        ChangeImage();
        ShowStack();
    }

    public void ChangeImage()
    {
        change_img.sprite = thisimg.sprite;
        for(int i = 0; i < 3; i++)
        {
            if(i == buttonNum)
            {
                checkMarks[i].SetActive(true);
            }
            else
               checkMarks[i].SetActive(false); 
        }
    }

    public void GetStack()
    {
        
        if(ItemSlot.shopslot[buttonNum].shopType == "equip")
        {
            iname = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].name;
            rank = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].rank;
            part = DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].part;
            attackPower = (int)DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackPower;
            attackRange = (int)DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackRange;
            attackSpeed = (int)DataManager.instance.equipmentList.equipment[ItemSlot.shopslot[buttonNum].shopNum].attackSpeed;
        }
        else if(ItemSlot.shopslot[buttonNum].shopType == "item")
        {
            iname = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].name;
            rank = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].rank;
            part = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].category;
            attackPower = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackPowerValue;
            attackRange = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackRangeValue;
            attackSpeed = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackSpeedValue;
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
        if(ItemSlot.shopslot[buttonNum].shopType == "equip")
        {
            if(part == 0)
                Part.text = " / 엣지";
            else if(part == 1)
                Part.text = " / 바디";
            else if(part == 2)
                Part.text = " / 핸들";

            Info.text = power + range + speed;
        }
        else if(ItemSlot.shopslot[buttonNum].shopType == "item")
        {
            bool powerC, speedC, rangeC; //아이템 문자열 치환 여부
            double powerP, speedP, rangeP; //플레이어 정보
            string script; //출력할 문자열, 치환할 문자열

            script = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].script; //치환할 문자열

            powerP = GameManager.instance.player.attackPower; 
            speedP = GameManager.instance.player.attackSpeed;
            rangeP = GameManager.instance.player.attackRange;

            powerC = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackPowerCalc;
            speedC = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackSpeedCalc;
            rangeC = DataManager.instance.itemList.item[ItemSlot.shopslot[buttonNum].shopNum].attackRangeCalc;

            //출력
            if(part == 0)
                Part.text = " / 공격";
            else if(part == 0)
                Part.text = " / 방어";
            else if(part == 0)
                Part.text = " / 보조";

            if(powerC)
            {
                info = script.Replace("#at#", (powerP * attackPower).ToString());
            }
            else if(speedC)
            {
                info = script.Replace("#as#", (speedP * attackSpeed).ToString());
            }
            else if(rangeC)
            {
                info = script.Replace("#ar#", (rangeP * attackRange).ToString());
            }

            Info.text = "<size=20> [+" + info + "]</size>";
        }  
    }

    public void CloseInfo()
    {
        change_img.sprite = Resources.Load<Sprite>("slotBack") as Sprite;
        Name.text = "";
        Rank.text = "";
        Part.text = "";
        Info.text = "";
    }
    public void CompareInfo()
    {
        selectType = ItemSlot.shopslot[buttonNum].shopType;
        selectNum = ItemSlot.shopslot[buttonNum].shopNum;

        ItemSlot.selectType = selectType;
        ItemSlot.selectNum = selectNum;
        ItemSlot.btnNum = buttonNum;
        Debug.Log("selectNum "+ selectNum.ToString());
        Debug.Log("selectType" + selectType);
    }
}
