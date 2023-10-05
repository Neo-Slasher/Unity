using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (!File.Exists(Application.persistentDataPath + "/UserData.json")) {
            hasSavedData = false;
            InitPlayerData();
        } else {
            hasSavedData = true;
            LoadPlayerData();
        }
    }


    public void InitPlayerData() {
        // init player
        player = new Player();
        //player.item.Add(DataManager.instance.itemList.item[6]);
#if UNITY_EDITOR
        string initData = File.ReadAllText(Application.dataPath + "/Data/Json/InitPlayer.json");
        player = JsonUtility.FromJson<Player>(initData);
#endif
#if UNITY_ANDROID
        player = ResourceDataLoad<Player>("InitPlayer");
#endif
        SavePlayerData();
    }

    public void SavePlayerData() {
        player.playingGame = true;
        string json = JsonUtility.ToJson(player);

        File.WriteAllText(Application.persistentDataPath + "/UserData.json", json);

    }

    public void LoadPlayerData() {
        Debug.Log("LoadPlayerData");
        string savedData = File.ReadAllText(Application.persistentDataPath + "/UserData.json");
        Debug.Log(savedData);
        player = JsonUtility.FromJson<Player>(savedData);
    }

    void Cheat()
    {
        player.maxHp = 2000;
        player.curHp = 2000;
        player.level = 20;
        player.money = 100000;
    }

    public void ChangeJoyStickSize(float value) {
        player.joyStickSize = value;
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
