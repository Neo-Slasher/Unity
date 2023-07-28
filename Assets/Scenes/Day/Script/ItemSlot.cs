using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    //특성에 따른 아이템 슬롯 개수 변화와 장비 및 아이템의 랜덤 출력

    public static ItemSlot Instance;
    public int slotNum = 3;
    public Button[] slots;
    public TMP_Text ItemRank;
    public TMP_Text ItemAlpha;
    public Image ItemImage;

    public int shopMinRank = 3;
    public int shopMaxRank = 2;
    public List<int> equips = new List<int>();//랭크 기준에 부합하는 장비들 모음

    
    private int totalEquip = 48; //전체 장비 개수
    private int totalItem = 60; //전체 아이템 개수
    private int equipORitem;//장비와 아이템 랜덤 계수
    private int equipRank;//장비의 랭크(최대, 최소 랭크와 비교용)
    
    public class ShopSlot
    {
        public string shopType;
        public int shopNum;
    } 
    public List<ShopSlot> shopslot = new List<ShopSlot>();


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

    void Start()
    {   
        GetTraitInfo();

        for(int i = 2; i >= slotNum; i--)
        {
            slots[i].interactable = false;
            slots[i].transform.Find("Image").gameObject.SetActive(true);
            slots[i].transform.Find("alpha").gameObject.SetActive(false);
            slots[i].transform.Find("rank").gameObject.SetActive(false);
        }

        PrintItem();
    }

    void Update()
    {
        
    }

    void GetTraitInfo() //특성에 따른 아이템 랭크 및 슬롯 개수 결정
    {
        for(int i = 0; i < totalEquip; i++) //아이템 랭크
        {
            equipRank = DataManager.instance.equipmentList.equipment[i].rank;
            if(equipRank <= shopMinRank && equipRank >= shopMaxRank)
                equips.Add(i);
        }
    }

    void RandomItem() //장비 및 아이템 종류 랜덤으로 선택
    {
        for(int i = 0; i < slotNum; i++)
        {
            equipORitem = Random.Range(0,10);

            if(equipORitem < 6)
            {
                int equipNum = Random.Range(0, equips.Count);
                shopslot.Add(new ShopSlot(){shopType = "equip", shopNum = equips[equipNum]});
            }
            else if(equipORitem >=6 && equipORitem <= 9)
            {
                shopslot.Add(new ShopSlot(){shopType = "item", shopNum = Random.Range(0, totalItem)});
            }
            Debug.Log("shopType " + shopslot[i].shopType);
            Debug.Log("shopNum " + shopslot[i].shopNum.ToString());
        }
    }

    void PrintItem() //선택된 아이템 슬롯에 출력
    {
        RandomItem();

        string iRank = " ";
        
        for(int i = 0; i < slotNum; i++)
        {
            ItemImage = slots[i].gameObject.GetComponent<Image>();
            ItemAlpha = slots[i].transform.Find("alpha").gameObject.GetComponent<TMP_Text>();
            ItemRank = slots[i].transform.Find("rank").gameObject.GetComponent<TMP_Text>();

            if(shopslot[i].shopType == "equip")
            {
                ItemImage.sprite = Resources.Load<Sprite>("Equip/" + i.ToString()) as Sprite; ////이미지 들어오면 경로 변경해야함!!!!!!
                ItemAlpha.text = DataManager.instance.equipmentList.equipment[shopslot[i].shopNum].price + "α";
            
                if(DataManager.instance.equipmentList.equipment[shopslot[i].shopNum].rank == 0)
                    iRank = "S";
                else if(DataManager.instance.equipmentList.equipment[shopslot[i].shopNum].rank == 1)
                    iRank = "A";
                else if(DataManager.instance.equipmentList.equipment[shopslot[i].shopNum].rank == 2)
                    iRank = "B";
                else if(DataManager.instance.equipmentList.equipment[shopslot[i].shopNum].rank == 3)
                    iRank = "C";

                ItemRank.text = iRank;
            }
            else if(shopslot[i].shopType == "item") //아이템 파일 만들어지면 수정!
            {
                ItemImage.sprite = Resources.Load<Sprite>("Item/" + i.ToString()) as Sprite; ////이미지 들어오면 경로dddd 변경해야함!!!!!!
                ItemAlpha.text = "Alapha";
                ItemRank.text = "Rank";
            }
        }
    }
}
