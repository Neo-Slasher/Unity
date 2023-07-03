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
    }


}
