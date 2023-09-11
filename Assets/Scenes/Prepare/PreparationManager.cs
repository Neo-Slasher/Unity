using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PreparationManager;

public class PreparationManager : MonoBehaviour
{
    public PreparationTraitManager traitManager;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI addMaxHpText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI addMoveSpeedText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI addAttackPowerText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI addAttackSpeedText;
    public TextMeshProUGUI attackRangeText;
    public TextMeshProUGUI addAttackRangeText;
    public TextMeshProUGUI startMoneyText;
    public TextMeshProUGUI getMoneyText;    

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


    public TextMeshProUGUI traitName;
    public TextMeshProUGUI traitLv;
    public TextMeshProUGUI traitScript;

    public GameObject traitBoard1;
    public GameObject traitBoard2;
    public GameObject traitBoard3;
    public GameObject traitBoard4;
    public Image traitImage;

    public Image levelSelect1;
    public Image levelSelect2;
    public Image levelSelect3;
    public Image levelSelect4;
    public Sprite levelBackground1;
    public Sprite levelBackgroundSelect1;
    public Sprite levelBackground2;
    public Sprite levelBackgroundSelect2;
    public Sprite levelBackground3;
    public Sprite levelBackgroundSelect3;
    public Sprite levelBackground4;
    public Sprite levelBackgroundSelect4;


    public Button[] traitButtons;
    private GameObject[] traitButtonss = new GameObject[63];

    public Sprite[] traitImages;

    public Player addValueByTrait;

    public Button startButton;
    public int traitCount;

    void Start() {
        traitCount = 0;
        LoadStatus();
        LoadAddStatus();
        LoadDifficulty();
        ActivateDifficultyButton();
        LoadTraitButton();
        LoadSelectedTraitUI();
        UnActiveTraitButton();
        UnActiveTraitBoard();

        OnClickFirstTraitButton();
        CheckStartButton();
    }

