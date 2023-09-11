using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneManager : MonoBehaviour {
    public TextMeshProUGUI typingText;
    public List<string> stories;
    public Image background;
    public List<Sprite> backgrounds;
    public int storyNumber;
    public float speed = 0.21f;
    public bool autoStory = false;
    public bool touchScreen = false;

    // Start is called before the first frame update
    void Start() {
        stories = DataManager.instance.storyList.stories[GameManager.instance.player.difficulty].story;

        if (GameManager.instance.player.difficulty == 0) // intro
            background.sprite = backgrounds[0];
        else if (GameManager.instance.player.difficulty == 8) // bad ending
            background.sprite = backgrounds[9];
        else
            background.sprite = backgrounds[8];

        StartStory();
    }

    public void OnClickSkipButton() {
        ChangeScene();   
    }

    public void ChangeScene() {
        if (GameManager.instance.player.difficulty == 0) {
            SceneManager.LoadScene("PreparationScene");
        }
        else {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void OnClickAutoButton() {
        autoStory = !autoStory;
    }

    public void OnClickScreen() {
        touchScreen = true;
    }
  

    IEnumerator Typing(TextMeshProUGUI typingText, string message, float speed) {
        for (int i = 0; i < message.Length; i++) {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }

        touchScreen = false;
        if (autoStory)
            yield return new WaitForSeconds(1.5f);
        else
            yield return new WaitUntil(() => touchScreen == true);
        storyNumber++;
        NextStory();
    }

    public void StartStory() {
        storyNumber = 0;
        NextStory();
    }

    public void NextStory() {
        if (storyNumber == stories.Count) { // debug
            storyNumber = 0;
            ChangeScene();
            return;
        }
        if (GameManager.instance.player.difficulty == 0) {
            background.sprite = backgrounds[storyNumber];
        }
        StartCoroutine(Typing(typingText, stories[storyNumber], speed));
    }
}

