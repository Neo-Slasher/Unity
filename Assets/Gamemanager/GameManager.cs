using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Item {

}


// TODO: Character 클래스와 통합 예정
// 죽은 클래스
public class PlayData {
    public int level;
    public float timeElapsed;
    public int money;
    public List<Item>[] inventory;
    public Item[] itemSlot = new Item[3];
    public int trait; // int -> ??? 나중에 수정
    public int traitPoint;
    public int combatPower; // 전투력
    // public Status status; 
}


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
        this.player = new Player();
        File.WriteAllText(Application.dataPath + "/UserData.json", JsonUtility.ToJson(this.player));
    }

    public void LoadPlayerData() {
        string savedData = File.ReadAllText(Application.dataPath + "/UserData.json");
        this.player = JsonUtility.FromJson<Player>(savedData);
    }
}
