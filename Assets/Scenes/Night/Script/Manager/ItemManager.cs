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
    int[] itemRankArr;
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
    Sprite[] multiSlasherSprite;
    [SerializeField]
    float reaperSpeed = 600; //원운동 속도
    ChargingReaper chargingReaperScript;
    public bool isChargingReaperUse = false;

    private void Awake()
    {
        //게임 매니저나 어딘가에서 아이템 인덱스 같은 거 들고오기
        //itemIdxArr = new int[character.ReturnCharacterItemSlot()];
    }

    private void Start()
    {
        StartItem();
        //SetTempItem1();
    }

    //아이템 체크하려고 만든 임시 코드
    //void SetTempItem1()
    //{
    //    FindItem(tempItemIdx);
    //}

    void SetItemIdxArr()
    {
        for (int i = 0; i < GameManager.instance.player.item.Length; i++)
        {
            itemIdxArr[i] = GameManager.instance.player.item[i].itemIdx;
            itemRankArr[i] = GameManager.instance.player.item[i].rank;
            itemIdxArr[i] -= (itemRankArr[i] * 15);     //아이템 인덱스로 ItemName을 구분하기 때문에 강제로 만든 식입니다.
                                                        //아이템 인덱스 - 랭크*15 = itemName
        }
    }

    void StartItem()
    {
        for(int i =0; i< itemIdxArr.Length; i++)
        {
            FindItem(itemIdxArr[i], itemRankArr[i]);
        }
    }

    void FindItem(int getIdx, int getRank)
    {
        ItemName nowItemName = (ItemName)getIdx;
        Debug.Log(nowItemName.ToString());
        switch(nowItemName)
        {
            case ItemName.None:
                break;
            case ItemName.CentryBall:
                StartCoroutine(CentryBallCoroutine(getRank));
                break;
            case ItemName.ChargingReaper:
                StartCoroutine(ChargingReaperCoroutine(getRank));
                break;
            case ItemName.DisasterDrone:
                DisasterDrone(getRank);
                break;
            case ItemName.MultiSlash:
                StartCoroutine(MultiSlashCoroutine(getRank));
                break;
            case ItemName.RailPiercer:
                RailPiercer(getRank);
                break;
            case ItemName.FirstAde:
                StartCoroutine(FirstAdeCoroutine(getRank));
                break;
            case ItemName.Barrior:
                StartCoroutine(BarriorCoroutine(getRank));
                break;
            case ItemName.HologramTrick:
                StartCoroutine(HologramTrickCoroutine(getRank));
                break;
            case ItemName.AntiPhenet:
                AntiPhenet(getRank);
                break;
            case ItemName.RegenerationArmor:
                RegenerationArmor(getRank);
                break;
            case ItemName.GravityBind:
                GravityBind(getRank);
                break;
            case ItemName.MoveBack:
                StartCoroutine(MoveBack(getRank));
                break;
            case ItemName.Booster:
                StartCoroutine(BoosterCoroutine(getRank));
                break;
            case ItemName.BioSnach:
                BioSnach(getRank);
                break;
            case ItemName.InterceptDrone:
                InterceptDroneCoroutine(getRank);
                break;
        }
    }

    IEnumerator CentryBallCoroutine(int getitemRank)
    {
        GameObject centryBallParent = Instantiate(itemPrefabArr[1]);
        GameObject centryBall = centryBallParent.transform.GetChild(0).gameObject;
        CentryBall centryBallScript = centryBallParent.GetComponent<CentryBall>();
        centryBallParent.transform.SetParent(character.transform);
        centryBallScript.SetItemRank(getitemRank);

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

    IEnumerator ChargingReaperCoroutine(int getRank)
    {
        isChargingReaperUse = true;
        GameObject chargingReaperParent = Instantiate(itemPrefabArr[2]);
        chargingReaperParent.transform.SetParent(characterParent.transform);
        chargingReaperScript = chargingReaperParent.GetComponent<ChargingReaper>();
        chargingReaperScript.SetItemRank(getRank);

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

    void DisasterDrone(int getRank)
    {
        GameObject disasterDroneParent = Instantiate(itemPrefabArr[3]);
        disasterDroneParent.transform.SetParent(character.transform);
        disasterDroneParent.transform.localPosition = character.transform.position;
        disasterDroneParent.GetComponent<DisasterDrone>().character = character;
        disasterDroneParent.GetComponent<DisasterDrone>().nightManager = nightManager;
        disasterDroneParent.GetComponent<DisasterDrone>().SetItemRank(getRank);
    }

    IEnumerator MultiSlashCoroutine(int getRank)
    {
        //랭크에 따라 다른 작업 추가
        float slashTime = 6;
        float attackPowerRate = (float)DataManager.instance.itemList.item[3].attackPowerValue;
        float slashAttackPower = (float)character.ReturnCharacterAttackPower() * attackPowerRate;

        switch (getRank)
        {
            case 0:
                slashTime = 6;
                break;
            case 1:
                slashTime = 5;
                break;
            case 2:
                slashTime = 4;
                break;
            case 3:
                slashTime = 3;
                break;
        }

        //2회 연속공격 + 에너지파
        while (!nightManager.isStageEnd)
        {
            character.isDoubleAttack = true;

            while (character.isDoubleAttack)
            {
                yield return null;
            }
            Debug.Log("end");
            ShootSwordAura(slashAttackPower);

            yield return new WaitForSeconds(slashTime);
        }
    }

    public void SetMultiSlasherSprite(bool isMultiActive)
    {
        SpriteRenderer hitBoxSpriteRenderer = hitBox.GetComponent<SpriteRenderer>();

        if (isMultiActive)
            hitBoxSpriteRenderer.sprite = multiSlasherSprite[0];   //멀티 슬레쉬 스프라이트
        else
            hitBoxSpriteRenderer.sprite = multiSlasherSprite[1];   //멀티 슬레쉬 스프라이트
    }

    void ShootSwordAura(float getSlashAttackPower)
    {
        GameObject swordAura = Instantiate(itemPrefabArr[4]);
        swordAura.transform.SetParent(characterParent.transform);
        swordAura.transform.position = hitBox.transform.position;
        swordAura.GetComponent<SwordAura>().attackDamage = getSlashAttackPower;

        //Color swordAuraColor = Color.blue;
        //swordAuraColor.a = 0.5f;

        //swordAura.GetComponent<SpriteRenderer>().color = swordAuraColor;
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

    void RailPiercer(int getRank)
    {
        GameObject railPiercerParent = Instantiate(itemPrefabArr[5]);
        railPiercerParent.transform.SetParent(characterParent.transform);
        RailPiercer railPiercerScript = railPiercerParent.GetComponent<RailPiercer>();

        railPiercerScript.character = character;
        railPiercerScript.nightManager = nightManager;
        railPiercerScript.SetItemRank(getRank);

        railPiercerScript.SetRailPiercerPos();
        railPiercerScript.ShootRailPiercer();
    }

    IEnumerator FirstAdeCoroutine(int getRank)
    {
        GameObject firstAdeParent = Instantiate(itemPrefabArr[6]);
        firstAdeParent.transform.SetParent(character.transform);
        firstAdeParent.transform.localPosition = character.transform.position;
        firstAdeParent.SetActive(false);

        double nowHp = character.ReturnCharacterHitPoint();
        double firstAdeHp = character.ReturnCharacterHitPointMax() * 0.4f;
        float healHp = (float)character.ReturnCharacterAttackPower();
        int coolTime = 30;


        switch (getRank)
        {
            case 0:
                healHp *= (float)DataManager.instance.itemList.item[5].attackPowerValue;
                coolTime = 30;
                break;
            case 1:
                healHp *= (float)DataManager.instance.itemList.item[20].attackPowerValue;
                coolTime = 25;
                break;
            case 2:
                healHp *= (float)DataManager.instance.itemList.item[35].attackPowerValue;
                coolTime = 20;
                break;
            case 3:
                healHp *= (float)DataManager.instance.itemList.item[50].attackPowerValue;
                coolTime = 15;
                break;
        }

        while (!nightManager.isStageEnd)
        {
            while (nowHp >= firstAdeHp)
            {
                nowHp = character.ReturnCharacterHitPoint();
                yield return null;
            }

            character.HealHp(healHp, firstAdeParent);

            yield return new WaitForSeconds(coolTime);
        }
    }

    IEnumerator BarriorCoroutine(int getRank)
    {
        GameObject barriorParent = Instantiate(itemPrefabArr[7]);
        barriorParent.transform.SetParent(character.transform);
        barriorParent.transform.position = character.transform.position;
        Barrior barriorScript = barriorParent.GetComponent<Barrior>();

        float characterAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float characterAttackPower = (float)character.ReturnCharacterAttackPower();
        float shieldPoint = characterAttackPower;

        double timeCount = 50 / characterAttackSpeed;

        switch(getRank)
        {
            case 0:
                shieldPoint *= (float)DataManager.instance.itemList.item[6].attackPowerValue;
                timeCount = 90 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[6].attackSpeedValue);
                break;
            case 1:
                shieldPoint *= (float)DataManager.instance.itemList.item[6].attackPowerValue;
                timeCount = 90 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[21].attackSpeedValue);
                break;
            case 2:
                shieldPoint *= (float)DataManager.instance.itemList.item[6].attackPowerValue;
                timeCount = 90 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[36].attackSpeedValue);
                break;
            case 3:
                shieldPoint *= (float)DataManager.instance.itemList.item[6].attackPowerValue;
                timeCount = 90 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[51].attackSpeedValue);
                break;
        }

        while (!nightManager.isStageEnd)
        {
            character.SetShieldPointData(shieldPoint);
            barriorScript.CreateBarrior();
            
            yield return new WaitUntil(() => character.ReturnCharacterShieldPoint() == 0);
            barriorScript.SetBarriorActive(false);

            yield return new WaitForSeconds((float)timeCount);
        }
    }

    IEnumerator HologramTrickCoroutine(int getRank)
    {
        GameObject[] hologramParentArr = new GameObject[2];
        Vector3 hologramVector;
        character.isHologramAnimate = true;

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
            character.hologramAnimatorArr[i] = hologramParent.GetComponent<Animator>();
            character.hologramRendererArr[i] = hologramParent.GetComponent<SpriteRenderer>();
        }

        float characterAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float characterAttackRange = (float)character.ReturnCharacterAttackRange();

        float duration = characterAttackRange;
        float timeCount = 1200 / characterAttackSpeed;

        switch (getRank)
        {
            case 0:
                timeCount = 120 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[7].attackSpeedValue);
                duration = characterAttackRange * (float)DataManager.instance.itemList.item[7].attackRangeValue;
                break;
            case 1:
                timeCount = 120 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[22].attackSpeedValue);
                duration = characterAttackRange * (float)DataManager.instance.itemList.item[22].attackRangeValue;
                break;
            case 2:
                timeCount = 120 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[37].attackSpeedValue);
                duration = characterAttackRange * (float)DataManager.instance.itemList.item[37].attackRangeValue;
                break;
            case 3:
                timeCount = 120 / (characterAttackSpeed * (float)DataManager.instance.itemList.item[52].attackSpeedValue);
                duration = characterAttackRange * (float)DataManager.instance.itemList.item[52].attackRangeValue;
                break;
        }

        while (!nightManager.isStageEnd)
        {
            character.isHologramTrickOn = true;
            yield return new WaitForSeconds(duration);
            character.isHologramTrickOn = false;
            
            yield return new WaitForSeconds(timeCount);
        }

        character.isHologramAnimate = false;
    }

    void AntiPhenet(int getRank)
    {
        character.isAntiPhenetOn = true;

        switch(getRank)
        {
            case 0:
                character.SetAntiPhenetData((float)DataManager.instance.itemList.item[8].attackPowerValue);
                break;
            case 1:
                character.SetAntiPhenetData((float)DataManager.instance.itemList.item[23].attackPowerValue);
                break;
            case 2:
                character.SetAntiPhenetData((float)DataManager.instance.itemList.item[38].attackPowerValue);
                break;
            case 3:
                character.SetAntiPhenetData((float)DataManager.instance.itemList.item[53].attackPowerValue);
                break;
        }
    }

    void RegenerationArmor(int getRank)
    {
        float addHp = (float)character.ReturnCharacterAttackPower();
        float addHpRegen = (float)character.ReturnCharacterAttackRange();

        switch (getRank)
        {
            case 0:
                addHp *= (float)DataManager.instance.itemList.item[9].attackPowerValue;
                addHpRegen *= (float)DataManager.instance.itemList.item[9].attackRangeValue;
                Debug.Log(addHpRegen);
                break;
            case 1:
                addHp *= (float)DataManager.instance.itemList.item[24].attackPowerValue;
                addHpRegen *= (float)DataManager.instance.itemList.item[24].attackRangeValue;
                break;
            case 2:
                addHp *= (float)DataManager.instance.itemList.item[39].attackPowerValue;
                addHpRegen *= (float)DataManager.instance.itemList.item[39].attackRangeValue;
                break;
            case 3:
                addHp *= (float)DataManager.instance.itemList.item[54].attackPowerValue;
                addHpRegen *= (float)DataManager.instance.itemList.item[54].attackRangeValue;
                break;
        }

        character.SetCharacterHitPointMax(addHp);
        character.SetCharacterHpRegen(addHpRegen);
    }

    void GravityBind(int getRank)
    {
        GameObject gravityBindParent = Instantiate(itemPrefabArr[11]);
        gravityBindParent.transform.SetParent(characterParent.transform);
        gravityBindParent.transform.localPosition = character.transform.position;
        gravityBindParent.transform.GetChild(0).GetComponent<GravityBind>().character = character;
        gravityBindParent.transform.GetChild(0).GetComponent<GravityBind>().nightManager = nightManager;
        gravityBindParent.transform.GetChild(0).GetComponent<GravityBind>().SetItemRank(getRank);
    }

    IEnumerator MoveBack(int getRank)
    {
        float getAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float timeCount = 200 / getAttackSpeed;

        switch (getRank)
        {
            case 0:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[11].attackSpeedValue);
                break;
            case 1:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[26].attackSpeedValue);
                break;
            case 2:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[41].attackSpeedValue);
                break;
            case 3:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[56].attackSpeedValue);
                break;
        }
        
        while (!nightManager.isStageEnd)
        {
            character.isMoveBackOn = true;
            yield return new WaitForSeconds(timeCount);
        }
    }

    IEnumerator BoosterCoroutine(int getRank)
    {
        float getBasicSpeed = (float)character.ReturnCharacterMoveSpeed();
        float getAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float getAttackRange = (float)character.ReturnCharacterAttackRange();
        float getAttackPower = (float)character.ReturnCharacterAttackPower();

        float timeCount = 300 / getAttackSpeed;
        float duration = getAttackRange;
        double speed = getAttackPower;
        
        switch (getRank)
        {
            case 0:
                timeCount = 30 / (getAttackSpeed * (float)DataManager.instance.itemList.item[12].attackSpeedValue);
                duration = getAttackRange * (float)DataManager.instance.itemList.item[12].attackRangeValue;
                speed = getBasicSpeed + getAttackPower * (float)DataManager.instance.itemList.item[12].attackPowerValue;
                break;
            case 1:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[27].attackSpeedValue);
                duration = getAttackRange * (float)DataManager.instance.itemList.item[27].attackRangeValue;
                speed = getBasicSpeed + getAttackPower * (float)DataManager.instance.itemList.item[27].attackPowerValue;
                break;
            case 2:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[42].attackSpeedValue);
                duration = getAttackRange * (float)DataManager.instance.itemList.item[42].attackRangeValue;
                speed = getBasicSpeed + getAttackPower * (float)DataManager.instance.itemList.item[42].attackPowerValue;
                break;
            case 3:
                timeCount = 20 / (getAttackSpeed * (float)DataManager.instance.itemList.item[57].attackSpeedValue);
                duration = getAttackRange * (float)DataManager.instance.itemList.item[57].attackRangeValue;
                speed = getBasicSpeed + getAttackPower * (float)DataManager.instance.itemList.item[57].attackPowerValue;
                break;
        }
        
        while (!nightManager.isStageEnd)
        {
            character.SetMoveSpeed(speed);
            yield return new WaitForSeconds(duration);
            character.SetMoveSpeed(getBasicSpeed);

            yield return new WaitForSeconds(timeCount - duration);
        }
    }

    void BioSnach(int getRank)
    {
        float getAbsorbAttackData = 1;

        switch (getRank)
        {
            case 0:
                getAbsorbAttackData = 1;
                break;
            case 1:
                getAbsorbAttackData = 2;
                break;
            case 2:
                getAbsorbAttackData = 3;
                break;
            case 3:
                getAbsorbAttackData = 5;
                break;
        }
        character.SetAbsorbAttackData(getAbsorbAttackData);
    }
    
    void InterceptDroneCoroutine(int getRank)
    {
        GameObject interceptDroneParent = Instantiate(itemPrefabArr[15]);
        interceptDroneParent.transform.SetParent(character.transform);
        interceptDroneParent.transform.localPosition = character.transform.position;
        interceptDroneParent.GetComponent<InterceptDrone>().character = character;
        interceptDroneParent.GetComponent<InterceptDrone>().nightManager = nightManager;
        interceptDroneParent.GetComponent<InterceptDrone>().SetItemRank(getRank);

        float getAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float getAttackRange = (float)character.ReturnCharacterAttackRange();

        interceptDroneParent.GetComponent<InterceptDrone>().SetInterceptDrone(getAttackRange, getAttackSpeed);
    }
}
