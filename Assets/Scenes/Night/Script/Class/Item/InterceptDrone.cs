using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptDrone : MonoBehaviour
{
    public NightManager nightManager;
    public Character character;
    LayerMask projLayer;

    [SerializeField]
    GameObject droneArea;
    [SerializeField]
    GameObject droneAreaImage;
    [SerializeField]
    GameObject interceptDroneImage;
    [SerializeField]
    GameObject interceptDroneSearchingImage;
    [SerializeField]
    SpriteRenderer droneRenderer;

    [SerializeField]
    float droneAngle;
    [SerializeField]
    float detectRadius;

    double getCharacterAttackSpeed;
    double getCharacterAttackRange;

    Projectile getProjScript;

    private void Start()
    {
        DetectProjectile();
        StartCoroutine(DroneRotate());
    }

    IEnumerator DroneRotate()
    {
        while (!nightManager.isStageEnd)
        {
            interceptDroneImage.transform.RotateAround(character.transform.position, Vector3.back, droneAngle);
            interceptDroneImage.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

            yield return null;
        }
    }
    //void SetCentryBallWatchEnemy(Collider2D getCol)
    //{
    //    Transform enemyTransform = getCol.transform;
    //    float angle;

    //    watchDir = enemyTransform.position - centryBallImage.transform.position;

    //    angle = Mathf.Atan2(watchDir.y, watchDir.x) * Mathf.Rad2Deg;
    //    centryBallImage.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    //}

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
                    StartCoroutine(SearchIngProjEffectCoroutine());
                    InterceptProj(colArr[i]);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SearchIngProjEffectCoroutine()
    {
        float nowAngle = 0;
        float spinSpeed = 720;
        droneAreaImage.SetActive(true);

        while (nowAngle <= 360)
        {
            nowAngle += Time.deltaTime * spinSpeed;
            interceptDroneSearchingImage.transform.rotation = Quaternion.Euler(0, 0, nowAngle * (-1));

            yield return null;
        }

        droneAreaImage.SetActive(false);
    }

    void InterceptProj(Collider2D getCol)
    {
        getProjScript = getCol.GetComponent<Projectile>();
        getProjScript.SetProjPos();
    }
}
