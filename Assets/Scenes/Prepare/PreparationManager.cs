using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;
    public TextMeshProUGUI thirdText;
    public TextMeshProUGUI fourthText;
    public TextMeshProUGUI fifthText;


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


    public void OnClickFirstTraitButton()
    {
        firstText.text = "Lv.1";
        secondText.text = "Lv.2";
        thirdText.text = "Lv.3";
        fourthText.text = "Lv.4";
        fifthText.text = "Lv.5";
    }

    public void OnClickSecondTraitButton()
    {
        firstText.text = "Lv.6";
        secondText.text = "Lv.7";
        thirdText.text = "Lv.8";
        fourthText.text = "Lv.9";
        fifthText.text = "Lv.10";
    }

    public void OnClickThirdTraitButton()
    {
        firstText.text = "Lv.11";
        secondText.text = "Lv.12";
        thirdText.text = "Lv.13";
        fourthText.text = "Lv.14";
        fifthText.text = "Lv.15";
    }

    public void OnClickFourthTraitButton()
    {
        firstText.text = "Lv.16";
        secondText.text = "Lv.17";
        thirdText.text = "Lv.18";
        fourthText.text = "Lv.19";
        fifthText.text = "Lv.20";
    }

}
