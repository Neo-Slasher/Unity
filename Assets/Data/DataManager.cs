using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;
using TMPro;

public class DataManager : MonoBehaviour {
    public static DataManager instance = null;

    public DifficultyList difficultyList;
    public TraitList traitList;
    public EquipmentList equipmentList;
    public AssassinationStageList assassinationStageList;
    public ItemList itemList;
    public MonsterList monsterList;
    public StoryList storyList;
    public ExpList expList;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }


    void Start() {
#if UNITY_EDITOR
        // init difficulty data
        string DifficultyData = File.ReadAllText(Application.dataPath + "/Data/Json/Difficulty.json");
        difficultyList = JsonUtility.FromJson<DifficultyList>(DifficultyData);
        //difficultyList = ResourceDataLoad<DifficultyList>("Difficulty");

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

        // exp data
        string ExpData = File.ReadAllText(Application.dataPath + "/Data/Json/Exp.json");
        expList = JsonUtility.FromJson<ExpList>(ExpData);
#endif

#if UNITY_ANDROID
        // init difficulty data
        difficultyList = ResourceDataLoad<DifficultyList>("Difficulty");
        equipmentList = ResourceDataLoad<EquipmentList>("Equipment");
        traitList = ResourceDataLoad<TraitList>("Trait");
        assassinationStageList = ResourceDataLoad<AssassinationStageList>("AssassinationStage");
        itemList = ResourceDataLoad<ItemList>("Item");
        monsterList = ResourceDataLoad<MonsterList>("Monster");
        storyList = ResourceDataLoad<StoryList>("Story");
        expList = ResourceDataLoad<ExpList>("Exp");
#endif
    }

    public T ResourceDataLoad<T>(string name) {
        T gameData;

        string directory = "Json/";
        string appender1 = name;

        StringBuilder builder = new StringBuilder(directory);
        builder.Append(appender1);

        TextAsset jsonString = Resources.Load<TextAsset>(builder.ToString());

        gameData = JsonUtility.FromJson<T>(jsonString.ToString());


        return gameData;
    }
}