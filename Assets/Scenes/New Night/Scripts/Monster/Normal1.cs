using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 메모
// 나중에 데이터 관련 손보면서 클래스 개념을 상속 관계로 나눠주자
public class Normal1 : MonoBehaviour
{
    Monster monster; // 나중에 MonsterStatus로 수정할 예정


    GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }


    void FixedUpdate() {
        transform.Translate(DirctionToPlayer() * Time.deltaTime); // * monster.moveSpeed
    }

    Vector3 DirctionToPlayer() {
        return (player.transform.position - transform.position).normalized;
    }

}

