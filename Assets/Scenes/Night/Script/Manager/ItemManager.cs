using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName
{
    None
        //공격
        , CentryBall = 1, ChargingReaper, DisasterDrone, MultiSlash, RailPiercer
        //방어
        , FirstAde = 6, Barrior, HologramTrick, AntiPhenet, RegenerationArmor
        //보조
        , GravityBind = 11, MoveBack, Booster, BioSnach, InterceptDrone
}

public class ItemManager : MonoBehaviour
{
    //아이템 사용을 여기서 할거임
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    GameObject characterParent;
    [SerializeField]
    Character character;

    [SerializeField]
    int[] itemIdxArr;
    [SerializeField]
    int tempItemIdx;

    [SerializeField]
    GameObject[] itemPrefabArr;

    //item 변수
    [SerializeField]
    float centryBallAngle;
    [SerializeField]
    float chargingReaperAngle;
    [SerializeField] 
    float reaperCircleR = 5; //반지름
    float reaperDeg = 0; //각도
    [SerializeField]
    float reaperSpeed = 100; //원운동 속도
    ChargingReaper chargingReaperScript;
    public bool isChargingReaperUse = false;

    private void Awake()
    {
        //게임 매니저나 어딘가에서 아이템 인덱스 같은 거 들고오기
        itemIdxArr = new int[character.ReturnCharacterItemSlot()];
    }

    private void Start()
    {
        //StartItem();
        SetTempItem1();
    }

    //아이템 체크하려고 만든 임시 코드
    void SetTempItem1()
    {
        FindItem(tempItemIdx);
    }

    void StartItem()
    {
        for(int i =0; i< itemIdxArr.Length; i++)
        {
            FindItem(itemIdxArr[i]);
        }
    }

    void FindItem(int getIdx)
    {
        ItemName nowItemName = (ItemName)getIdx;
        Debug.Log(nowItemName.ToString());
        switch(nowItemName)
        {
            case ItemName.None:
                break;
            case ItemName.CentryBall:
                StartCoroutine(CentryBallCoroutine());
                break;
            case ItemName.ChargingReaper:
                StartCoroutine(ChargingReaperCoroutine());
                break;
            case ItemName.DisasterDrone:
                DisasterDrone();
                break;
            case ItemName.MultiSlash:
                break;
            case ItemName.RailPiercer:
                break;
            case ItemName.FirstAde:
                break;
            case ItemName.Barrior:
                break;
            case ItemName.HologramTrick:
                break;
            case ItemName.AntiPhenet:
                break;
            case ItemName.RegenerationArmor:
                break;
            case ItemName.GravityBind:
                break;
            case ItemName.MoveBack:
                break;
            case ItemName.Booster:
                break;
            case ItemName.BioSnach:
                break;
            case ItemName.InterceptDrone:
                break;
        }
    }

    IEnumerator CentryBallCoroutine()
    {
        GameObject centryBallParent = Instantiate(itemPrefabArr[1]);
        GameObject centryBall = centryBallParent.transform.GetChild(0).gameObject;
        centryBallParent.transform.SetParent(character.transform);

        Vector3 centryBallPos = character.transform.position;
        centryBallPos.y += 5;

        centryBall.transform.localPosition = centryBallPos;
        while (!nightManager.isStageEnd)
        {
            centryBall.transform.RotateAround(character.transform.position, Vector3.back, centryBallAngle);
            yield return null;
        }
    }

    IEnumerator ChargingReaperCoroutine()
    {
        isChargingReaperUse = true;
        GameObject chargingReaperParent = Instantiate(itemPrefabArr[2]);
        chargingReaperParent.transform.SetParent(characterParent.transform);
        chargingReaperScript = chargingReaperParent.GetComponent<ChargingReaper>();

        Transform reaperImageTransform = chargingReaperParent.transform.GetChild(0);

        while (!nightManager.isStageEnd)
        {
            if (chargingReaperScript.IsChargingGaugeFull())
            {
                reaperDeg += Time.deltaTime * reaperSpeed;
                if (reaperDeg < 360)
                {
                    var rad = Mathf.Deg2Rad * (reaperDeg);
                    var x = reaperCircleR * Mathf.Sin(rad);
                    var y = reaperCircleR * Mathf.Cos(rad);
                    reaperImageTransform.transform.position = character.transform.position + new Vector3(x, y);
                    reaperImageTransform.transform.rotation = Quaternion.Euler(0, 0, reaperDeg * -1); //가운데를 바라보게 각도 조절
                }
                else
                {
                    reaperDeg = 0;
                    chargingReaperScript.ReaperUse();
                    chargingReaperScript.chargingGauge -= 100;
                }
                yield return null;
            }
            else
                yield return new WaitUntil(() => chargingReaperScript.IsChargingGaugeFull());
        }
    }

    public void ChargingReaperGauge()
    {
        if (isChargingReaperUse)
            chargingReaperScript.ChargingGauge();
    }

    void DisasterDrone()
    {
        GameObject disasterDroneParent = Instantiate(itemPrefabArr[3]);
        disasterDroneParent.transform.SetParent(character.transform);
        disasterDroneParent.transform.localPosition = character.transform.position;
        disasterDroneParent.GetComponent<DisasterDrone>().character = character;
        disasterDroneParent.GetComponent<DisasterDrone>().nightManager = nightManager;
    }
}
