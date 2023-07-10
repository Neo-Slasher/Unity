using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    double attackDamage = 500;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Normal" || collision.tag == "Elite")
            collision.GetComponent<EnemyParent>().EnemyDamaged(attackDamage);
        else if(collision.name == "BackGround")
            Destroy(this.gameObject);
    }
}
