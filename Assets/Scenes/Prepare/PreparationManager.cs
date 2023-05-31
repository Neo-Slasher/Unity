using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = GameManager.instance.player.getLevel().ToString();
        expText.text = "Exp  " + GameManager.instance.player.getCurExp().ToString() + "/" + GameManager.instance.player.getReqExp().ToString();
        maxHpText.text = GameManager.instance.player.getMaxHp().ToString();
        moveSpeedText.text = GameManager.instance.player.getMoveSpeed().ToString();
        attackPowerText.text = GameManager.instance.player.getAttackPower().ToString();
        attackSpeedText.text = GameManager.instance.player.getAttackSpeed().ToString();
        attackRangeText.text = GameManager.instance.player.getAttackRange().ToString();
        moneyText.text = GameManager.instance.player.getMoney().ToString();
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
