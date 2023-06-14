using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "BackGround")
        {
            this.gameObject.SetActive(false);
            if(this.transform.parent != null)
                this.transform.position = this.transform.parent.position;
        }
    }
}
