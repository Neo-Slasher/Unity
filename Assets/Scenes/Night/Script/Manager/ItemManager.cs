using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    GameObject hitBox;

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
    float reaperCircleR = 3; //반지름
    float reaperDeg = 0; //각도
    [SerializeField]
    float reaperSpeed = 600; //원운동 속도
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
                StartCoroutine(MultiSlashCoroutine());
                break;
            case ItemName.RailPiercer:
                RailPiercer();
                break;
            case ItemName.FirstAde:
                StartCoroutine(FirstAdeCoroutine());
                break;
            case ItemName.Barrior:
                StartCoroutine(BarriorCoroutine());
                break;
            case ItemName.HologramTrick:
                StartCoroutine(HologramTrickCoroutine());
                break;
            case ItemName.AntiPhenet:
                AntiPhenet();
                break;
            case ItemName.RegenerationArmor:
                RegenerationArmor();
                break;
            case ItemName.GravityBind:
                GravityBind();
                break;
            case ItemName.MoveBack:
                StartCoroutine(MoveBack());
                break;
            case ItemName.Booster:
                StartCoroutine(BoosterCoroutine());
                break;
            case ItemName.BioSnach:
                BioSnach();
                break;
            case ItemName.InterceptDrone:
                InterceptDroneCoroutine();
                break;
        }
    }

    IEnumerator CentryBallCoroutine()
    {
        GameObject centryBallParent = Instantiate(itemPrefabArr[1]);
        GameObject centryBall = centryBallParent.transform.GetChild(0).gameObject;
        CentryBall centryBallScript = centryBallParent.GetComponent<CentryBall>();
        centryBallParent.transform.SetParent(character.transform);

        Vector3 centryBallPos = character.transform.position;
        centryBallPos.y += 7;

        centryBall.transform.localPosition = centryBallPos;
        while (!nightManager.isStageEnd)
        {
            centryBall.transform.RotateAround(character.transform.position, Vector3.back, centryBallAngle);
            if (!centryBallScript.StopCentryBall())
            {
                centryBall.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
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

    IEnumerator MultiSlashCoroutine()
    {
        //2회 연속공격 + 에너지파
        while (!nightManager.isStageEnd)
        {
            character.isDoubleAttack = true;

            while (character.isDoubleAttack)
            {
                yield return null;
            }
            Debug.Log("end");
            ShootSwordAura();
            yield return new WaitForSeconds(6);
        }
    }

    void ShootSwordAura()
    {
        GameObject swordAura = Instantiate(itemPrefabArr[4]);
        swordAura.transform.SetParent(characterParent.transform);
        swordAura.transform.position = hitBox.transform.position;

        Color swordAuraColor = Color.blue;
        swordAuraColor.a = 0.5f;

        swordAura.GetComponent<SpriteRenderer>().color = swordAuraColor;
        SetSwordAuraAngle(swordAura);

        if(character.fixPos != Vector3.zero)
            swordAura.GetComponent<Rigidbody2D>().velocity = character.fixPos.normalized * 10;
        else
            swordAura.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10;
    }

    void SetSwordAuraAngle(GameObject getSwordAura)
    {
        if (character.fixPos != Vector3.zero)
        {
            float dot = Vector3.Dot(character.fixPos, new Vector3(1, 0, 0));
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (character.fixPos.y >= 0)
                getSwordAura.transform.rotation = Quaternion.Euler(0, 0, angle);
            else
                getSwordAura.transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
        }
        else
        {
            getSwordAura.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void RailPiercer()
    {
        GameObject railPiercerParent = Instantiate(itemPrefabArr[5]);
        railPiercerParent.transform.SetParent(characterParent.transform);
        RailPiercer railPiercerScript = railPiercerParent.GetComponent<RailPiercer>();

        railPiercerScript.character = character;
        railPiercerScript.nightManager = nightManager;

        double characterAttackSpeed = character.ReturnCharacterAttackSpeed();
        double characterAttackPower = character.ReturnCharacterAttackPower();

        railPiercerScript.SetRailPiercerPos();
        railPiercerScript.ShootRailPiercer(characterAttackSpeed, characterAttackPower);
    }

    IEnumerator FirstAdeCoroutine()
    {
        GameObject firstAdeParent = Instantiate(itemPrefabArr[6]);
        firstAdeParent.transform.SetParent(character.transform);
        firstAdeParent.transform.localPosition = character.transform.position;
        firstAdeParent.SetActive(false);

        double nowHp = character.ReturnCharacterHitPoint();
        double firstAdeHp = character.ReturnCharacterHitPointMax() * 0.4f;
        double healHp = character.ReturnCharacterAttackPower();

        while(!nightManager.isStageEnd)
        {
            while (nowHp >= firstAdeHp)
            {
                nowHp = character.ReturnCharacterHitPoint();
                yield return null;
            }

            character.HealHp(healHp, firstAdeParent);

            yield return new WaitForSeconds(20);
        }
    }

    IEnumerator BarriorCoroutine()
    {
        GameObject barriorParent = Instantiate(itemPrefabArr[7]);
        barriorParent.transform.SetParent(character.transform);
        barriorParent.transform.position = character.transform.position;
        Barrior barriorScript = barriorParent.GetComponent<Barrior>();

        double characterAttackSpeed = character.ReturnCharacterAttackSpeed();
        double characterAttackPower = character.ReturnCharacterAttackPower();
        float shieldPoint = (float)characterAttackPower;

        double timeCount = 50 / characterAttackSpeed;
        while (!nightManager.isStageEnd)
        {
            character.SetShieldPointData(shieldPoint);
            barriorScript.CreateBarrior();
            
            yield return new WaitUntil(() => character.ReturnCharacterShieldPoint() == 0);
            barriorScript.SetBarriorActive(false);

            yield return new WaitForSeconds((float)timeCount);
        }
    }

    IEnumerator HologramTrickCoroutine()
    {
        GameObject[] hologramParentArr = new GameObject[2];
        Vector3 hologramVector;

        for (int i = 0; i < 2; i++)
        {
            hologramVector = character.transform.position;
            GameObject hologramParent = Instantiate(itemPrefabArr[8]);
            hologramParent.transform.SetParent(character.transform);
            hologramParent.transform.position = character.transform.position;
            hologramParentArr[i] = hologramParent;

            if (i == 0)
            {
                hologramVector.x -= 1;
            }
            else
            {
                hologramVector.x += 1;
            }

            hologramParentArr[i].transform.position = hologramVector;
        }

        double characterAttackSpeed = character.ReturnCharacterAttackSpeed();
        double characterAttackRange = character.ReturnCharacterAttackRange();

        double duration = characterAttackRange;
        double timeCount = 1200 / characterAttackSpeed;

        while(!nightManager.isStageEnd)
        {
            character.isHologramTrickOn = true;
            yield return new WaitForSeconds((float)duration);
            character.isHologramTrickOn = false;
            
            yield return new WaitForSeconds((float)timeCount);
        }
    }

    void AntiPhenet()
    {
        character.isAntiPhenetOn = true;
    }

    void RegenerationArmor()
    {
        double addHp = character.ReturnCharacterAttackPower();
        double addHpRegen = character.ReturnCharacterAttackRange();

        //임시 코드
        addHpRegen = 100;

        character.SetCharacterHitPointMax(addHp);
        character.SetCharacterHpRegen(addHpRegen);
    }

    void GravityBind()
    {
        GameObject gravityBindParent = Instantiate(itemPrefabArr[11]);
        gravityBindParent.transform.SetParent(characterParent.transform);
        gravityBindParent.transform.localPosition = character.transform.position;
        gravityBindParent.transform.GetChild(0).GetComponent<GravityBind>().character = character;
        gravityBindParent.transform.GetChild(0).GetComponent<GravityBind>().nightManager = nightManager;
    }

    IEnumerator MoveBack()
    {
        double getAttackSpeed = character.ReturnCharacterAttackSpeed();
        float timeCount = (float)(200 / getAttackSpeed);

        while(!nightManager.isStageEnd)
        {
            character.isMoveBackOn = true;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator BoosterCoroutine()
    {
        double getBasicSpeed = character.ReturnCharacterMoveSpeed();
        double getAttackSpeed = character.ReturnCharacterAttackSpeed();
        double getAttackRange = character.ReturnCharacterAttackRange();
        double getAttackPower = character.ReturnCharacterAttackPower();

        float timeCount = (float)(300 / getAttackSpeed); Debug.Log(timeCount);
        float duration = (float)getAttackRange;
        double speed = getAttackPower; 

        while(!nightManager.isStageEnd)
        {
            character.SetMoveSpeed(speed);
            yield return new WaitForSeconds(duration);
            character.SetMoveSpeed(getBasicSpeed);

            yield return new WaitForSeconds(timeCount - duration);
        }
    }

    void BioSnach()
    {
        float getAbsorbAttackData = 100;
        character.SetAbsorbAttackData(getAbsorbAttackData);
    }
    
    void InterceptDroneCoroutine()
    {
        GameObject interceptDroneParent = Instantiate(itemPrefabArr[15]);
        interceptDroneParent.transform.SetParent(character.transform);
        interceptDroneParent.transform.localPosition = character.transform.position;
        interceptDroneParent.GetComponent<InterceptDrone>().character = character;
        interceptDroneParent.GetComponent<InterceptDrone>().nightManager = nightManager;

        double getAttackSpeed = character.ReturnCharacterAttackSpeed();
        double getAttackRange = character.ReturnCharacterAttackRange();

        interceptDroneParent.GetComponent<InterceptDrone>().SetInterceptDrone(getAttackRange, getAttackSpeed);
    }
}
