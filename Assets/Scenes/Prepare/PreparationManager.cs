using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreparationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSkipButton() {
        SceneManager.LoadScene("DayScene");
    }

    public void OnClickTestButton() {
        Debug.Log(GameManager.instance.player.getHp());
    }
}
