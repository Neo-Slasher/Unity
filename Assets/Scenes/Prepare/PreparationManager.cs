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

    public Button[] traitButtons;

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
        int traitNumber = int.Parse(buttonObject.name);

        DisableTraitInSameLevel(traitNumber);

        GameManager.instance.player.trait[traitNumber] = true;

        Button traitButton = buttonObject.GetComponent<Button>();
        traitButton.interactable = false;
    }

    void DisableTraitInSameLevel(int traitNumber) {
        switch (traitNumber) {
            case 1:
            case 2:
                traitButtons[1].interactable = true;
                traitButtons[2].interactable = true;
                GameManager.instance.player.trait[1] = false;
                GameManager.instance.player.trait[2] = false;
                break;
            case 3:
            case 4:
            case 5:
                traitButtons[3].interactable = true;
                traitButtons[4].interactable = true;
                traitButtons[5].interactable = true;
                GameManager.instance.player.trait[3] = false;
                GameManager.instance.player.trait[4] = false;
                GameManager.instance.player.trait[5] = false;
                break;
            case 6:
            case 7:
            case 8:
                traitButtons[6].interactable = true;
                traitButtons[7].interactable = true;
                traitButtons[8].interactable = true;
                GameManager.instance.player.trait[6] = false;
                GameManager.instance.player.trait[7] = false;
                GameManager.instance.player.trait[8] = false;
                break;
            case 9:
            case 10:
                traitButtons[9].interactable = true;
                traitButtons[10].interactable = true;
                GameManager.instance.player.trait[9] = false;
                GameManager.instance.player.trait[10] = false;
                break;
            case 11:
            case 12:
            case 13:
                traitButtons[11].interactable = true;
                traitButtons[12].interactable = true;
                traitButtons[13].interactable = true;
                GameManager.instance.player.trait[11] = false;
                GameManager.instance.player.trait[12] = false;
                GameManager.instance.player.trait[13] = false;
                break;
            case 14:
            case 15:
                traitButtons[14].interactable = true;
                traitButtons[15].interactable = true;
                GameManager.instance.player.trait[14] = false;
                GameManager.instance.player.trait[15] = false;
                break;
            case 16:
            case 17:
            case 18:
            case 19:
                traitButtons[16].interactable = true;
                traitButtons[17].interactable = true;
                traitButtons[18].interactable = true;
                traitButtons[19].interactable = true;
                GameManager.instance.player.trait[16] = false;
                GameManager.instance.player.trait[17] = false;
                GameManager.instance.player.trait[18] = false;
                GameManager.instance.player.trait[19] = false;
                break;
            case 20:
            case 21:
            case 22:
                traitButtons[20].interactable = true;
                traitButtons[21].interactable = true;
                traitButtons[22].interactable = true;
                GameManager.instance.player.trait[20] = false;
                GameManager.instance.player.trait[21] = false;
                GameManager.instance.player.trait[22] = false;
                break;
            case 23:
            case 24:
            case 25:
                traitButtons[23].interactable = true;
                traitButtons[24].interactable = true;
                traitButtons[25].interactable = true;
                GameManager.instance.player.trait[23] = false;
                GameManager.instance.player.trait[24] = false;
                GameManager.instance.player.trait[25] = false;
                break;
            case 26:
            case 27:
            case 28:
            case 29:
                traitButtons[26].interactable = true;
                traitButtons[27].interactable = true;
                traitButtons[28].interactable = true;
                traitButtons[29].interactable = true;
                GameManager.instance.player.trait[26] = false;
                GameManager.instance.player.trait[27] = false;
                GameManager.instance.player.trait[28] = false;
                GameManager.instance.player.trait[29] = false;
                break;
            case 30:
            case 31:
            case 32:
                traitButtons[30].interactable = true;
                traitButtons[31].interactable = true;
                traitButtons[32].interactable = true;
                GameManager.instance.player.trait[30] = false;
                GameManager.instance.player.trait[31] = false;
                GameManager.instance.player.trait[32] = false;
                break;
            case 33:
            case 34:
            case 35:
                traitButtons[33].interactable = true;
                traitButtons[34].interactable = true;
                traitButtons[35].interactable = true;
                GameManager.instance.player.trait[33] = false;
                GameManager.instance.player.trait[34] = false;
                GameManager.instance.player.trait[35] = false;
                break;
            case 36:
            case 37:
            case 38:
                traitButtons[36].interactable = true;
                traitButtons[37].interactable = true;
                traitButtons[38].interactable = true;
                GameManager.instance.player.trait[36] = false;
                GameManager.instance.player.trait[37] = false;
                GameManager.instance.player.trait[38] = false;
                break;
            case 39:
            case 40:
            case 41:
                traitButtons[39].interactable = true;
                traitButtons[40].interactable = true;
                traitButtons[41].interactable = true;
                GameManager.instance.player.trait[39] = false;
                GameManager.instance.player.trait[40] = false;
                GameManager.instance.player.trait[41] = false;
                break;
            case 42:
            case 43:
            case 44:
            case 45:
                traitButtons[42].interactable = true;
                traitButtons[43].interactable = true;
                traitButtons[44].interactable = true;
                traitButtons[45].interactable = true;
                GameManager.instance.player.trait[42] = false;
                GameManager.instance.player.trait[43] = false;
                GameManager.instance.player.trait[44] = false;
                GameManager.instance.player.trait[45] = false;
                break;
            case 46:
            case 47:
            case 48:
                traitButtons[46].interactable = true;
                traitButtons[47].interactable = true;
                traitButtons[48].interactable = true;
                GameManager.instance.player.trait[46] = false;
                GameManager.instance.player.trait[47] = false;
                GameManager.instance.player.trait[48] = false;
                break;
            case 49:
            case 50:
            case 51:
            case 52:
                traitButtons[49].interactable = true;
                traitButtons[50].interactable = true;
                traitButtons[51].interactable = true;
                traitButtons[52].interactable = true;
                GameManager.instance.player.trait[49] = false;
                GameManager.instance.player.trait[50] = false;
                GameManager.instance.player.trait[51] = false;
                GameManager.instance.player.trait[52] = false;
                break;
            case 53:
            case 54:
            case 55:
                traitButtons[53].interactable = true;
                traitButtons[54].interactable = true;
                traitButtons[55].interactable = true;
                GameManager.instance.player.trait[53] = false;
                GameManager.instance.player.trait[54] = false;
                GameManager.instance.player.trait[55] = false;
                break;
            case 56:
            case 57:
            case 58:
                traitButtons[56].interactable = true;
                traitButtons[57].interactable = true;
                traitButtons[58].interactable = true;
                GameManager.instance.player.trait[56] = false;
                GameManager.instance.player.trait[57] = false;
                GameManager.instance.player.trait[58] = false;
                break;
            case 59:
            case 60:
            case 61:
            case 62:
                traitButtons[59].interactable = true;
                traitButtons[60].interactable = true;
                traitButtons[61].interactable = true;
                traitButtons[62].interactable = true;
                GameManager.instance.player.trait[59] = false;
                GameManager.instance.player.trait[60] = false;
                GameManager.instance.player.trait[61] = false;
                GameManager.instance.player.trait[62] = false;
                break;

        }
    }
}