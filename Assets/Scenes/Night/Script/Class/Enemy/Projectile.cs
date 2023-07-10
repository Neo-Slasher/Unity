using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public bool isEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemy)
        {
            //���� �� ����ü�� ���
            if (collision.name == "BackGround")
            {
                this.gameObject.SetActive(false);
                if (this.transform.parent != null)
                    this.transform.position = this.transform.parent.position;
            }
            else if(collision.name == "Character")
            {
                this.gameObject.SetActive(false);
                if (this.transform.parent != null)
                    this.transform.position = this.transform.parent.position;
            }
            
        }
        else
        {
            //���� �� ����ü�� ���
            if (collision.name == "BackGround")
            {
                this.gameObject.SetActive(false);
                if (this.transform.parent != null)
                    this.transform.position = this.transform.parent.parent.GetChild(0).position;
            }
            else if (collision.tag == "Normal" || collision.tag == "Elite")
            {
                this.gameObject.SetActive(false);
                if (this.transform.parent != null)
                    this.transform.position = this.transform.parent.parent.GetChild(0).position;
            }
        }
    }
}
