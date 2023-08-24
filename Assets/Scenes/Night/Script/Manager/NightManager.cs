using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NightManager : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager;
    [SerializeField]
    TraitManager traitManager;
    [SerializeField]
    TimerManager timerManager;
    [SerializeField]
    NightSFXManager nightSFXManager;
    [SerializeField]
    GameObject character;
    KillData killdata;  //죽인 몬스터 마리수 카운트

    //UI
    [SerializeField]
    GameObject endPopup;

    //몬스터 출현 등에 사용할 예정
    [SerializeField]
    GameObject[] normalEnemyPrefabArr;
    [SerializeField]
    GameObject[] eliteEnemyPrefabArr;

    NormalEnemy[] normalEnemyArr;
    EliteEnemy[] eliteEnemyArr;
    int normalEnemyCount = 3;
    int eliteEnemyCount = 3;

    Vector3 nowCharPos;
    [SerializeField]
    Transform enemyCloneParent;

    //전투시 필요한 데이터
    public bool isStageEnd = false; //밤이 끝났는지 알아보는 변수

    [SerializeField]
    TextMeshProUGUI[] killCountTextArr; //0: 노멀 킬수, 1: 엘리트 킬수, 2: 노멀 알파, 3: 엘리트 알파
    [SerializeField]
    TextMeshProUGUI aliveOrDieText;
    public int killCount = 0;
    public int killNormal = 0;
    public int killElite = 0;

    //환경설정 추가
    [SerializeField]
    Button settingBtn;
    [SerializeField]
    GameObject settingParent;

    //게임 종료 팝업
    [SerializeField]
    Sprite[] itemSpriteArr;
    [SerializeField]
    Image getItemImage;
    [SerializeField]
    TextMeshProUGUI getItemName;

    private void Start()
    {
        GameManager.instance.player.curHp = GameManager.instance.player.maxHp;

        //적 배열 생성
        normalEnemyArr = new NormalEnemy[normalEnemyCount];
        eliteEnemyArr = new EliteEnemy[eliteEnemyCount];

        SetEnemyArrData();

        //몬스터 생성 함수
        InstantiateEnemy();
        //TestEnemy();
    }

    void SetEnemyArrData()
    {
        for (int i = 0; i < normalEnemyCount; i++)
        {
            normalEnemyArr[i] = normalEnemyPrefabArr[i].GetComponent<NormalEnemy>();
            normalEnemyArr[i].SetCharacter(character);

            normalEnemyArr[i].SetNormalEnemyType(i);    //적들의 기본데이터 불러오기
            normalEnemyArr[i].SetEnemyStatus(GameManager.instance.player.level);
        }

        for (int i = 0; i < eliteEnemyCount; i++)
        {
            eliteEnemyArr[i] = eliteEnemyPrefabArr[i].GetComponent<EliteEnemy>();
            eliteEnemyArr[i].SetCharacter(character);

            eliteEnemyArr[i].SetEliteEnemyType(i);  //적들의 기본데이터 불러오기
            eliteEnemyArr[i].SetEnemyStatus(GameManager.instance.player.level);
        }
    }

    //각 몬스터마다 소환 함수를 따로 두고 스폰율에 따라 차등 생성
    void InstantiateEnemy()
    {
        //노멀 적과 엘리트 적 소환하는 함수
        for (int normalEnemyDataIndex = 0; normalEnemyDataIndex < normalEnemyCount; normalEnemyDataIndex++)
            StartCoroutine(InstantiateNormalEnemyCoroutine(normalEnemyDataIndex));
        for (int eliteEnemyDataIndex = 0; eliteEnemyDataIndex < eliteEnemyCount; eliteEnemyDataIndex++)
            StartCoroutine(InstantiateEliteEnemyCoroutine(eliteEnemyDataIndex));
    }

    void TestEnemy()
    {
        eliteEnemyArr[1].SetEnforceData(GameManager.instance.player.level, true);
        GameObject eliteEnemyClone = Instantiate(normalEnemyPrefabArr[0], SetEnemyPos(), Quaternion.identity);
        eliteEnemyClone.transform.SetParent(enemyCloneParent);
        eliteEnemyClone.SetActive(true);
    }

    IEnumerator InstantiateNormalEnemyCoroutine(int nowEnemyIndex)
    {
        //스폰률이 0이면 스폰 x
        if (GetSpawn(false, nowEnemyIndex) != 0)
        {
            yield return new WaitForSeconds((float)SpawnTIme(false, nowEnemyIndex));
            while (!isStageEnd && timerManager.timerCount > 1)
            {
                //몬스터 스폰
                GameObject normalEnemyClone = Instantiate(normalEnemyPrefabArr[nowEnemyIndex], SetEnemyPos(), Quaternion.identity);
                normalEnemyClone.GetComponent<EnemyParent>().SetEnforceData(GameManager.instance.player.level, false);


                normalEnemyClone.transform.SetParent(enemyCloneParent);
                normalEnemyClone.SetActive(true);
                yield return new WaitForSeconds((float)SpawnTIme(false, nowEnemyIndex));
            }
        }
    }

    IEnumerator InstantiateEliteEnemyCoroutine(int nowEnemyIndex)
    {
        if (GetSpawn(true, nowEnemyIndex) != 0)
        {
            yield return new WaitForSeconds((float)SpawnTIme(true, nowEnemyIndex));
            while (!isStageEnd && timerManager.timerCount > 1)
            {
                //몬스터 스폰
                GameObject eliteEnemyClone = Instantiate(eliteEnemyPrefabArr[nowEnemyIndex], SetEnemyPos(), Quaternion.identity);
                eliteEnemyClone.GetComponent<EnemyParent>().SetEnforceData(GameManager.instance.player.level, false);

                eliteEnemyClone.transform.SetParent(enemyCloneParent);
                eliteEnemyClone.SetActive(true);
                yield return new WaitForSeconds((float)SpawnTIme(true, nowEnemyIndex));
            }
        }
    }

    double SpawnTIme(bool isElite, int nowEnemyIndex)
    {
        double nowSpawn = 0;
        double totalSpawn = 0;

        nowSpawn = GetSpawn(isElite, nowEnemyIndex);

        //누적 오브젝트 수를 구하고 60초 안에 모든 오브젝트가 나오도록 구현
        totalSpawn = nowSpawn * 60;
        return (double)(60f / (int)totalSpawn);
    }

    double GetSpawn(bool isElite, int getIndex)
    {
        int nowAssassination = GameManager.instance.player.assassinationCount;
        switch (getIndex)
        {
            case 0:
                if (isElite)
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].elite1Spawn;
                else
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].normal1Spawn;
            case 1:
                if (isElite)
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].elite2Spawn;
                else
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].normal2Spawn;
            case 2:
                if (isElite)
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].elite3Spawn;
                else
                    return DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].normal3Spawn;
            default:
                return -1;
        }
    }

    //적이 스폰되는 벡터 구하기
    Vector3 SetEnemyPos()
    {
        nowCharPos = character.transform.position;

        float xPos = 0;
        float yPos = 0;
        Vector3 instantiatePos = new Vector3(xPos, yPos, 0);
        do
        {
            xPos = Random.Range(-15f, 15f);
            yPos = Random.Range(-30f, 30f);
            instantiatePos.x = xPos;
            instantiatePos.y = yPos;
        } while (!IsPosInGround(instantiatePos));

        instantiatePos += character.transform.position;

        return instantiatePos;
    }

    bool IsPosInGround(Vector3 getVector)
    {
        if (getVector.x < -10 || getVector.x > 10)
            return true;
        else if (getVector.y < -20 || getVector.y > 20)
            return true;
        else
            return false;
    }

    public void SetStageEnd()
    {
        isStageEnd = true;

        //플레이어 공격 정지
        character.GetComponent<Character>().StopAttack();
        character.GetComponent<Character>().EndMove();

        //조이스틱 사용 정지와는 nightManager.isStageEnd를 매번 받기 때문에 알아서 꺼짐

        //적 오브젝트 정지
        for (int i = 0; i < enemyCloneParent.childCount; i++)
        {
            //일반 적일때 
            if (enemyCloneParent.GetChild(i).tag == "Normal")
            {
                enemyCloneParent.GetChild(i).GetComponent<NormalEnemy>().isStageEnd = true;
                //해당 콜라이더 정지는 이후 오브젝트 형태에 따라 변경될 예정@@@@@@@@@@
                enemyCloneParent.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }

            //엘리트 적일때
            else if (enemyCloneParent.GetChild(i).tag == "Elite")
            {
                enemyCloneParent.GetChild(i).GetComponent<EliteEnemy>().isStageEnd = true;
                enemyCloneParent.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        SetEndPopUp();
        endPopup.SetActive(true);
    }

    //팝업창 데이터 설정
    void SetEndPopUp()
    {
        if (timerManager.timerCount == 0)
            aliveOrDieText.text = "생존";
        else
            aliveOrDieText.text = "사망";

        killCountTextArr[0].text = killNormal.ToString();
        killCountTextArr[1].text = killElite.ToString();

        GetItem();
        GetMoney();
        GameManager.instance.SavePlayerData();
    }

    //끝난 날짜 및 데이터 조정 및 저장
    public void OnTouchEndBtn()
    {
        //7일차가 아니면 저장하고 낮씬으로 가고 아니면 엔딩으로 갑니다.
        if (GameManager.instance.player.day < 7)
        {
            GameManager.instance.player.day++;
            //뭐 대충 재화 정리까지 다 하고 세이브
            GameManager.instance.player.curHp = GameManager.instance.player.maxHp;
            GameManager.instance.SavePlayerData();
            UnityEngine.SceneManagement.SceneManager.LoadScene("DayScene");
        }
        else
        {
            //재화 정리하고 엔딩으로
            int nowDifficulty = GameManager.instance.player.difficulty;
            if (nowDifficulty != 7)
            {
                Player player = GameManager.instance.player;
                GameManager.instance.player.curExp += DataManager.instance.difficultyList.difficulty[nowDifficulty].rewardExp;
                if(player.reqExp< player.curExp)
                {
                    GameManager.instance.player.curExp -= GameManager.instance.player.reqExp;
                    GameManager.instance.player.level++;
                }
                
            }
            GameManager.instance.SavePlayerData();
            UnityEngine.SceneManagement.SceneManager.LoadScene("CutScene");
        }
    }

    public void UpdateKillCount()
    {
        killCount++;
        itemManager.ChargingReaperGauge();  //차징 리퍼 쓰면 동작
    }

    public void UpdateKillNormalCount()
    {
        killNormal++;
    }

    public void UpdateKillEliteCount()
    {
        killElite++;
    }

    //밤이 끝나고 아이템 획득하는 함수
    void GetItem()
    {
        //(처치한 일반 몬스터 수 × 해당 단계에서의 일반 드랍 확률) +
        //(처치한 정예 몬스터 수 × 해당 단계에서의 정예 드랍 확률) = (장비 및 아이템을 획득할 확률)
        int nowDifficulty = GameManager.instance.player.difficulty;
        int nowAssassination = GameManager.instance.player.assassinationCount;
        int nowRank = GameManager.instance.player.dropRank + DataManager.instance.difficultyList.difficulty[nowDifficulty].dropRank
                        + DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].stageDropRank;
        int getItemRank = SetItemRank(nowRank);

        double normalRate = DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].normalDropRate * killNormal;
        double eliteRate = DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].eliteDropRate * killElite;
        
        float rate = (float)(normalRate + eliteRate);
        
        float nowProb = (float)Random.Range(0, 100) / 100;
        Debug.Log(rate + " / "+ nowProb);
        if (nowProb < rate)
        {
            //아이템 획득
            int getItemIdx = Random.Range(0 + getItemRank * 15, 14 + getItemRank * 15);
            Item getItem = DataManager.instance.itemList.item[getItemIdx];

            getItemImage.sprite = itemSpriteArr[getItem.imgIdx];
            getItemName.text = getItem.name;

            SetPlayerItem(getItem);
        }
        else
        {
            //획득 x
            getItemImage.gameObject.SetActive(false);
            getItemName.text = "없음";
        }
    }

    int SetItemRank(int nowRank)
    {
        if (nowRank < 8)
            return 0;
        else if (nowRank < 17)
            return 1;
        else if (nowRank < 31)
            return 2;
        else
            return 3;
    }

    void SetPlayerItem(Item getItem)
    {
        if(GameManager.instance.player.item.Count < GameManager.instance.player.itemSlot)
        {
            GameManager.instance.player.item.Add(getItem);
        }
    }

    void GetMoney()
    {
        //(처치한 일반 적 수 * 일반 적 현상금 + 정예 적 수 * 적 현상금) * 획득 재화값
        int nowDifficulty = GameManager.instance.player.difficulty;
        int nowAssassination = GameManager.instance.player.assassinationCount;

        int normalMoney = DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].normalReward * killNormal;
        int eliteMoney = DataManager.instance.assassinationStageList.assassinationStage[nowAssassination].eliteReward * killElite;

        int resultMoney = (int)((normalMoney + eliteMoney) * (1+ GameManager.instance.player.earnMoney));
        Debug.Log(resultMoney);
        GameManager.instance.player.money += resultMoney;

        killCountTextArr[2].text = normalMoney + "a";
        killCountTextArr[3].text = eliteMoney + "a";
    }

    //환경설정 여는 함수
    public void OnClickSettingBtn()
    {
        settingParent.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickContinueBtn()
    {
        settingParent.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickExitBtn()
    {
        //씬 이동  
        settingParent.SetActive(false);
        SetEndPopUp();
        endPopup.SetActive(true);
    }
}
