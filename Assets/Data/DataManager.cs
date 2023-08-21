using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    public DifficultyList difficultyList;
    public TraitList traitList;
    public EquipmentList equipmentList;
    public AssassinationStageList assassinationStageList;
    public ItemList itemList;
    public MonsterList monsterList;
    public StoryList storyList;

    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }


    void Start()
    {
        // init difficulty data
        string DifficultyData = File.ReadAllText(Application.dataPath + "/Data/Json/Difficulty.json");
        difficultyList = JsonUtility.FromJson<DifficultyList>(DifficultyData);

        // init Equipment data
        string EquipmentData = File.ReadAllText(Application.dataPath + "/Data/Json/Equipment.json");
        equipmentList = JsonUtility.FromJson<EquipmentList>(EquipmentData);

        // init trait data
        string TraitData = File.ReadAllText(Application.dataPath + "/Data/Json/Trait.json");
        traitList = JsonUtility.FromJson<TraitList>(TraitData);

        // assassination stage data
        string AssassinationStageData = File.ReadAllText(Application.dataPath + "/Data/Json/AssassinationStage.json");
        assassinationStageList = JsonUtility.FromJson<AssassinationStageList>(AssassinationStageData);

        // item data 
        string ItemData = File.ReadAllText(Application.dataPath + "/Data/Json/Item.json");
        itemList = JsonUtility.FromJson<ItemList>(ItemData);

        // monster data
        string MonsterData = File.ReadAllText(Application.dataPath + "/Data/Json/Monster.json");
        monsterList = JsonUtility.FromJson<MonsterList>(MonsterData);

        // stroy data
        string StoryData = File.ReadAllText(Application.dataPath + "/Data/Json/Story.json");
        storyList = JsonUtility.FromJson<StoryList>(StoryData);
    }
}
