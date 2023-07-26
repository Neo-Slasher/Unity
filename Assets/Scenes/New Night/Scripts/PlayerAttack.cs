using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();   
    }

    private void Start() {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        while (true) {
            yield return new WaitForSeconds(3.0f);
            animator.SetTrigger("attack");
        }
    }

}
