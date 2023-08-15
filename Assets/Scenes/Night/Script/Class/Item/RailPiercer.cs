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
    [SerializeField]
    SpriteRenderer hitBoxRenderer;
    [SerializeField]
    SpriteRenderer railPiercerImageRenderer;
    Rigidbody2D railPiercerRigid;
    [SerializeField] double attackPower = 500;
    [SerializeField] double attackSpeed;
    float hitBoxScale;
    bool isWatchRight = true;
    bool isShoot = false;

    int itemRank;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Normal" || collision.tag == "Elite")
        {
            collision.GetComponent<EnemyParent>().EnemyDamaged(attackPower);
        }
    }

    public void SetItemRank(int getRank)
    {
        itemRank = getRank;
        SetRailPiercerData();
    }

    void SetRailPiercerData()
    {
        float characterAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float characterAttackPower = (float)character.ReturnCharacterAttackPower();

        switch (itemRank)
        {
            case 0:
                attackPower = characterAttackPower * DataManager.instance.itemList.item[4].attackPowerValue;
                attackSpeed = 10 / (characterAttackSpeed * DataManager.instance.itemList.item[4].attackSpeedValue);
                break;
            case 1:
                attackPower = characterAttackPower * DataManager.instance.itemList.item[19].attackPowerValue;
                attackSpeed = 10 / (characterAttackSpeed * DataManager.instance.itemList.item[19].attackSpeedValue);
                break;
            case 2:
                attackPower = characterAttackPower * DataManager.instance.itemList.item[34].attackPowerValue;
                attackSpeed = 10 / (characterAttackSpeed * DataManager.instance.itemList.item[34].attackSpeedValue);
                break;
            case 3:
                attackPower = characterAttackPower * DataManager.instance.itemList.item[49].attackPowerValue;
                attackSpeed = 10 / (characterAttackSpeed * DataManager.instance.itemList.item[49].attackSpeedValue);
                break;
        }
    }

    public void ShootRailPiercer()
    {
        railPiercerHitBox.SetActive(false);

        //������ �ٶ󺸸� �������� ���̰� �����
        if (isWatchRight)
        {
            hitBoxScale = 2;
        }
        else
        {
            hitBoxScale = 2;
        }
        StartCoroutine(ShootRailPiercerCoroutine());
    }

    IEnumerator ShootRailPiercerCoroutine()
    {
        Vector3 railPiercerImageScale = Vector3.zero;
        
        while (!nightManager.isStageEnd)
        {
            isShoot = true;

            //��Ʈ�ڽ��� ���� ���⿡ ��� x ��ǥ�� ������
            Vector3 hitBoxPos = Vector3.zero;
            if (character.nowDir.x >= 0)
            {
                isWatchRight = true;
            }
            else
            {
                isWatchRight = false;
            }
            SetRailPiercerViewPos();

            //���� ��� �κ�
            railPiercerHitBox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            railPiercerHitBox.SetActive(false);
            isShoot = false;

            yield return new WaitForSeconds((float)attackSpeed);
        }
    }

    void SetRailPiercerViewPos()
    {
        Transform railPiercerTransform = this.transform;
        float angle;
        Vector3 watchDir = character.nowDir;

        angle = Mathf.Atan2(watchDir.y, watchDir.x) * Mathf.Rad2Deg;
        railPiercerTransform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }


    public void SetRailPiercerPos()
    {
        hitBoxRenderer = railPiercerHitBox.GetComponent<SpriteRenderer>();
        railPiercerImageRenderer = railPiercerImage.GetComponent<SpriteRenderer>();
        StartCoroutine(SetRailPiercerPosCoroutine());
    }

    IEnumerator SetRailPiercerPosCoroutine()
    {
        railPiercerRigid = this.GetComponent<Rigidbody2D>();
        Vector3 nowPos = character.transform.position;
        
        
        while(!nightManager.isStageEnd)
        {
            nowPos = character.transform.position;

            //������ �Ƚ�� ���� ��
            if (!isShoot)
            {
                if (character.nowDir.x >= 0)
                {
                    railPiercerImageRenderer.flipX = true;
                    nowPos.x -= 2;
                }
                else
                {
                    railPiercerImageRenderer.flipX = false;
                    nowPos.x += 2;
                }
            }
            //�� ��
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
