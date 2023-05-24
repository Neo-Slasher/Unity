using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    GameObject characterObject;
    [SerializeField]
    Rigidbody2D characterRigid;
    [SerializeField]
    SpriteRenderer characterSpriteRanderer;
    [SerializeField]
    Image hpBarImage;
    [SerializeField]
    GameObject hitBox;
    [SerializeField]
    HitBox hitBoxScript;

    //캐릭터 임시 데이터
    [SerializeField]
    CharacterTrashData characterTrashData;
    int nowHp;

    //컨트롤 변수
    Vector3 nowDir;
    float hitboxDistance = 1.75f; //히트박스와 캐릭터와의 거리
    bool isAttack;

    private void Start()
    {
        characterRigid = GetComponent<Rigidbody2D>();
        characterSpriteRanderer = GetComponent<SpriteRenderer>();

        //캐릭터의 스테이터스를 장비 등 변화에 따라 변화시킨다.
        hitBoxScript.getOffensePower = characterTrashData.offensePower;    //무기 공격력 임시로 줌

        //기본 Hp값 설정
        nowHp = characterTrashData.Hp;

        CharacterAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //캐릭터 데미지 먹을 때
        CharacterDamaged(collision);
    }

    public void CharacterMove(Vector3 joystickDir)
    {
        //캐릭 이동모션 시작

        //캐릭 실제 이동
        nowDir= joystickDir.normalized;
        characterRigid.velocity = joystickDir.normalized * SetMoveSpeed(characterTrashData.moveSpeed);
    }

    float SetMoveSpeed(int getMoveSpeed)
    {
        float result = 0;
        result = ((float)getMoveSpeed * 25) / 128;

        return result;
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
            //공격 애니메이션 진행

            //히트박스 온오프
            SetHitbox();
            hitBox.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            hitBox.SetActive(false);

            //다음 공격까지 대기
            yield return new WaitForSeconds(0.5f);
            isAttack = false;
        }

        hitBox.SetActive(false);
    }

    //이동 방향에 따라 히트박스 위치 조절하는 함수
    void SetHitbox()
    {
        if (nowDir != Vector3.zero)
        {
            hitBox.transform.localPosition = nowDir * hitboxDistance;
            float dot = Vector3.Dot(nowDir, new Vector3(1, 0, 0));
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if(nowDir.y >= 0)
                hitBox.transform.rotation = Quaternion.Euler(0, 0, angle);
            else
                hitBox.transform.rotation = Quaternion.Euler(0, 0, 180 - angle);
        }
        else
        {
            hitBox.transform.localPosition = new Vector3(hitboxDistance, 0, 0);
            hitBox.transform.rotation = Quaternion.Euler(0, 0, 0);
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

        Debug.Log("Damaged!");
        GameObject nowCollision = collision.gameObject;
        int nowAttackPower = 0;

        //적 데미지 받아오기
        if (nowCollision.name == "NormalEnemyPrefab(Clone)")
        {
            nowAttackPower = nowCollision.GetComponent<NormalEnemy>().GetEnemyAttackPower();
            nowCollision.GetComponent<NormalEnemy>().SetIsAttacked();
        }
        else if (nowCollision.name == "EliteEnemyPrefab(Clone)")
        {
            nowAttackPower = nowCollision.GetComponent<EliteEnemy>().GetEnemyAttackPower();
            nowCollision.GetComponent<EliteEnemy>().SetIsAttacked();
        }
        else
        {
            Debug.Log(nowCollision.name + "이 정상적이지 않아서 공격력을 받아올 수 없음");
            return;
        }


        if(nowAttackPower != 0)
            SetDamagedAnim();

        //Hp - 적 데미지 계산
        if (nowHp > nowAttackPower)
        {
            //캐릭터 체력이 더 높으므로 체력 감소
            nowHp -= nowAttackPower;
            //감소한 만큼 플레이어 체력바 줄어들게
            hpBarImage.fillAmount = (float)nowHp / (float)characterTrashData.Hp;
            
        }
        else
        {
            //체력이 다 떨어졌으므로 사망
            nowHp = 0;
            hpBarImage.fillAmount = 0;
            nightManager.SetStageEnd();
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
}