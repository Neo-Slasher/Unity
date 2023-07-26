using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool hasSavedData;
    public Player player;


    
    void Awake() {
        if (instance == null)  
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);


        if (!File.Exists(Application.dataPath + "/UserData.json")) {
            hasSavedData = false;
        } else {
            hasSavedData = true;
        }

    }


    public void InitPlayerData() {
        // init player
        this.player = new Player();
        string initData = File.ReadAllText(Application.dataPath + "/Data/Json/InitPlayer.json");
        this.player =  JsonUtility.FromJson<Player>(initData);

        // save player
        string json = JsonUtility.ToJson(this.player);
        File.WriteAllText(Application.dataPath + "/UserData.json", json);
        // File.WriteAllText(Application.persistentDataPath + "/UserData.json", json);
    }

    public void LoadPlayerData() {
        string savedData = File.ReadAllText(Application.dataPath + "/UserData.json");
        //string savedData = File.ReadAllText(Application.persistentDataPath + "/UserData.json");
        this.player = JsonUtility.FromJson<Player>(savedData);
    }
}
