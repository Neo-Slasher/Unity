using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreparationManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI attackRangeText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI getMoneyText;

    public int choiceDifficulty;
    public Button difficultyLeftButton;
    public Button difficultyRightButton;
    public TextMeshProUGUI difficultyLevelText;
    public TextMeshProUGUI recommandLvText;
    public TextMeshProUGUI rewardExpText;
    public TextMeshProUGUI goalMoneyText;
    public TextMeshProUGUI enemyStatusText;
    public TextMeshProUGUI normalEnhanceText;
    public TextMeshProUGUI eliteEnhanceText;
    public TextMeshProUGUI dropRankText;
    public TextMeshProUGUI enemyRespawnText;



    public GameObject traitBoard1;
    public GameObject traitBoard2;
    public GameObject traitBoard3;
    public GameObject traitBoard4;

    public List<GameObject> traitButtons;


    void Start()
    {
        LoadStatus();
        choiceDifficulty = 0;
        LoadDifficulty();
        ActivateDifficultyButton();
    }

    private void LoadStatus()
    {
        levelText.text = GameManager.instance.player.level.ToString();
        expText.text = "Exp  " + GameManager.instance.player.curExp.ToString() + "/" + GameManager.instance.player.reqExp.ToString();
        maxHpText.text = GameManager.instance.player.maxHp.ToString();
        moveSpeedText.text = GameManager.instance.player.moveSpeed.ToString();
        attackPowerText.text = GameManager.instance.player.attackPower.ToString();
        attackSpeedText.text = GameManager.instance.player.attackSpeed.ToString();
        attackRangeText.text = GameManager.instance.player.attackRange.ToString();
        moneyText.text = GameManager.instance.player.money.ToString();
    }

    public void OnClickDifficultyLeftButton()
    {
        choiceDifficulty -= 1;
        LoadDifficulty();
        ActivateDifficultyButton();
    }

    public void OnClickDifficultyRightButton()
    {
        choiceDifficulty += 1;
        LoadDifficulty();
        ActivateDifficultyButton();
    }

    public void ActivateDifficultyButton()
    {

        difficultyLeftButton.interactable = (choiceDifficulty == 0) ? false : true;
        difficultyRightButton.interactable = (choiceDifficulty == 6) ? false : true;
    }

    private void LoadDifficulty()
    {
        difficultyLevelText.text = (choiceDifficulty + 1).ToString();
        recommandLvText.text = "권장 Lv." + DataManager.instance.difficultyList.difficulty[choiceDifficulty].recommandLv.ToString();
        rewardExpText.text = "보상 EXP " + DataManager.instance.difficultyList.difficulty[choiceDifficulty].rewardExp.ToString();
        goalMoneyText.text = "- 목표금액 " + DataManager.instance.difficultyList.difficulty[choiceDifficulty].goalMoney.ToString();
        enemyStatusText.text = "- 적 체력, 이동속도, 공격력 +" + DataManager.instance.difficultyList.difficulty[choiceDifficulty].enemyStatus.ToString() + "%";
        normalEnhanceText.text = "- 일반 적이 " + DataManager.instance.difficultyList.difficulty[choiceDifficulty].normalEnhance.ToString() + "% 확률로 강화";
        eliteEnhanceText.text = "- 정예 적이 " + DataManager.instance.difficultyList.difficulty[choiceDifficulty].eliteEnhance.ToString() + "% 확률로 강화";
        dropRankText.text = "- 아이템 드롭률 +" + DataManager.instance.difficultyList.difficulty[choiceDifficulty].dropRank.ToString() + "%";
        enemyRespawnText.text = "- 적 개체수 +" + DataManager.instance.difficultyList.difficulty[choiceDifficulty].enemyRespawn.ToString() + "%";
    }

    public void OnClickStartButton() {
        SceneManager.LoadScene("DayScene");
    }

    public void OnClickCancelButton()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void OnClickFirstTraitButton() {
        traitBoard1.SetActive(true);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(false);
    }

    public void OnClickSecondTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(true);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(false);
    }

    public void OnClickThirdTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(true);
        traitBoard4.SetActive(false);
    }

    public void OnClickFourthTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(true);
    }


    public void OnClickTraitButton() {
        GameObject buttonObject = EventSystem.current.currentSelectedGameObject;

        Button traitButton = buttonObject.GetComponent<Button>();
        traitButton.interactable = false;

        int traitNumber = int.Parse(buttonObject.name);
        GameManager.instance.player.trait[traitNumber] = true;

        TraitChecking(traitNumber);
    }

    void TraitChecking(int traitNumber) {
        switch (traitNumber) {

        }
    }
}
