using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneManager : MonoBehaviour {
    public TextMeshProUGUI typingText;
    public List<string> stories;
    public int storyNumber;
    public float speed = 0.07f;

    // Start is called before the first frame update
    void Start() {
        stories = DataManager.instance.storyList.stories[GameManager.instance.player.day - 1].story;
        StartStory();
    }

    public void OnClickSkipButton() {
        SceneManager.LoadScene("PreparationScene");
    }


    public void OnClickAutoButton() {

    }

    IEnumerator Typing(TextMeshProUGUI typingText, string message, float speed) {
        for (int i = 0; i < message.Length; i++) {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(1.5f);
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
            return;
        }
        StartCoroutine(Typing(typingText, stories[storyNumber], speed));
    }
}

