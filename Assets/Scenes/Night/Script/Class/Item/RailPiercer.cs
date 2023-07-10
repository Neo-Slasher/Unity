using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RailPiercer : MonoBehaviour
{
    [SerializeField]
    GameObject railPiercerImage;
    [SerializeField]
    GameObject railPiercerHitBox;

    public NightManager nightManager;
    public Character character;

    Rigidbody2D railPiercerRigid;
    double attackPower = 500;
    double attackSpeed;
    float hitBoxScale;
    bool isWatchRight = true;
    bool isShoot = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Normal" || collision.tag == "Elite")
            collision.GetComponent<EnemyParent>().EnemyDamaged(attackPower);
    }

    public void ShootRailPiercer(double getAttackSpeed, double getAttackDamage)
    {
        railPiercerHitBox.SetActive(false);

        attackSpeed = getAttackSpeed;
        attackPower = getAttackDamage;

        //우측을 바라보면 우측으로 길이가 길어짐
        if (isWatchRight)
        {
            hitBoxScale = 21.6f - railPiercerImage.transform.position.x;
        }
        else
        {
            hitBoxScale = 21.6f + railPiercerImage.transform.position.x;
        }
        StartCoroutine(ShootRailPiercerCoroutine());
    }

    IEnumerator ShootRailPiercerCoroutine()
    {
        Vector3 hitBoxScaleVector = Vector3.zero;
        while (!nightManager.isStageEnd)
        {
            isShoot = true;
            //히트박스가 맵 끝까지 도달하도록 설정하는 부분
            //hitBoxScaleVector = railPiercerImage.transform.position;
            hitBoxScaleVector.y = 1;
            hitBoxScaleVector.x = hitBoxScale;
            railPiercerHitBox.transform.localScale = hitBoxScaleVector;

            //히트박스를 보는 방향에 쏘도록 x 좌표를 조정함
            Vector3 hitBoxPos = Vector3.zero;
            if (character.nowDir.x >= 0)
            {
                isWatchRight = true;
                hitBoxPos.x = hitBoxScale;
            }
            else
            {
                isWatchRight = false;
                hitBoxPos.x = (-1) * hitBoxScale;
            }
            hitBoxPos.x /= 2;
            railPiercerHitBox.transform.localPosition = hitBoxPos;


            //이제 쏘는 부분
            railPiercerHitBox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            railPiercerHitBox.SetActive(false);
            isShoot = false;

            yield return new WaitForSeconds((float)attackSpeed / 10);
        }
    }


    public void SetRailPiercerPos()
    {
        StartCoroutine(SetRailPiercerPosCoroutine());
    }

    IEnumerator SetRailPiercerPosCoroutine()
    {
        railPiercerRigid = this.GetComponent<Rigidbody2D>();
        Vector3 nowPos = character.transform.position;
        float fixPos = 0;
        
        while(!nightManager.isStageEnd)
        {
            nowPos = character.transform.position;

            //광선을 쏘고 있을 때
            if (!isShoot)
            {
                if (character.nowDir.x >= 0)
                    nowPos.x -= 2;
                else
                    nowPos.x += 2;
            }
            //안쏠 때
            else
            {
                if(isWatchRight)
                    nowPos.x -= 2;
                else
                    nowPos.x += 2;
            }
            nowPos.y += 2;

            this.transform.position = nowPos;
            railPiercerRigid.velocity = character.ReturnSpeed();
            yield return null;
        }
    }
}
