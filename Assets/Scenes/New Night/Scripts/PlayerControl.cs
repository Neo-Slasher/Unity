using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;

    public float speed;
    public FloatingJoystick floatingJoystick;
    Vector3 moveDirection;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        moveDirection = new Vector3(floatingJoystick.Horizontal * Time.deltaTime * speed, floatingJoystick.Vertical * Time.deltaTime * speed, 0.0f); // normalize 안한 버전
        
        transform.localScale = (floatingJoystick.Horizontal <= 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        animator.SetBool("run", (moveDirection != Vector3.zero) ? true : false);

        transform.Translate(moveDirection);
    }
}
