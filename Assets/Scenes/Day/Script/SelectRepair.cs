using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectRepair : MonoBehaviour
{
    public Image change_img;

    public TMP_Text Name;
    public TMP_Text Rank;
    public TMP_Text Part;
    public TMP_Text Info;

    public GameObject[] checkMarks;

    public string iname = "";
    public int rank = -1;
    public int part = -1;
    public double attackPower;
    public double attackRange;
    public double attackSpeed;

    string power = "";
    string range = "";
    string speed = "";
    string info = "";

    public int ButtonNum;
    public int mNum;
    
    public void OnDisable()
    {
        change_img.gameObject.SetActive(false);
        Name.text = " ";
        Rank.text = " ";
        Part.text = " ";
        Info.text = " ";
        checkMarks[mNum].SetActive(false);
    }
    public void SelectEquip()
    {
        CheckMarks();
        change_img.gameObject.SetActive(true);
        if (ButtonNum < GameManager.instance.player.equipment.Count)
        {
            change_img.sprite = Resources.Load<Sprite>("Equip/" + GameManager.instance.player.equipment[ButtonNum].name) as Sprite;
            Name.text = GameManager.instance.player.equipment[ButtonNum].name;
            rank = GameManager.instance.player.equipment[ButtonNum].rank;
            part = GameManager.instance.player.equipment[ButtonNum].part;
            attackPower = (int)GameManager.instance.player.equipment[ButtonNum].attackPower;
            attackRange = (int)GameManager.instance.player.equipment[ButtonNum].attackRange;
            attackSpeed = (int)GameManager.instance.player.equipment[ButtonNum].attackSpeed;

            if (attackPower > 0)
                power = "공격력 +" + attackPower.ToString() + "\n";
            else if (attackPower < 0)
                power = "공격력 " + attackPower.ToString() + "\n";

            if (attackRange > 0)
                range = "공격범위 +" + attackRange.ToString() + "\n";
            else if (attackRange < 0)
                range = "공격범위 " + attackRange.ToString() + "\n";

            if (attackSpeed > 0)
                speed = "공격속도 +" + attackSpeed.ToString() + "\n";
            else if (attackSpeed < 0)
                speed = "공격속도 " + attackSpeed.ToString() + "\n";

            if (rank == 3)
                Rank.text = "S등급";
            else if (rank == 2)
                Rank.text = "A등급";
            else if (rank == 1)
                Rank.text = "B등급";
            else if (rank == 0)
                Rank.text = "C등급";

            if (part == 0)
                Part.text = " / 엣지";
            else if (part == 1)
                Part.text = " / 바디";
            else if (part == 2)
                Part.text = " / 핸들";

            Info.text = power + range + speed;
        }
    }
    public void SelectItem()
    {
        mNum = 3;
        CheckMarks();
        change_img.gameObject.SetActive(true);
        if (ButtonNum < GameManager.instance.player.item.Count)
        {
            change_img.sprite = Resources.Load<Sprite>("Item/" + GameManager.instance.player.item[ButtonNum].name) as Sprite;
            Name.text = GameManager.instance.player.item[ButtonNum].name;
            rank = GameManager.instance.player.item[ButtonNum].rank;
            part = GameManager.instance.player.item[ButtonNum].category;
            attackPower = GameManager.instance.player.item[ButtonNum].attackPowerValue;
            attackRange = GameManager.instance.player.item[ButtonNum].attackRangeValue;
            attackSpeed = GameManager.instance.player.item[ButtonNum].attackSpeedValue;

            bool powerC, speedC, rangeC; //아이템 문자열 치환 여부
            double powerP, speedP, rangeP; //플레이어 정보
            string script; //출력할 문자열, 치환할 문자열

            script =GameManager.instance.player.item[ButtonNum].script; //치환할 문자열

            powerP = GameManager.instance.player.attackPower; 
            speedP = GameManager.instance.player.attackSpeed;
            rangeP = GameManager.instance.player.attackRange;

            powerC = GameManager.instance.player.item[ButtonNum].attackPowerCalc;
            speedC = GameManager.instance.player.item[ButtonNum].attackSpeedCalc;
            rangeC = GameManager.instance.player.item[ButtonNum].attackRangeCalc;

            if (rank == 3)
                Rank.text = "S등급";
            else if (rank == 2)
                Rank.text = "A등급";
            else if (rank == 1)
                Rank.text = "B등급";
            else if (rank == 0)
                Rank.text = "C등급";

            if(part == 0)
                Part.text = " / 공격";
            else if(part == 1)
                Part.text = " / 방어";
            else if(part == 2)
                Part.text = " / 보조";

            if(powerC)
            {
                script = script.Replace("#at#", (powerP * attackPower).ToString());
            }
            if(speedC)
            {
                script = script.Replace("#as#", (speedP * attackSpeed).ToString());
            }
            if(rangeC)
            {
                script = script.Replace("#ar#", (rangeP * attackRange).ToString());
            }

            info = script;

            Info.text = "<size=20> " + info + "</size>";
        }
    }
    
    public void CheckMarks()
    {
        mNum += ButtonNum;
        for (int i = 0; i < checkMarks.Length; i++)
        {
            if(i == mNum)
                checkMarks[i].gameObject.SetActive(true);
            else
                checkMarks[i].gameObject.SetActive(false);
        }

    }
    
}
