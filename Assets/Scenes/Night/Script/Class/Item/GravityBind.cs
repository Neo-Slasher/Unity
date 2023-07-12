using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBind : MonoBehaviour
{
    public NightManager nightManager;
    public Character character;
    LayerMask enemyLayer;
    [SerializeField]
    float detectRadius;
    [SerializeField]
    double getEnemySpeed;

    EnemyParent getEnemyScript;

    private void Update()
    {
        this.transform.localPosition = character.transform.position;
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
        getEnemyScript.SetEnemyMoveSpeed(getEnemySpeed * 0.5f);
    }

    void ExitSlowEnemy(Collider2D getCol)
    {
        getEnemyScript = getCol.GetComponent<EnemyParent>();

        getEnemySpeed = getEnemyScript.ReturnEnemyMoveSpeed();

        getEnemyScript.isSlow = false;
        getEnemyScript.SetEnemyMoveSpeed(getEnemySpeed * 2);
    }
}
