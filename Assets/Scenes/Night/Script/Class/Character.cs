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
    //CharacterTrashData characterData;
    Player characterData;
    [SerializeField]
    bool isCheat;

    //컨트롤 변수
    [SerializeField] 
    float hpBarPositionController;
    double maxShield;
    public Vector3 nowDir;
    float hitboxDistance = 3; //히트박스와 캐릭터와의 거리
    public Vector3 fixPos = Vector3.zero;
    bool isAttack;
    public bool canChange = false;
    public bool isAbsorb = false;
    public double nowMoveSpeed;

    //아이템 관련
    public bool isDoubleAttack = false;
    public bool isHologramTrickOn = false;
    public bool isHologramAnimate = false;
    public bool isAntiPhenetOn = false;
    public bool isMoveBackOn = false;

    // 애니메이션
    private Animator animator;
    public Animator[] hologramAnimatorArr;
    public SpriteRenderer[] hologramRendererArr;

    private void Awake() {
        //characterData = new CharacterTrashData(isCheat);
        GameManager.instance.LoadPlayerData();          //임시코드@@@@@@@@@@@@@

        characterData = GameManager.instance.player;
        if (characterData.curHp == 0)
            characterData.curHp = characterData.maxHp;
        characterRigid = this.GetComponent<Rigidbody2D>();
        characterSpriteRanderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //캐릭터의 스테이터스를 장비 등 변화에 따라 변화시킨다.
        hitBoxScript.getAttackPower = characterData.attackPower;    //무기 공격력 임시로 줌
        nowMoveSpeed = characterData.moveSpeed;
    }

    private void Start() {
        SetHitBoxScale();
        StartAttack();

        if (characterData.hpRegen > 0)
            HpRegen();
    }

    private void OnTriggerEnter2D(Collider2D collision) { 
        //캐릭터 데미지 받을 때
        CharacterDamaged(collision);
    }

    void SetHitBoxScale()
    {
        float goalY = (float)GameManager.instance.player.attackRange * 0.15f;
        Vector3 goalScale = new Vector3(1, goalY, 1);

        hitBox.transform.localScale = goalScale;
    }

    public void SetCharacterTrashData(EffectType getEffectType, float getEffectValue, bool getEffectMulti)
    {
        switch (getEffectType)
        {
            case EffectType.none:
                break;
            case EffectType.hp:
                    characterData.curHp += getEffectValue;
                    characterData.maxHp += getEffectValue;
                break;
            case EffectType.moveSpeed:
                characterData.moveSpeed += getEffectValue;
                break;
            case EffectType.attackPower:
                characterData.attackPower += getEffectValue;
                break;
            case EffectType.attackSpeed:
                characterData.attackSpeed += getEffectValue;
                break;
            case EffectType.attackRange:
                characterData.attackRange += getEffectValue;
                break;
            case EffectType.startMoney:
                if (!getEffectMulti)
                    characterData.startMoney += (int)getEffectValue;
                break;
            case EffectType.earnMoney:
                characterData.earnMoney += getEffectValue;
                break;
            case EffectType.shopSlot:
                if (!getEffectMulti)
                    characterData.shopSlot += (int)getEffectValue;
                break;
            case EffectType.itemSlot:
                if (!getEffectMulti)
                    characterData.itemSlot += (int)getEffectValue;
                break;
            case EffectType.shopMinRank:
                if (!getEffectMulti)
                    characterData.shopMinRank += (int)getEffectValue;
                break;
            case EffectType.shopMaxRank:
                if (!getEffectMulti)
                    characterData.shopMaxRank += (int)getEffectValue;
                break;
            case EffectType.dropRank:
                if (!getEffectMulti)
                    characterData.dropRank += (int)getEffectValue;
                break;
            case EffectType.dropRate:
                characterData.dropRate += getEffectValue;
                break;
            case EffectType.healByHit:
                characterData.healByHit += getEffectValue;
                break;
            case EffectType.hpRegen:
                characterData.hpRegen += getEffectValue;
                break;
            case EffectType.dealOnMax:
                    characterData.dealOnMaxHp += getEffectValue;
                break;
            case EffectType.dealOnHp:
                    characterData.dealOnCurHp += getEffectValue;
                break;
        }
    }

    // 아래는 이동 관련 로직(이 주석은 리팩토링이 끝나면 지울 것)
    public void StartMove(Vector3 joystickDir) {
        nowDir = joystickDir.normalized;
        characterRigid.velocity = joystickDir.normalized * ConvertMoveSpeedToPixelSpeed(characterData.moveSpeed);
        animator.SetBool("move", true);
        characterSpriteRanderer.flipX = (nowDir.x < 0) ? false : true;

        //홀로그램도 같이 움직이도록
        if(isHologramAnimate)
        {
            for(int i =0; i<2; i++)
            {
                hologramAnimatorArr[i].SetBool("move", true);
                hologramRendererArr[i].flipX = (nowDir.x < 0) ? false : true;
            }
        }

        //transform.localScale = (nowDir.x < 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);     아이템도 같이 이동해서 주석처리했어
    }

    public void EndMove() {
        nowDir = Vector3.zero;
        characterRigid.velocity = Vector3.zero;
        animator.SetBool("move", false);

        //홀로그램도 같이 움직이도록
        if (isHologramAnimate)
        {
            for (int i = 0; i < 2; i++)
            {
                hologramAnimatorArr[i].SetBool("move", false);
            }
        }
    }

    float ConvertMoveSpeedToPixelSpeed(double getMoveSpeed) {
        return ((float)getMoveSpeed * 25) / 100;
    }

    //Character 스크립트에서는 공격 애니메이션과 히트 박스 온오프만 사용
    //실질적 데이터 교환은 enemy 스크립트에서 이루어짐.
    public void StartAttack() {
        if (!isAttack) {
            isAttack = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    public void StopAttack() {
        StopCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine() {
        float attackSpeed = 10 / (float)GameManager.instance.player.attackSpeed;

        while (!nightManager.isStageEnd) {
            if (!isDoubleAttack) {
                //공격 애니메이션 진행
                animator.SetTrigger("attack");

                //홀로그램도 같이 움직이도록
                if (isHologramAnimate)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        hologramAnimatorArr[i].SetTrigger("attack");
                    }
                }

                hitBox.SetActive(true);
                SetHitbox();
                yield return new WaitForSeconds(0.2f);
                hitBox.SetActive(false);

                isMoveBackOn = false;
            } else {
                itemManager.SetMultiSlasherSprite(true);
                hitBox.SetActive(true);
                SetHitbox();
                yield return new WaitForSeconds(0.1f);
                hitBox.SetActive(false);

                yield return new WaitForSeconds(0.1f);

                hitBox.SetActive(true);
                SetHitbox();
                yield return new WaitForSeconds(0.2f);
                hitBox.SetActive(false);
                itemManager.SetMultiSlasherSprite(false);

                isDoubleAttack = false;
                isMoveBackOn = false;
            }

            //다음 공격까지 대기
            yield return new WaitForSeconds(attackSpeed);
            isAbsorb = false;
            isAttack = false;
        }
    }

    //이동속도 컨트롤할때 이 함수 쓸 예정
    public void SetMoveSpeed(double moveSpeed) {
        characterData.moveSpeed = moveSpeed;
    }

    //이동 방향에 따라 히트박스 위치 조절하는 함수
    public void SetHitbox() {
        fixPos = (nowDir.normalized != Vector3.zero) ? nowDir.normalized : ((fixPos == Vector3.zero) ? Vector3.left : fixPos);

        hitBox.transform.localPosition = this.transform.position + fixPos * hitboxDistance;
        float dot = Vector3.Dot(fixPos, new Vector3(1, 0, 0));
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if(fixPos.y >= 0)
            hitBox.transform.rotation = Quaternion.Euler(0, 0, angle);
        else
            hitBox.transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
        
        StartCoroutine(SetHitBoxCoroutine());
    }

    IEnumerator SetHitBoxCoroutine() {
        while(hitBox.gameObject.activeSelf == true) {
            hitBoxRigid.velocity = nowDir.normalized * ConvertMoveSpeedToPixelSpeed(characterData.moveSpeed);
            yield return null;
        }
    }


    // 나중에 리팩토링 할 예정
    // 1. 함수명
    // 2. 로직
    public void AbsorbAttack()   //canChange가 참이면 최대 체력일때 쉴드로 전환 가능
    {
        if(characterData.healByHit > 0 && !isAbsorb)
        {
            isAbsorb = true;

            if (characterData.curHp + characterData.healByHit < characterData.maxHp)
            {
                characterData.curHp += characterData.healByHit;
                hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
                //SetShieldImage();
            }
            else
            {
                //초과량 쉴드로 전환
                if(canChange)
                {
                    //쉴드가 최대 체력 초과면 리턴
                    if (characterData.shieldPoint >= characterData.maxHp)
                        return;    

                    double excessHeal = characterData.curHp + characterData.healByHit
                                                                            - characterData.maxHp;

                    //쉴드가 최대 체력을 넘지 못하게 제어
                    if (characterData.shieldPoint + excessHeal >= characterData.maxHp)
                    {
                        characterData.shieldPoint = characterData.maxHp;
                    }

                    //그냥 보호막 회복
                    else
                        characterData.shieldPoint += excessHeal;
                }

                characterData.curHp = characterData.maxHp;
                SetShieldImage();
            }    
        }
    }

    void HpRegen() {
        StartCoroutine(HpRegenCoroutine());
    }
    
    IEnumerator HpRegenCoroutine()
    {
        while (!nightManager.isStageEnd)
        {
            if (characterData.curHp < characterData.maxHp)
            {
                if (characterData.curHp + characterData.hpRegen >= characterData.maxHp)
                {
                    characterData.curHp = characterData.maxHp;
                }
                else
                {
                    characterData.curHp += characterData.hpRegen;
                }

                hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
                yield return new WaitForSeconds(1);
            }
            else
                yield return new WaitForSeconds(1);
        }
    }





    public void CharacterDamaged(Collider2D enemyCollision) {
        //공격이 성공할 때 히트박스가 캐릭터의 하위 오브젝트라 데미지를 받는 경우가 있어서 만든 코드
        if(hitBoxScript.isAttacked) {
            hitBoxScript.isAttacked = false;
            return;
        }

        GameObject enemy = enemyCollision.gameObject;
        double nowAttackPower = 0;

        if (enemy.tag == "Normal") {
            nowAttackPower = enemy.GetComponent<NormalEnemy>().GetEnemyAttackPower();
            enemy.GetComponent<NormalEnemy>().SetIsAttacked();
        }
        else if (enemy.tag == "Elite") {
            nowAttackPower = enemy.GetComponent<EliteEnemy>().GetEnemyAttackPower();
            enemy.GetComponent<EliteEnemy>().SetIsAttacked();
        }
        else if(enemy.tag == "Projectile") {
            if (enemy.GetComponent<Projectile>().isEnemy)  {
                nowAttackPower = enemy.transform.parent.GetComponent<EliteEnemy>().GetEnemyAttackPower();
                enemy.transform.parent.GetComponent<EliteEnemy>().SetIsAttacked();
            }
        }
        else {
            Debug.Log(enemy.name + "이 정상적이지 않아서 공격력을 받아올 수 없음");
            return;
        }


        if(nowAttackPower != 0)
            SetDamagedAnim();

        if (isHologramTrickOn)
            return;

        SetDamageData(nowAttackPower);
    }

    void SetDamageData(double getAttackData)
    {
        //안티 페넷 사용시 데미지 경감 계산
        getAttackData = AntiPhenetUse(getAttackData);

        //쉴드로 데미지 받을 때
        if (characterData.shieldPoint > 0)
        {
            //어차피 체력 100%에서 쉴드가 생기므로 
            //1. 체력바를 쉴드 비율만큼 옆으로 민다
            //2. 해당 체력바 위치에 쉴드 이미지를 놓는다.
            //3. 쉴드의 fillamount를 설정한다.
            if(characterData.shieldPoint >= getAttackData)
            {
                characterData.shieldPoint -= getAttackData;
                SetShieldImage();
            }
            else
            {
                double nowAttactDamage = getAttackData - (float)characterData.shieldPoint;
                characterData.shieldPoint = 0;
                SetShieldImage();

                characterData.curHp -= nowAttactDamage;
                //감소한 만큼 플레이어 체력바 줄어들게
                hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
            }
        }
        else { // 체력으로 데미지 받을 때
            //Hp - 적 데미지 계산
            if (characterData.curHp > getAttackData) {
                Debug.Log("damaged");
                characterData.curHp -= getAttackData;
                hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
            }
            else {
                //체력이 다 떨어졌으므로 사망
                Debug.Log("GameEnd");
                // TODO: 죽은 모션이 나온 후 결과 창이 뜨도록 딜레이 필요
                animator.SetTrigger("die");

                //홀로그램도 같이 움직이도록
                if (isHologramAnimate)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        hologramAnimatorArr[i].SetTrigger("die");
                    }
                }

                characterData.curHp = 0;
                hpBarImage.fillAmount = 0;

                nightManager.SetStageEnd();
            }
        }
    }

    //데미지를 받았을 경우 액션
    void SetDamagedAnim() {
        animator.SetTrigger("knockback");

        //홀로그램도 같이 움직이도록
        if (isHologramAnimate)
        {
            for (int i = 0; i < 2; i++)
            {
                hologramAnimatorArr[i].SetTrigger("knockback");
            }
        }

        StartCoroutine(SetDamagedAnimCoroutine());
    }

    IEnumerator SetDamagedAnimCoroutine() {
        for (int i = 0; i < 2; i++) {
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
        float shieldData = (float)characterData.curHp * getShieldPoint;
        characterData.shieldPoint = shieldData;
        SetShieldImage();
    }

    //해당 숫자만큼 쉴드 생성
    public void SetShieldPointData(float getShieldPoint)
    {
        characterData.shieldPoint = getShieldPoint;
        SetShieldImage();
    }
    

    void SetShieldImage()
    {
        Vector3 fixShieldPos = shieldBarImage.transform.localPosition;

        hpBarImage.fillAmount = (float)characterData.curHp /
                        ((float)characterData.shieldPoint + (float)characterData.curHp);

        fixShieldPos.x = hpBarImage.rectTransform.sizeDelta.x * hpBarImage.fillAmount;

        shieldBarImage.transform.localPosition = fixShieldPos;
        shieldBarImage.fillAmount = (float)characterData.shieldPoint /
                        ((float)characterData.shieldPoint + (float)characterData.curHp);
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
        return characterData.curHp;
    }

    public double ReturnCharacterHitPointMax()
    {
        return characterData.maxHp;
    }

    public double ReturnCharacterAttackPower() 
    { 
        return characterData.attackPower;
    }

    public double ReturnCharacterAttackSpeed()
    {
        return characterData.attackSpeed;
    }

    public void SetCharacterAttackPower(double getAttackPower)
    {
        characterData.attackPower = getAttackPower;
        hitBoxScript.getAttackPower = characterData.attackPower;
    }

    public void SetAbsorbAttackData(float getHealByHit)
    {
        characterData.healByHit += getHealByHit;
    }

    //아이템쪽
    public int ReturnCharacterItemSlot()
    {
        return characterData.itemSlot;
    }

    public void UpdateKillCount()
    {
        nightManager.UpdateKillCount();
    }
    public void UpdateKillNormalCount()
    {
        nightManager.UpdateKillNormalCount();
    }
    public void UpdateKillEliteCount()
    {
        nightManager.UpdateKillEliteCount();
    }

    public double ReturnCharacterMoveSpeed()
    {
        return characterData.moveSpeed;
    }

    public Vector3 ReturnSpeed()
    {
        return nowDir.normalized * ConvertMoveSpeedToPixelSpeed(characterData.moveSpeed);
    }

    //아이템 6번 퍼스트 에이드에서 체력 회복할 때 쓰려고 만듬
    public void HealHp(double getHealHp, GameObject getImage)
    {
        StartCoroutine(HealHpCoroutine(getHealHp, getImage));
    }

    IEnumerator HealHpCoroutine(double getHealHp, GameObject getImage)
    {
        float time = 3;
        float deltaTime = 0;
        double value = 0;
        double nowHeal = 0;

        getImage.SetActive(true);

        while (time> deltaTime)
        {
            deltaTime+= Time.deltaTime;
            value = getHealHp * Time.deltaTime;
            nowHeal += value;

            //힐량 초과시 종료
            if (nowHeal >= getHealHp)
                break;

            if (characterData.curHp < characterData.maxHp)
            {
                characterData.curHp += value;

                //만약 피가 오버되면 종료
                if(characterData.curHp >= characterData.maxHp)
                {
                    characterData.curHp = characterData.maxHp;
                    hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
                    break;
                }

                hpBarImage.fillAmount = (float)characterData.curHp / (float)characterData.maxHp;
            }
            yield return null;
        }
        getImage.SetActive(false);
        Debug.Log("End");
    }

    public double ReturnCharacterShieldPoint()
    {
        return characterData.shieldPoint;
    }

    public double ReturnCharacterAttackRange()
    {
        return characterData.attackRange;
    }

    //데미지 경감용

    public void SetAntiPhenetData(float getReductionRate)
    {
        characterData.damageReductionRate = getReductionRate;
    }

    double AntiPhenetUse(double getAttackPowerData)
    {
        if(isAntiPhenetOn)
        {
            return getAttackPowerData * (1 - characterData.damageReductionRate);
        }
        else
            return getAttackPowerData;
    }

    public void SetCharacterHitPointMax(double getHitPoint)
    {
        characterData.maxHp += getHitPoint;
        characterData.curHp = characterData.maxHp;
    }

    public void SetCharacterHpRegen(double getHpRegen)
    {
        characterData.hpRegen = getHpRegen;
    }
}
