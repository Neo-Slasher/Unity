using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptDrone : MonoBehaviour
{
    public NightManager nightManager;
    public Character character;
    LayerMask projLayer;
    [SerializeField]
    float detectRadius;

    double getCharacterAttackSpeed;
    double getCharacterAttackRange;

    Projectile getProjScript;

    private void Start()
    {
        DetectProjectile();
    }

    public void SetInterceptDrone(double getAttackSpeed, double getAttackRange)
    {
        getCharacterAttackRange = getAttackRange;
        getCharacterAttackSpeed = getAttackSpeed;
    }

    void DetectProjectile()
    {
        projLayer = LayerMask.NameToLayer("Projectile");
        StartCoroutine(DetectProjectileCoroutine());
    }

    IEnumerator DetectProjectileCoroutine()
    {
        int layerMask = (1 << projLayer);
        while (!nightManager.isStageEnd)
        {
            Collider2D[] colArr = Physics2D.OverlapCircleAll(character.transform.position, detectRadius, layerMask);

            if (colArr.Length > 0)
            {
                for (int i = 0; i < colArr.Length; i++)
                {
                    InterceptProj(colArr[i]);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void InterceptProj(Collider2D getCol)
    {
        getProjScript = getCol.GetComponent<Projectile>();
        getProjScript.SetProjPos();
    }
}
