using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    GameObject characterObject;
    [SerializeField]
    Rigidbody characterRigid;
    [SerializeField]
    GameObject hitBox;

    //캐릭터 데이터
    public float tempSpeed = 2;

    //컨트롤 변수
    Vector3 nowDir;
    float hitboxDistance = 1.75f; //히트박스와 캐릭터와의 거리
    bool isAttack;

    private void Start()
    {
        characterRigid = GetComponent<Rigidbody>();

        CharacterAttack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //캐릭터 데미지 먹을 때
    }

    public void CharacterMove(Vector3 joystickDir)
    {
        //캐릭 이동모션 시작

        //캐릭 실제 이동
        nowDir= joystickDir.normalized;
        characterRigid.velocity = joystickDir.normalized * tempSpeed;
    }

    public void CharacterStop(Vector3 joystickDir)
    {
        nowDir = Vector3.zero;
        characterRigid.velocity = Vector3.zero;
    }

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

            //공격한 뒤 데이터 정리

            //다음 공격까지 대기
            yield return new WaitForSeconds(1);
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

    public void CharacterDamaged()
    {

    }
}
