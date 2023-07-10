using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CentryBall : MonoBehaviour
{
    [SerializeField]
    NightManager nightManager;
    [SerializeField]
    Character character;
    [SerializeField]
    GameObject projectileObject;
    [SerializeField]
    GameObject[] projectilesPulling;
    int pullingScale = 30;
    int nowPullingIndex = 0;

    bool isShoot = false;

    LayerMask enemyLayer;
    [SerializeField]
    float detectRadius;

    private void Awake()
    {
        nightManager = GameObject.Find("NightManager").GetComponent<NightManager>();
        character = GameObject.Find("CharacterImage").GetComponent<Character>();
    }

    private void Start()
    {
        SetProjectile();

    }
    //공격 함수 들어갈 예정 + 범위는 overlap
    void SetProjectile()
    {
        projectilesPulling = new GameObject[pullingScale];
        GameObject centryBallImage = this.transform.GetChild(0).gameObject;

        //투사체 준비
        for (int i = 0; i < pullingScale; i++)
        {
            GameObject nowProj = Instantiate(projectileObject, this.transform);
            nowProj.GetComponent<Projectile>().isEnemy = false;
            nowProj.transform.SetParent(this.transform.GetChild(1));
            nowProj.transform.position = centryBallImage.transform.position;
            nowProj.SetActive(false);
            projectilesPulling[i] = nowProj;
        }
        DetectEnemy();
    }

    void ShootProjectile(Collider2D getCol)
    {
        StartCoroutine(ShootProjectileCoroutine(getCol));
    }

    IEnumerator ShootProjectileCoroutine(Collider2D getCol)
    {
        if (!isShoot)
        {
            isShoot = true;
            projectilesPulling[nowPullingIndex].transform.position = this.transform.GetChild(0).position;
            projectilesPulling[nowPullingIndex].SetActive(true);

            Vector3 moveDir = getCol.transform.position - this.transform.position;

            projectilesPulling[nowPullingIndex].GetComponent<Rigidbody2D>().velocity
                = moveDir.normalized * 5;

            yield return new WaitForSeconds(2f);

            if (nowPullingIndex < 30)
                nowPullingIndex++;
            else
                nowPullingIndex = 0;

            isShoot = false;
        }
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
                //투사체 발사
                ShootProjectile(SetShortestDistanceCol(colArr));
            }

            yield return new WaitForSeconds(1f);
        }
    }

    Collider2D SetShortestDistanceCol(Collider2D[] getColArr)
    {
        Collider2D shortestDistanceCol = getColArr[0]; 
        float shortestDistance = (getColArr[0].transform.position - character.transform.position).magnitude;
        float nowDistance;

        for(int i =1; i< getColArr.Length; i++)
        {
            nowDistance = (getColArr[i].transform.position - character.transform.position).magnitude;
            if ( nowDistance < shortestDistance)
            {
                shortestDistanceCol = getColArr[i];
                shortestDistance = nowDistance;
            }
        }
        
        return shortestDistanceCol;

    }
}
