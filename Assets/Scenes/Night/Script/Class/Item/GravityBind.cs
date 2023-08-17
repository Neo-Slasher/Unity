using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBind : MonoBehaviour
{
    public NightManager nightManager;
    public Character character;
    LayerMask enemyLayer;
    [SerializeField]
    float detectScale;
    [SerializeField]
    double getEnemySpeed;
    [SerializeField]
    float spinSpeed;

    [SerializeField]
    float slowRate;

    EnemyParent getEnemyScript;

    int itemRank;

    private void Start()
    {
        StartCoroutine(SpinGravityBindCoroutine());
    }

    private void Update()
    {
        this.transform.localPosition = character.transform.position;
    }

    public void SetItemRank(int getRank)
    {
        itemRank = getRank;
        SetGravityBindData();
    }

    void SetGravityBindData()
    {
        float characterAttackSpeed = (float)character.ReturnCharacterAttackSpeed();
        float characterAttackRange = (float)character.ReturnCharacterAttackRange();

        switch (itemRank)
        {
            case 0:
                detectScale = characterAttackRange * 0.15f * (float)DataManager.instance.itemList.item[10].attackRangeValue;
                slowRate = characterAttackSpeed * (float)DataManager.instance.itemList.item[10].attackSpeedValue * 0.01f;
                Debug.Log(slowRate);
                break;
            case 1:
                detectScale = characterAttackRange * 0.15f * (float)DataManager.instance.itemList.item[25].attackRangeValue;
                slowRate = characterAttackSpeed * (float)DataManager.instance.itemList.item[25].attackSpeedValue * 0.01f;
                break;
            case 2:
                detectScale = characterAttackRange * 0.15f * (float)DataManager.instance.itemList.item[40].attackRangeValue;
                slowRate = characterAttackSpeed * (float)DataManager.instance.itemList.item[40].attackSpeedValue * 0.01f;
                break;
            case 3:
                detectScale = characterAttackRange * 0.15f * (float)DataManager.instance.itemList.item[55].attackRangeValue;
                slowRate = characterAttackSpeed * (float)DataManager.instance.itemList.item[55].attackSpeedValue * 0.01f;
                break;
        }
        detectScale /= 3;   //�̹��� �⺻ �ȼ��� 300px�� 100px�� �����ְ� ������ �����ϱ� ���� �־���.
        detectScale += 1.8f;    //�⺻ �� 180px �߰�
        this.transform.localScale = new Vector3(detectScale, detectScale, detectScale);
    }

    private void OnTriggerEnter2D(Collider2D getCol)
    {
        if(getCol.tag == "Normal" || getCol.tag == "Elite")
        {
            SlowEnemy(getCol);
        }
    }

    private void OnTriggerExit2D(Collider2D getCol)
    {
        if (getCol.tag == "Normal" || getCol.tag == "Elite")
        {
            ExitSlowEnemy(getCol);
        }
    }

    void SlowEnemy(Collider2D getCol)
    {
        getEnemyScript = getCol.GetComponent<EnemyParent>();
        getEnemySpeed = getEnemyScript.ReturnEnemyMoveSpeed();

        getEnemyScript.isSlow = true;
        getEnemyScript.SetEnemyMoveSpeed(getEnemySpeed * (1 - slowRate));
    }

    void ExitSlowEnemy(Collider2D getCol)
    {
        getEnemyScript = getCol.GetComponent<EnemyParent>();

        getEnemySpeed = getEnemyScript.ReturnEnemyMoveSpeed();

        getEnemyScript.isSlow = false;
        getEnemyScript.SetEnemyMoveSpeed(getEnemySpeed / (1 - slowRate));
    }

    IEnumerator SpinGravityBindCoroutine()
    {
        float nowAngle = 0;

        while(!nightManager.isStageEnd)
        {
            nowAngle += Time.deltaTime * spinSpeed;

            if (nowAngle >=360)
            {
                nowAngle -= 360;
            }

            this.transform.rotation = Quaternion.Euler(0, 0, nowAngle * -1);

            yield return null;
        }
    }
}
