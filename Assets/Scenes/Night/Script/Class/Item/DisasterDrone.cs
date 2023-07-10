using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DisasterDrone : MonoBehaviour
{
    public NightManager nightManager;
    public Character character;
    LayerMask enemyLayer;
    [SerializeField]
    float detectRadius;
    [SerializeField]
    double damage;

    EnemyParent getEnemyScript;

    private void Start()
    {
        DetectEnemy();
    }

    void DetectEnemy()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        StartCoroutine(DetectEnemyCoroutine());
    }

    IEnumerator DetectEnemyCoroutine()
    {
        int layerMask = (1 << enemyLayer);
        while (!nightManager.isStageEnd)
        {
            Collider2D[] colArr = Physics2D.OverlapCircleAll(character.transform.position, detectRadius, layerMask);

            if (colArr.Length > 0)
            {
                for(int i =0; i< colArr.Length; i++)
                {
                    AttackEnemys(colArr[i]);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void AttackEnemys(Collider2D getCol)
    {
        getEnemyScript = getCol.GetComponent<EnemyParent>();
        damage = getEnemyScript.ReturnEnemyHitPointMax();

        damage /= 1;

        getEnemyScript.EnemyDamaged(damage);
    }
}