    // GameObject.Find는 Active된 오브젝트만 찾을 수 있으므로 주
    private void LoadTraitButton() {
        for (int i = 1; i <= 62; ++i) {
            traitButtonss[i] = GameObject.Find(i.ToString());
            traitButtonss[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = traitImages[DataManager.instance.traitList.trait[i-1].imageIndex];
        }
    }

    private void LoadSelectedTraitUI() {
        Trait tempTrait = DataManager.instance.traitList.trait[0];
        traitName.text = tempTrait.name;
        traitLv.text = tempTrait.requireLv.ToString() + " / " + (tempTrait.rank == 0 ? "일반" : "핵심");
        traitScript.text = tempTrait.script;
        traitImage.sprite = traitButtonss[1].GetComponent<Image>().sprite; // 인덱스는 1부터 시작
        traitImage.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = traitButtonss[1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite;
    }

    private int GetMaxTraitCount() {
        int level = GameManager.instance.player.level;
        if (level == 1) return 2;
        else if (level == 2) return 5;
        else if (level == 3) return 8;
        else if (level == 4) return 10;
        else if (level == 5) return 13;
        else if (level == 6) return 15;
        else if (level == 7) return 19;
        else if (level == 8) return 22;
        else if (level == 9) return 25;
        else if (level == 10) return 29;
        else if (level == 11) return 32;
        else if (level == 12) return 35;
        else if (level == 13) return 38;
        else if (level == 14) return 41;
        else if (level == 15) return 45;
        else if (level == 16) return 48;
        else if (level == 17) return 52;
        else if (level == 18) return 55;
        else if (level == 19) return 58;
        else if (level == 20) return 62;
        return 0;
    }

    // 본인 레벨 이하의 특성만 선택 가능하도록 버튼 off
    private void UnActiveTraitButton() {
        int maxTraitButton = GetMaxTraitCount();
        for (int i = 1; i <= maxTraitButton; ++i) {
            if (GameManager.instance.player.trait[i]) {
                traitCount++;
                traitButtonss[i].GetComponent<Button>().interactable = false;
            }
        }
        for (int i = maxTraitButton + 1; i <= 62; ++i) {
            traitButtonss[i].GetComponent<Button>().interactable = false;
        }
    }

    private void UnActiveTraitBoard() {
        traitBoard1.SetActive(true);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(false);
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
        startMoneyText.text = GameManager.instance.player.startMoney.ToString();
    }

    public void OnClickDifficultyLeftButton() {
        GameManager.instance.player.difficulty -= 1;
        LoadDifficulty();
        ActivateDifficultyButton();
    }

    public void OnClickDifficultyRightButton() {
        GameManager.instance.player.difficulty += 1;
        LoadDifficulty();
        ActivateDifficultyButton();
    }

    public void ActivateDifficultyButton()
    {
        difficultyLeftButton.interactable = (GameManager.instance.player.difficulty == 0) ? false : true;
        difficultyRightButton.interactable = (GameManager.instance.player.difficulty == 7) ? false : true;
    }

    private void LoadDifficulty()
    {
        difficultyLevelText.text = (GameManager.instance.player.difficulty + 1).ToString();
        recommandLvText.text = "권장 Lv." + DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].recommandLv.ToString();
        rewardExpText.text = "보상 EXP " + DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].rewardExp.ToString();
        goalMoneyText.text = "- 목표금액 " + DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].goalMoney.ToString();
        enemyStatusText.text = "- 적 체력, 이동속도, 공격력 " + (DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].enemyStatus * 100f).ToString() + "%";
        normalEnhanceText.text = "- 일반 적이 " + (DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].normalEnhance * 100f).ToString() + "% 확률로 강화";
        eliteEnhanceText.text = "- 정예 적이 " + (DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].eliteEnhance * 100f).ToString() + "% 확률로 강화";
        dropRankText.text = "- 아이템 드롭률 " + (DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].dropRank * 100f).ToString() + "%";
        enemyRespawnText.text = "- 적 개체수 " + (DataManager.instance.difficultyList.difficulty[GameManager.instance.player.difficulty].enemyRespawn * 100f).ToString() + "%";
    }


    public void OnClickStartButton() {
        traitManager.SaveTrait();
        GameManager.instance.SavePlayerData();
        SceneManager.LoadScene("DayScene");
    }

    public void OnClickCancelButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadAddStatus() {
        addMaxHpText.text = (traitManager.dummyPlayer.maxHp > 0 ? "+" : "") + traitManager.dummyPlayer.maxHp;
        addMoveSpeedText.text = (traitManager.dummyPlayer.moveSpeed > 0 ? "+" : "") + traitManager.dummyPlayer.moveSpeed;
        addAttackPowerText.text = (traitManager.dummyPlayer.attackPower > 0 ? "+" : "") + traitManager.dummyPlayer.attackPower;
        addAttackSpeedText.text = (traitManager.dummyPlayer.attackSpeed > 0 ? "+" : "") + traitManager.dummyPlayer.attackSpeed;
        addAttackRangeText.text = (traitManager.dummyPlayer.attackRange > 0 ? "+" : "") + traitManager.dummyPlayer.attackRange;
        startMoneyText.text = (GameManager.instance.player.startMoney + traitManager.dummyPlayer.startMoney).ToString();
        getMoneyText.text = (GameManager.instance.player.earnMoney + traitManager.dummyPlayer.earnMoney).ToString();
    }



    public void OnClickFirstTraitButton() {
        traitBoard1.SetActive(true);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(false);
        levelSelect1.sprite = levelBackgroundSelect1;
        levelSelect2.sprite = levelBackground2;
        levelSelect3.sprite = levelBackground3;
        levelSelect4.sprite = levelBackground4;
    }

    public void OnClickSecondTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(true);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(false);
        levelSelect1.sprite = levelBackground1;
        levelSelect2.sprite = levelBackgroundSelect2;
        levelSelect3.sprite = levelBackground3;
        levelSelect4.sprite = levelBackground4;
    }

    public void OnClickThirdTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(true);
        traitBoard4.SetActive(false);
        levelSelect1.sprite = levelBackground1;
        levelSelect2.sprite = levelBackground2;
        levelSelect3.sprite = levelBackgroundSelect3;
        levelSelect4.sprite = levelBackground4;
    }

    public void OnClickFourthTraitButton() {
        traitBoard1.SetActive(false);
        traitBoard2.SetActive(false);
        traitBoard3.SetActive(false);
        traitBoard4.SetActive(true);
        levelSelect1.sprite = levelBackground1;
        levelSelect2.sprite = levelBackground2;
        levelSelect3.sprite = levelBackground3;
        levelSelect4.sprite = levelBackgroundSelect4;
    }


    public void OnClickTraitButton() {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        int traitNumber = int.Parse(button.name);
        Trait tempTrait = DataManager.instance.traitList.trait[traitNumber - 1];

        DisableTraitInSameLevel(traitNumber);
        
        // activae trait
        traitManager.activeTrait(tempTrait);
        GameManager.instance.player.trait[traitNumber] = true;
        button.GetComponent<Button>().interactable = false;

        traitName.text = tempTrait.name;
        traitLv.text = tempTrait.requireLv.ToString() + " / " + (tempTrait.rank == 0 ? "일반" : "핵심");
        traitScript.text = tempTrait.script;
        traitImage.sprite = button.GetComponent<Image>().sprite;
        traitImage.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = button.transform.GetChild(1).gameObject.GetComponent<Image>().sprite;

        traitCount++;
        LoadAddStatus();

        CheckStartButton();
    }

    void CheckStartButton() {
        startButton.interactable = (traitCount == GameManager.instance.player.level) ? true : false;
    }


    void DisableTrait(int traitNumber) {
        traitManager.unactiveTrait(DataManager.instance.traitList.trait[traitNumber - 1]);
        traitButtons[traitNumber].interactable = true;
        GameManager.instance.player.trait[traitNumber] = false;
        traitCount--;
    }

    void DisableTraitInSameLevel(int traitNumber) {
        switch (traitNumber) {
            case 1:
            case 2:
                if (GameManager.instance.player.trait[1])
                    DisableTrait(1);
                if (GameManager.instance.player.trait[2])
                    DisableTrait(2);
                break; 
            case 3:
            case 4:
            case 5:
                if (GameManager.instance.player.trait[3])
                    DisableTrait(3);
                if (GameManager.instance.player.trait[4])
                    DisableTrait(4);
                if (GameManager.instance.player.trait[5])
                    DisableTrait(5);
                break;
            case 6:
            case 7:
            case 8:
                if (GameManager.instance.player.trait[6])
                    DisableTrait(6);
                if (GameManager.instance.player.trait[7])
                    DisableTrait(7);
                if (GameManager.instance.player.trait[8])
                    DisableTrait(8);
                break;
            case 9:
            case 10:
                if (GameManager.instance.player.trait[9])
                    DisableTrait(9);
                if (GameManager.instance.player.trait[10])
                    DisableTrait(10);
                break;
            case 11:
            case 12:
            case 13:
                if (GameManager.instance.player.trait[11])
                    DisableTrait(11);
                if (GameManager.instance.player.trait[12])
                    DisableTrait(12);
                if (GameManager.instance.player.trait[13])
                    DisableTrait(13);
                break;
            case 14:
            case 15:
                if (GameManager.instance.player.trait[14])
                    DisableTrait(14);
                if (GameManager.instance.player.trait[15])
                    DisableTrait(15);
                break;
            case 16:
            case 17:
            case 18:
            case 19:
                if (GameManager.instance.player.trait[16])
                    DisableTrait(16);
                if (GameManager.instance.player.trait[17])
                    DisableTrait(17);
                if (GameManager.instance.player.trait[18])
                    DisableTrait(18);
                if (GameManager.instance.player.trait[19])
                    DisableTrait(19);
                break;
            case 20:
            case 21:
            case 22:
                if (GameManager.instance.player.trait[20])
                    DisableTrait(20);
                if (GameManager.instance.player.trait[21])
                    DisableTrait(21);
                if (GameManager.instance.player.trait[22])
                    DisableTrait(22);
                break;
            case 23:
            case 24:
            case 25:
                if (GameManager.instance.player.trait[23])
                    DisableTrait(23);
                if (GameManager.instance.player.trait[24])
                    DisableTrait(24);
                if (GameManager.instance.player.trait[25])
                    DisableTrait(25);
                break;
            case 26:
            case 27:
            case 28:
            case 29:
                if (GameManager.instance.player.trait[26])
                    DisableTrait(26);
                if (GameManager.instance.player.trait[27])
                    DisableTrait(27);
                if (GameManager.instance.player.trait[28])
                    DisableTrait(28);
                if (GameManager.instance.player.trait[29])
                    DisableTrait(29);
                break;
            case 30:
            case 31:
            case 32:
                if (GameManager.instance.player.trait[30])
                    DisableTrait(30);
                if (GameManager.instance.player.trait[31])
                    DisableTrait(31);
                if (GameManager.instance.player.trait[32])
                    DisableTrait(32);
                break;
            case 33:
            case 34:
            case 35:
                if (GameManager.instance.player.trait[33])
                    DisableTrait(33);
                if (GameManager.instance.player.trait[34])
                    DisableTrait(34);
                if (GameManager.instance.player.trait[35])
                    DisableTrait(35);
                break;
            case 36:
            case 37:
            case 38:
                if (GameManager.instance.player.trait[36])
                    DisableTrait(36);
                if (GameManager.instance.player.trait[37])
                    DisableTrait(37);
                if (GameManager.instance.player.trait[38])
                    DisableTrait(38);
                break;
            case 39:
            case 40:
            case 41:
                if (GameManager.instance.player.trait[39])
                    DisableTrait(39);
                if (GameManager.instance.player.trait[40])
                    DisableTrait(40);
                if (GameManager.instance.player.trait[41])
                    DisableTrait(41);
                break;
            case 42:
            case 43:
            case 44:
            case 45:
                if (GameManager.instance.player.trait[42])
                    DisableTrait(42);
                if (GameManager.instance.player.trait[43])
                    DisableTrait(43);
                if (GameManager.instance.player.trait[44])
                    DisableTrait(44);
                if (GameManager.instance.player.trait[45])
                    DisableTrait(45);
                break;
            case 46:
            case 47:
            case 48:
                if (GameManager.instance.player.trait[46])
                    DisableTrait(46);
                if (GameManager.instance.player.trait[47])
                    DisableTrait(47);
                if (GameManager.instance.player.trait[48])
                    DisableTrait(48);
                break;
            case 49:
            case 50:
            case 51:
            case 52:
                if (GameManager.instance.player.trait[49])
                    DisableTrait(49);
                if (GameManager.instance.player.trait[50])
                    DisableTrait(50);
                if (GameManager.instance.player.trait[51])
                    DisableTrait(51);
                if (GameManager.instance.player.trait[52])
                    DisableTrait(52);
                break;
            case 53:
            case 54:
            case 55:
                if (GameManager.instance.player.trait[53])
                    DisableTrait(53);
                if (GameManager.instance.player.trait[54])
                    DisableTrait(54);
                if (GameManager.instance.player.trait[55])
                    DisableTrait(55);
                break;
            case 56:
            case 57:
            case 58:
                if (GameManager.instance.player.trait[56])
                    DisableTrait(56);
                if (GameManager.instance.player.trait[57])
                    DisableTrait(57);
                if (GameManager.instance.player.trait[58])
                    DisableTrait(58);
                break;
            case 59:
            case 60:
            case 61:
            case 62:
                if (GameManager.instance.player.trait[59])
                    DisableTrait(59);
                if (GameManager.instance.player.trait[60])
                    DisableTrait(60);
                if (GameManager.instance.player.trait[61])
                    DisableTrait(61);
                if (GameManager.instance.player.trait[62])
                    DisableTrait(62);
                break;

        }
    }
}