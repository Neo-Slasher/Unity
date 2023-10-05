using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CompareItem : MonoBehaviour
{
    public FightingPower FightingPower;
    public ItemSlot ItemSlot;
    public GameObject Pbutton;
    //////////////////////////////////////// 출력할 것들
    public GameObject BackGround;
    public GameObject equipPanel;
    public GameObject itemPanel;
    public GameObject selectPanel;

    public Image equipImage;
    public TMP_Text EName;
    public TMP_Text ERank;
    public TMP_Text EPart;
    public TMP_Text EInfo;

    public Image[] havingItems;
    public Image itemImage;
    public TMP_Text IName;
    public TMP_Text IRank;
    public TMP_Text IPart;
    public TMP_Text IInfo;

    public Image selectImage;
    public TMP_Text SName;
    public TMP_Text SRank;
    public TMP_Text SPart;
    public TMP_Text SInfo;
    //////////////////////////////////////

    int selectNum;
    string selectType;
    int price;

    public string iname = "";
    public int rank = -1;
    public int part = -1;
    public double attackPower;
    public double attackRange;
    public double attackSpeed;

    double iattackPower, iattackSpeed, iattackRange;//아이템 관련
    public int printigNum = -1;
    List<int> itemNums = new List<int>();
    List<int> itemRanks = new List<int>();

    public int chosen = -1; //비교 후 출력할 아이템 

    string power = "";
    string range = "";
    string speed = "";
    string info = "";

    public void ItemCompare()
    {
        if (ItemSlot.btnNum != -1)
        {
            Debug.Log("아이템 비교");
            selectNum = ItemSlot.selectNum;
            selectType = ItemSlot.selectType;

            BackGround.SetActive(true);
            GetInfo();
            if (selectType == "equip")
            {
                equipPanel.SetActive(true);
                PrintingEquip();
            }
            else if (selectType == "item")
            {
                itemPanel.SetActive(true);
                PrintingItem();
            }
            selectPanel.SetActive(true);
            PrintingSelect();

            if(GameManager.instance.player.money < price)
                Pbutton.GetComponent<Button>().interactable = false;
        }

    }

    public void Reset()
    {
        equipImage.sprite = Resources.Load<Sprite>("slotBack") as Sprite;
        EName.text = " ";
        ERank.text = " ";
        EPart.text = " ";
        EInfo.text = " ";

        itemImage.sprite = Resources.Load<Sprite>("slotBack") as Sprite;
        IName.text = " ";
        IRank.text = " ";
        IPart.text = " ";
        IInfo.text = " ";
    }

    public void Cancel()
    {
        Reset();
        chosen = -1;
        itemImage.gameObject.SetActive(false);
        BackGround.SetActive(false);
        selectPanel.SetActive(false);
        equipPanel.SetActive(false);
        itemPanel.SetActive(false);
        Debug.Log(GameManager.instance.player.money);

    }
    public void Purchase()
    {
        if (GameManager.instance.player.money >= price)
        {
            if (chosen == -1)
            {
                if (selectType == "equip")
                {
                    Debug.Log("selectNum" + selectNum.ToString());
                    Debug.Log(DataManager.instance.equipmentList.equipment[selectNum].index);
                    GameManager.instance.player.equipment.Add(DataManager.instance.equipmentList.equipment[selectNum]);
                    ItemSlot.slots[ItemSlot.btnNum].interactable = false;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<SelectItem>().buttonNum = -1;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<Image>().sprite = Resources.Load<Sprite>("slotBack") as Sprite;
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("Button").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("alpha").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("rank").gameObject.SetActive(false);
                    GameManager.instance.player.money -= price;
                    ItemSlot.btnNum = -1;
                    Cancel();
                }
                else if (selectType == "item" && GameManager.instance.player.item.Count < GameManager.instance.player.itemSlot)
                {
                    GameManager.instance.player.item.Add(DataManager.instance.itemList.item[selectNum]);
                    ItemSlot.slots[ItemSlot.btnNum].interactable = false;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<SelectItem>().buttonNum = -1;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<Image>().sprite = Resources.Load<Sprite>("slotBack") as Sprite;
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("Button").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("alpha").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("rank").gameObject.SetActive(false);
                    GameManager.instance.player.money -= price;
                    ItemSlot.btnNum = -1;
                    Cancel();
                }
            }
            else
            {
                if (selectType == "equip")
                {
                    Debug.Log("chosen" + chosen.ToString());
                    GameManager.instance.player.equipment[chosen] = DataManager.instance.equipmentList.equipment[selectNum];
                    ItemSlot.slots[ItemSlot.btnNum].interactable = false;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<SelectItem>().buttonNum = -1;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<Image>().sprite = Resources.Load<Sprite>("slotBack") as Sprite;
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("Button").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("alpha").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("rank").gameObject.SetActive(false);
                    GameManager.instance.player.money -= price;
                    ItemSlot.btnNum = -1;
                    Cancel();
                }
                else if (selectType == "item")
                {
                    Debug.Log("chosen" + chosen.ToString());
                    GameManager.instance.player.item[chosen] = DataManager.instance.itemList.item[selectNum];
                    ItemSlot.slots[ItemSlot.btnNum].interactable = false;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<SelectItem>().buttonNum = -1;
                    ItemSlot.slots[ItemSlot.btnNum].GetComponent<Image>().sprite = Resources.Load<Sprite>("slotBack") as Sprite;
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("Button").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("alpha").gameObject.SetActive(false);
                    ItemSlot.slots[ItemSlot.btnNum].transform.Find("rank").gameObject.SetActive(false);
                    GameManager.instance.player.money -= price;
                    ItemSlot.btnNum = -1;
                    Cancel();
                }
            }

            if (selectType == "equip")
            {
                FightingPower.selectNum = selectNum;
                FightingPower.ChangingPower();
            }
            if(selectType == "item")
            {
                FightingPower.Printing();
            }

        }
        else
            Debug.Log("돈이 부족합니다");

        chosen = -1;
    }
    void GetInfo()
    {
        if (selectType == "equip")
        {
            iname = DataManager.instance.equipmentList.equipment[selectNum].name;
            rank = DataManager.instance.equipmentList.equipment[selectNum].rank;
            part = DataManager.instance.equipmentList.equipment[selectNum].part;
            price = DataManager.instance.equipmentList.equipment[selectNum].price;
            attackPower = (int)DataManager.instance.equipmentList.equipment[selectNum].attackPower;
            attackRange = (int)DataManager.instance.equipmentList.equipment[selectNum].attackRange;
            attackSpeed = (int)DataManager.instance.equipmentList.equipment[selectNum].attackSpeed;

            if (attackPower > 0)
                power = "공격력 +" + attackPower.ToString() + "\n";
            else if (attackPower < 0)
                power = "공격력 " + attackPower.ToString() + "\n";

            if (attackRange > 0)
                range = "공격범위 +" + attackRange.ToString() + "\n";
            else if (attackRange < 0)
                range = "공격범위 " + attackRange.ToString() + "\n";

            if (attackSpeed > 0)
                speed = "공격속도 +" + attackSpeed.ToString() + "\n";
            else if (attackSpeed < 0)
                speed = "공격속도 " + attackSpeed.ToString() + "\n";
        }
        else if (selectType == "item")
        {
            iname = DataManager.instance.itemList.item[selectNum].name;
            rank = DataManager.instance.itemList.item[selectNum].rank;
            part = DataManager.instance.itemList.item[selectNum].category;
            price = DataManager.instance.itemList.item[selectNum].price;
            attackPower = DataManager.instance.itemList.item[selectNum].attackPowerValue;
            attackRange = DataManager.instance.itemList.item[selectNum].attackRangeValue;
            attackSpeed = DataManager.instance.itemList.item[selectNum].attackSpeedValue;
        }


    }
    void PrintingEquip() //장비창 출력
    {
        if (GameManager.instance.player.equipment.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.player.equipment.Count; i++)
            {
                int compPart = GameManager.instance.player.equipment[i].part;
                if (compPart == part)
                {
                    chosen = i;

                    string Epower, Erange, Espeed;
                    equipImage.gameObject.SetActive(true);
                    equipImage.sprite = Resources.Load<Sprite>("Equip/" + GameManager.instance.player.equipment[chosen].name) as Sprite;
                    EName.text = GameManager.instance.player.equipment[chosen].name;
                    int compRank = GameManager.instance.player.equipment[chosen].rank;
                    if (rank == 3)
                        ERank.text = "S등급";
                    else if (rank == 2)
                        ERank.text = "A등급";
                    else if (rank == 1)
                        ERank.text = "B등급";
                    else if (rank == 0)
                        ERank.text = "C등급";

                    if (part == 0)
                        EPart.text = " / 엣지";
                    else if (part == 1)
                        EPart.text = " / 바디";
                    else if (part == 2)
                        EPart.text = " / 핸들";

                    if (GameManager.instance.player.equipment[chosen].attackPower >= 0)
                        Epower = "공격력 +" + GameManager.instance.player.equipment[chosen].attackPower.ToString() + "\n";
                    else
                    {
                        Epower = "공격력 " + GameManager.instance.player.equipment[chosen].attackPower.ToString() + "\n";
                    }

                    if (GameManager.instance.player.equipment[chosen].attackRange >= 0)
                        Erange = "공격범위 +" + GameManager.instance.player.equipment[chosen].attackRange.ToString() + "\n";
                    else
                    {
                        Erange = "공격범위 " + GameManager.instance.player.equipment[i].attackRange.ToString() + "\n";
                    }

                    if (GameManager.instance.player.equipment[chosen].attackSpeed >= 0)
                        Espeed = "공격속도 +" + GameManager.instance.player.equipment[chosen].attackSpeed.ToString() + "\n";
                    else
                    {
                        Espeed = "공격속도 " + GameManager.instance.player.equipment[chosen].attackSpeed.ToString() + "\n";
                    }

                    EInfo.text = Epower + Erange + Espeed;
                }
            }
        }


    }
    void PrintingItem() //아이템 창 출력
    {
        if (GameManager.instance.player.item.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.player.item.Count; i++)
            {
                havingItems[i].gameObject.SetActive(true);
                havingItems[i].sprite = Resources.Load<Sprite>("Item/" + GameManager.instance.player.item[i].name) as Sprite;
                itemNums.Add(GameManager.instance.player.item[i].itemIdx);
                itemRanks.Add(GameManager.instance.player.item[i].rank);
            }
        }
    }

    public void ItemPrinting()
    {
        if (chosen != -1)
        {
            Debug.Log(chosen);
            bool powerC, speedC, rangeC; //아이템 문자열 치환 여부
            double powerP, speedP, rangeP; //플레이어 정보
            string script; //출력할 문자열, 치환할 문자열

            itemImage.gameObject.SetActive(true);
            script = GameManager.instance.player.item[chosen].script; //치환할 문자열

            powerP = GameManager.instance.player.attackPower;
            speedP = GameManager.instance.player.attackSpeed;
            rangeP = GameManager.instance.player.attackRange;

            powerC = GameManager.instance.player.item[chosen].attackPowerCalc;
            speedC = GameManager.instance.player.item[chosen].attackSpeedCalc;
            rangeC = GameManager.instance.player.item[chosen].attackRangeCalc;

            //출력
            IName.text = GameManager.instance.player.item[chosen].name;
            itemImage.sprite = Resources.Load<Sprite>("item/" + GameManager.instance.player.item[chosen].name) as Sprite;

            int irank = GameManager.instance.player.item[chosen].rank;
            int category = GameManager.instance.player.item[chosen].category;

            if (irank == 3)
                IRank.text = "S등급";
            else if (irank == 2)
                IRank.text = "A등급";
            else if (irank == 1)
                IRank.text = "B등급";
            else if (irank == 0)
                IRank.text = "C등급";

            if (category == 0)
                IPart.text = " / 공격";
            else if (category == 1)
                IPart.text = " / 방어";
            else if (category == 2)
                IPart.text = " / 보조";

            if(powerC)
            {
                script = script.Replace("#at#", (powerP * attackPower).ToString());
            }
            if(speedC)
            {
                script = script.Replace("#as#", (speedP * attackSpeed).ToString());
            }
            if(rangeC)
            {
                script = script.Replace("#ar#", (rangeP * attackRange).ToString());
            }

            info = script;

            IInfo.text = "<size=20>" + info + "</size>";

            Debug.Log("chosen" + chosen.ToString());
        }
        else if (chosen == -1)
        {
            itemImage.gameObject.SetActive(false);
            IName.text = " ";
            IRank.text = " ";
            IPart.text = " ";
            IInfo.text = " ";
        }
    }


    void PrintingSelect()
    {
        if (selectType == "equip")
        {
            selectImage.sprite = Resources.Load<Sprite>("Equip/" + iname) as Sprite;
            SName.text = iname;
            if (rank == 3)
                SRank.text = "S등급";
            else if (rank == 2)
                SRank.text = "A등급";
            else if (rank == 1)
                SRank.text = "B등급";
            else if (rank == 0)
                SRank.text = "C등급";

            if (part == 0)
                SPart.text = " / 엣지";
            else if (part == 1)
                SPart.text = " / 바디";
            else if (part == 2)
                SPart.text = " / 핸들";

            SInfo.text = power + range + speed;
        }
        else if (selectType == "item")
        {
            bool powerC, speedC, rangeC; //아이템 문자열 치환 여부
            double powerP, speedP, rangeP; //플레이어 정보
            string script; //출력할 문자열, 치환할 문자열

            script = DataManager.instance.itemList.item[selectNum].script; //치환할 문자열

            powerP = GameManager.instance.player.attackPower;
            speedP = GameManager.instance.player.attackSpeed;
            rangeP = GameManager.instance.player.attackRange;

            powerC = DataManager.instance.itemList.item[selectNum].attackPowerCalc;
            speedC = DataManager.instance.itemList.item[selectNum].attackSpeedCalc;
            rangeC = DataManager.instance.itemList.item[selectNum].attackRangeCalc;

            //출력
            SName.text = iname;
            selectImage.sprite = Resources.Load<Sprite>("item/" + iname) as Sprite;

            if (rank == 3)
                SRank.text = "S등급";
            else if (rank == 2)
                SRank.text = "A등급";
            else if (rank == 1)
                SRank.text = "B등급";
            else if (rank == 0)
                SRank.text = "C등급";

            if (part == 0)
                SPart.text = " / 공격";
            else if (part == 1)
                SPart.text = " / 방어";
            else if (part == 2)
                SPart.text = " / 보조";

            if(powerC)
            {
                script = script.Replace("#at#", (powerP * attackPower).ToString());
            }
            if(speedC)
            {
                script = script.Replace("#as#", (speedP * attackSpeed).ToString());
            }
            if(rangeC)
            {
                script = script.Replace("#ar#", (rangeP * attackRange).ToString());
            }

            info = script;

            SInfo.text = "<size=20>" + info + "</size>";
        }
    }
}




