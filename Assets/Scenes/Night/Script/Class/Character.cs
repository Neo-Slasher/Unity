using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    ItemManager itemManager;
    [SerializeField]
    GameObject characterObject;
    [SerializeField]
    Rigidbody2D characterRigid;
    [SerializeField]
    Rigidbody2D hitBoxRigid;
    [SerializeField]
    SpriteRenderer characterSpriteRanderer;
    [SerializeField]
    GameObject hpBarParent;
    [SerializeField]
    Image hpBarImage;
    [SerializeField]
    Image shieldBarImage;
    [SerializeField]
    GameObject hitBox;
    [SerializeField]
    HitBox hitBoxScript;

    //캐릭터 임시 데이터
    [SerializeField]
    CharacterTrashData characterTrashData;
    [SerializeField]
    bool isCheat;

    //컨트롤 변수
    [SerializeField] 
    float hpBarPositionController;
    double maxShield;
    public Vector3 nowDir;
    public bool isHitBoxFix = false;
    float hitboxDistance = 3; //히트박스와 캐릭터와의 거리
    public Vector3 fixPos = Vector3.zero;
    bool isAttack;
    public bool canChange = false;
    public bool isAbsorb = false;
    public double nowMoveSpeed;

    //아이템 관련
    public bool isDoubleAttack = false;
    public bool isHologramTrickOn = false;
    public bool isAntiPhenetOn = false;
    public bool isMoveBackOn = false;

    private void Awake()
    {
        characterTrashData = new CharacterTrashData(isCheat);
        characterRigid = this.GetComponent<Rigidbody2D>();
        characterSpriteRanderer = GetComponent<SpriteRenderer>();

        //캐릭터의 스테이터스를 장비 등 변화에 따라 변화시킨다.
        hitBoxScript.getAttackPower = characterTrashData.attackPower;    //무기 공격력 임시로 줌
        nowMoveSpeed = characterTrashData.moveSpeed;
    }

    private void Start()
    {
        CharacterAttack();

        if (characterTrashData.hpRegen > 0)
            HpRegen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //캐릭터 데미지 먹을 때
        CharacterDamaged(collision);
    }

    public void SetCharacterTrashData(EffectType getEffectType, float getEffectValue, bool getEffectMulti)
    {
        switch (getEffectType)
        {
            case EffectType.none:
                break;
            case EffectType.hp:
                    characterTrashData.hitPoint += getEffectValue;
                    characterTrashData.hitPointMax += getEffectValue;
                break;
            case EffectType.moveSpeed:
                characterTrashData.moveSpeed += getEffectValue;
                break;
            case EffectType.attackPower:
                characterTrashData.attackPower += getEffectValue;
                break;
            case EffectType.attackSpeed:
                characterTrashData.attackSpeed += getEffectValue;
                break;
            case EffectType.attackRange:
                characterTrashData.attackRange += getEffectValue;
                break;
            case EffectType.startMoney:
                if (!getEffectMulti)
                    characterTrashData.startMoney += (int)getEffectValue;
                break;
            case EffectType.earnMoney:
                characterTrashData.earnMoney += getEffectValue;
                break;
            case EffectType.shopSlot:
                if (!getEffectMulti)
                    characterTrashData.shopSlot += (int)getEffectValue;
                break;
            case EffectType.itemSlot:
                if (!getEffectMulti)
                    characterTrashData.itemSlot += (int)getEffectValue;
                break;
            case EffectType.shopMinRank:
                if (!getEffectMulti)
                    characterTrashData.shopMinRank += (int)getEffectValue;
                break;
            case EffectType.shopMaxRank:
                if (!getEffectMulti)
                    characterTrashData.shopMaxRank += (int)getEffectValue;
                break;
            case EffectType.dropRank:
                if (!getEffectMulti)
                    characterTrashData.dropRank += (int)getEffectValue;
                break;
            case EffectType.dropRate:
                characterTrashData.dropRate += getEffectValue;
                break;
            case EffectType.healByHit:
                characterTrashData.healByHit += getEffectValue;
                break;
            case EffectType.hpRegen:
                characterTrashData.hpRegen += getEffectValue;
                break;
            case EffectType.dealOnMax:
                    characterTrashData.dealOnMax += getEffectValue;
                break;
            case EffectType.dealOnHp:
                    characterTrashData.dealOnHp += getEffectValue;
                break;
        }
    }

    public void CharacterMove(Vector3 joystickDir)
    {
        //캐릭 이동모션 시작

        //캐릭 실제 이동
        nowDir= joystickDir.normalized;
        characterRigid.velocity = joystickDir.normalized * SetMoveSpeed(characterTrashData.moveSpeed);
    }

    float SetMoveSpeed(double getMoveSpeed)
    {
        double result = 0;
        result = (getMoveSpeed * 25) / 100;

        return (float)result;
    }

    //이동속도 컨트롤할때 이 함수 쓸 예정
    public void SetMoveSpeendData(double getMoveSpeed)
    {
        characterTrashData.moveSpeed = getMoveSpeed;
    }

    public void CharacterStop(Vector3 joystickDir)
    {
        nowDir = Vector3.zero;
        characterRigid.velocity = Vector3.zero;
    }

    //Character 스크립트에서는 공격 애니메이션과 히트 박스 온오프만 사용
    //실질적 데이터 교환은 enemy 스크립트에서 이루어짐.
    public void CharacterAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(CharacterAttackCoroutine());
        }
    }

    public void CharacterAttackStop()
    {
        StopCoroutine(CharacterAttackCoroutine());
    }

    IEnumerator CharacterAttackCoroutine()
    {
        while (!nightManager.isStageEnd)
        {
            if (!isDoubleAttack)
            {
                //공격 애니메이션 진행

                //히트박스 온오프
                hitBox.SetActive(true);
                SetHitbox();
                isHitBoxFix = true;

                yield return new WaitForSeconds(0.5f);
                hitBox.SetActive(false);
                isMoveBackOn = false;
            }
            else
            {
                //히트박스 온오프
                hitBox.SetActive(true);
                SetHitbox();
                isHitBoxFix = true;

                yield return new WaitForSeconds(0.1f);
                hitBox.SetActive(false);

                yield return new WaitForSeconds(0.1f);
                hitBox.SetActive(true);
                SetHitbox();
                yield return new WaitForSeconds(0.5f);
                hitBox.SetActive(false);

                isDoubleAttack = false;
                isMoveBackOn = false;
            }

            //다음 공격까지 대기
            yield return new WaitForSeconds(0.5f);
            isAbsorb = false;
            isAttack = false;
            isHitBoxFix = false;
        }
    }

    //이동 방향에 따라 히트박스 위치 조절하는 함수
    public void SetHitbox()
    {
        if (!isHitBoxFix)
            fixPos = nowDir.normalized;

        if (nowDir != Vector3.zero)
        {
            hitBox.transform.localPosition = this.transform.position + fixPos * hitboxDistance;
            float dot = Vector3.Dot(fixPos, new Vector3(1, 0, 0));
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if(fixPos.y >= 0)
                hitBox.transform.rotation = Quaternion.Euler(0, 0, angle);
            else
                hitBox.transform.rotation = Quaternion.Euler(0, 0, 180 - angle);
        }
        else
        {
            hitBox.transform.localPosition = this.transform.position + new Vector3(hitboxDistance, 0, 0);
            hitBox.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        StartCoroutine(SetHitBoxCoroutine());
    }

    IEnumerator SetHitBoxCoroutine()
    {
        while(hitBox.gameObject.activeSelf == true)
        {
            hitBoxRigid.velocity = nowDir.normalized * SetMoveSpeed(characterTrashData.moveSpeed);
            yield return null;
        }
    }

    public void AbsorbAttack()   //canChange가 참이면 최대 체력일때 쉴드로 전환 가능
    {
        if(characterTrashData.healByHit > 0 && !isAbsorb)
        {
            isAbsorb = true;

            if (characterTrashData.hitPoint + characterTrashData.healByHit < characterTrashData.hitPointMax)
            {
                characterTrashData.hitPoint += characterTrashData.healByHit;
                hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;
                //SetShieldImage();
            }
            else
            {
                //초과량 쉴드로 전환
                if(canChange)
                {
                    //쉴드가 최대 체력 초과면 리턴
                    if (characterTrashData.shieldPoint >= characterTrashData.hitPointMax)
                        return;    

                    double excessHeal = characterTrashData.hitPoint + characterTrashData.healByHit
                                                                            - characterTrashData.hitPointMax;

                    //쉴드가 최대 체력을 넘지 못하게 제어
                    if (characterTrashData.shieldPoint + excessHeal >= characterTrashData.hitPointMax)
                    {
                        characterTrashData.shieldPoint = characterTrashData.hitPointMax;
                    }

                    //그냥 보호막 회복
                    else
                        characterTrashData.shieldPoint += excessHeal;
                }

                characterTrashData.hitPoint = characterTrashData.hitPointMax;
                SetShieldImage();
            }    
        }
    }

    void HpRegen()
    {
        StartCoroutine(HpRegenCoroutine());
    }
    
    IEnumerator HpRegenCoroutine()
    {
        while (!nightManager.isStageEnd)
        {
            if (characterTrashData.hitPoint < characterTrashData.hitPointMax)
            {
                if (characterTrashData.hitPoint + characterTrashData.hpRegen >= characterTrashData.hitPointMax)
                {
                    characterTrashData.hitPoint = characterTrashData.hitPointMax;
                }
                else
                {
                    characterTrashData.hitPoint += characterTrashData.hpRegen;
                }

                hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;
                yield return new WaitForSeconds(1);
            }
            else
                yield return new WaitForSeconds(1);
        }
    }

    public void CharacterDamaged(Collider2D collision)
    {
        //공격이 성공할 때 히트박스가 캐릭터의 하위 오브젝트라 데미지를 받는 경우가 있어서 만든 코드
        if(hitBoxScript.isAttacked)
        {
            hitBoxScript.isAttacked = false;
            return;
        }

        //Debug.Log("Damaged!");
        GameObject nowCollision = collision.gameObject;
        double nowAttackPower = 0;

        //적 데미지 받아오기
        if (nowCollision.tag == "Normal")
        {
            nowAttackPower = nowCollision.GetComponent<NormalEnemy>().GetEnemyAttackPower();
            nowCollision.GetComponent<NormalEnemy>().SetIsAttacked();
        }
        else if (nowCollision.tag == "Elite")
        {
            nowAttackPower = nowCollision.GetComponent<EliteEnemy>().GetEnemyAttackPower();
            nowCollision.GetComponent<EliteEnemy>().SetIsAttacked();
        }
        else if(nowCollision.tag == "Projectile")
        {
            if (nowCollision.GetComponent<Projectile>().isEnemy)
            {
                nowAttackPower = nowCollision.transform.parent.GetComponent<EliteEnemy>().GetEnemyAttackPower();
                nowCollision.transform.parent.GetComponent<EliteEnemy>().SetIsAttacked();
            }
        }
        else
        {
            Debug.Log(nowCollision.name + "이 정상적이지 않아서 공격력을 받아올 수 없음");
            return;
        }


        if(nowAttackPower != 0)
            SetDamagedAnim();

        //홀로그램 켜지면 그냥 데미지 계산 안하고 빠빠이
        if (isHologramTrickOn)
            return;

        SetDamageData(nowAttackPower);
    }

    void SetDamageData(double getAttackData)
    {
        //안티 페넷 사용시 데미지 경감 계산
        getAttackData = AntiPhenetUse(getAttackData);

        //쉴드로 데미지 받을 때
        if (characterTrashData.shieldPoint > 0)
        {
            //어차피 체력 100%에서 쉴드가 생기므로 
            //1. 체력바를 쉴드 비율만큼 옆으로 민다
            //2. 해당 체력바 위치에 쉴드 이미지를 놓는다.
            //3. 쉴드의 fillamount를 설정한다.
            if(characterTrashData.shieldPoint >= getAttackData)
            {
                characterTrashData.shieldPoint -= getAttackData;
                SetShieldImage();
            }
            else
            {
                double nowAttactDamage = getAttackData - (float)characterTrashData.shieldPoint;
                characterTrashData.shieldPoint = 0;
                SetShieldImage();

                characterTrashData.hitPoint -= nowAttactDamage;
                //감소한 만큼 플레이어 체력바 줄어들게
                hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;
            }
        }

        //체력으로 데미지 받을 때
        else
        {
            //Hp - 적 데미지 계산
            if (characterTrashData.hitPoint > getAttackData)
            {
                //캐릭터 체력이 더 높으므로 체력 감소
                characterTrashData.hitPoint -= getAttackData;
                //감소한 만큼 플레이어 체력바 줄어들게
                hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;

            }
            else
            {
                Debug.Log("GameEnd");
                //체력이 다 떨어졌으므로 사망
                characterTrashData.hitPoint = 0;
                hpBarImage.fillAmount = 0;
                nightManager.SetStageEnd();
            }
        }
    }

    //데미지를 받았을 경우 액션
    void SetDamagedAnim()
    {
        StartCoroutine(SetDamagedAnimCoroutine());
    }

    IEnumerator SetDamagedAnimCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            Color nowColor = characterSpriteRanderer.color;
            nowColor.a = 0.7f;
            characterSpriteRanderer.color = nowColor;
            yield return new WaitForSeconds(0.2f);

            nowColor.a = 1f;
            characterSpriteRanderer.color = nowColor;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void SetStartShieldPointData(float getShieldPoint)
    {
        float shieldData = (float)characterTrashData.hitPoint * getShieldPoint;
        characterTrashData.shieldPoint = shieldData;
        SetShieldImage();
    }

    //해당 숫자만큼 쉴드 생성
    public void SetShieldPointData(float getShieldPoint)
    {
        characterTrashData.shieldPoint = getShieldPoint;
        SetShieldImage();
    }
    

    void SetShieldImage()
    {
        Vector3 fixShieldPos = shieldBarImage.transform.localPosition;

        hpBarImage.fillAmount = (float)characterTrashData.hitPoint /
                        ((float)characterTrashData.shieldPoint + (float)characterTrashData.hitPoint);

        fixShieldPos.x = hpBarImage.rectTransform.sizeDelta.x * hpBarImage.fillAmount;

        shieldBarImage.transform.localPosition = fixShieldPos;
        shieldBarImage.fillAmount = (float)characterTrashData.shieldPoint /
                        ((float)characterTrashData.shieldPoint + (float)characterTrashData.hitPoint);
    }

    public void SetHpBarPosition()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 
                                                            transform.position.y - hpBarPositionController, 0));
        hpBarParent.transform.position = hpBarPos;
    }

    //특성에서 주변을 탐색하고 싶을 때 사용할 함수
    public Collider2D[] ReturnOverLapColliders(float maxRadius, float minRadius)
    {
        Collider2D[] overLapMaxColArr = Physics2D.OverlapCircleAll(this.transform.position, maxRadius);
        Collider2D[] overLapMinColArr = Physics2D.OverlapCircleAll(this.transform.position, minRadius);
        Collider2D[] overLapColArr = null;
        if (overLapMinColArr.Length != 0)
            overLapColArr = overLapMaxColArr.Except(overLapMinColArr).ToArray();

        return overLapColArr;
    }

    public Collider2D[] ReturnOverLapColliders(float radius)
    {
        Collider2D[] overLapColArr = Physics2D.OverlapCircleAll(this.transform.position, radius);

        return overLapColArr;
    }

    public double ReturnCharacterHitPoint()
    {
        return characterTrashData.hitPoint;
    }

    public double ReturnCharacterHitPointMax()
    {
        return characterTrashData.hitPointMax;
    }

    public double ReturnCharacterAttackPower() 
    { 
        return characterTrashData.attackPower;
    }

    public double ReturnCharacterAttackSpeed()
    {
        return characterTrashData.attackSpeed;
    }

    public void SetCharacterAttackPower(double getAttackPower)
    {
        characterTrashData.attackPower = getAttackPower;
        hitBoxScript.getAttackPower = characterTrashData.attackPower;
    }

    public void SetAbsorbAttackData(float getHealByHit)
    {
        characterTrashData.healByHit += getHealByHit;
    }

    //아이템쪽
    public int ReturnCharacterItemSlot()
    {
        return characterTrashData.itemSlot;
    }

    public void UpdateKillCount()
    {
        nightManager.UpdateKillCount();
    }

    public double ReturnCharacterMoveSpeed()
    {
        return characterTrashData.moveSpeed;
    }

    public Vector3 ReturnSpeed()
    {
        return nowDir.normalized * SetMoveSpeed(characterTrashData.moveSpeed);
    }

    //아이템 6번 퍼스트 에이드에서 체력 회복할 때 쓰려고 만듬
    public void HealHp(double getHealHp)
    {
        StartCoroutine(HealHpCoroutine(getHealHp));
    }

    IEnumerator HealHpCoroutine(double getHealHp)
    {
        float time = 3;
        float deltaTime = 0;
        double value = 0;
        double nowHeal = 0;

        while (time> deltaTime)
        {
            deltaTime+= Time.deltaTime;
            value = getHealHp * Time.deltaTime;
            nowHeal += value;

            //힐량 초과시 종료
            if (nowHeal >= getHealHp)
                break;

            if (characterTrashData.hitPoint < characterTrashData.hitPointMax)
            {
                characterTrashData.hitPoint += value;

                //만약 피가 오버되면 종료
                if(characterTrashData.hitPoint >= characterTrashData.hitPointMax)
                {
                    characterTrashData.hitPoint = characterTrashData.hitPointMax;
                    hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;
                    break;
                }

                hpBarImage.fillAmount = (float)characterTrashData.hitPoint / (float)characterTrashData.hitPointMax;
            }
            yield return null;
        }
        Debug.Log("End");
    }

    public double ReturnCharacterShieldPoint()
    {
        return characterTrashData.shieldPoint;
    }

    public double ReturnCharacterAttackRange()
    {
        return characterTrashData.attackRange;
    }

    //데미지 경감용
    double AntiPhenetUse(double getAttackPowerData)
    {
        if(isAntiPhenetOn)
        {
            return getAttackPowerData / characterTrashData.attackPower;
        }
        else
            return getAttackPowerData;
    }

    public void SetCharacterHitPointMax(double getHitPoint)
    {
        characterTrashData.hitPointMax += getHitPoint;
        characterTrashData.hitPoint = characterTrashData.hitPointMax;
    }

    public void SetCharacterHpRegen(double getHpRegen)
    {
        characterTrashData.hpRegen = getHpRegen;
    }
}
