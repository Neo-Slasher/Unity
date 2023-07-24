using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //특성에 따른 아이템 슬롯 개수 변화와 장비 및 아이템의 랜덤 출력

    public static ItemSlot Instance;
    public int slotNum;
    public Button[] slots;
    public int[] equipNums;
    public int[] itemNums;

    private int totalEquip; //
    private int totalItem; //

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        slotNum = 1;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for(int i = 2; i >= slotNum; i--)
        {
            slots[i].interactable = false;
            slots[i].transform.Find("Image").gameObject.SetActive(true);
            slots[i].transform.Find("alpha").gameObject.SetActive(false);
            slots[i].transform.Find("rank").gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    void GetSlotNum() //특성에 따른 슬롯 개수 결정
    {

    }

    void RandomItem() //장비 및 아이템 종류 랜덤으로 선택
    {
        
    }

    void PrintItem() //선택된 아이템 슬롯에 출력
    {
        
    }
}
