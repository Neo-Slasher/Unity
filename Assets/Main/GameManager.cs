using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Item {

}

public class Status {
    public int hp;
    public float speed;
    public int offensePower;
    public float attackRange;
    public float attackSpeed;
}

public class PlayData {
    public int level;
    public float timeElapsed;
    public int money;
    public List<Item>[] inventory;
    public Item[] itemSlot = new Item[3];
    public int trait; // int -> ??? 나중에 수정
    public int traitPoint;
    public int combatPower; // 전투력
    public Status status; 
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool hasSavedData;
    public PlayData myData;


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
            string savedData = File.ReadAllText(Application.dataPath + "/UserData.json");
            myData = JsonUtility.FromJson<PlayData>(savedData);
        }
    }
}
