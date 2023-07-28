using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField]
    GameObject sparkImage;      //발사 이미지
    [SerializeField]
    GameObject centryBallImage;
    int pullingScale = 30;
    int nowPullingIndex = 0;

    Vector3 watchDir = Vector3.zero;
    bool isShoot = false;
    bool isStop = false;

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

            StartCoroutine(CentryBallShootAnimation(getCol));
            StartCoroutine(StopCentryBallCoroutine());

            isShoot = true;
            projectilesPulling[nowPullingIndex].transform.position = this.transform.GetChild(0).position;
            projectilesPulling[nowPullingIndex].SetActive(true);
            float angle = Mathf.Atan2(watchDir.y, watchDir.x) * Mathf.Rad2Deg;
            projectilesPulling[nowPullingIndex].transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

            Vector3 moveDir = getCol.transform.position - centryBallImage.transform.position;
            
            projectilesPulling[nowPullingIndex].GetComponent<Rigidbody2D>().velocity
                = moveDir.normalized * 10;

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
        Collider2D shortestCol = null;
        while (!nightManager.isStageEnd)
        { 
            Collider2D[] colArr = Physics2D.OverlapCircleAll(character.transform.position, detectRadius, layerMask);
            
            if (colArr.Length > 0)
            {
                //투사체 발사
                shortestCol = SetShortestDistanceCol(colArr);
                ShootProjectile(shortestCol);
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

    IEnumerator CentryBallShootAnimation(Collider2D getCol)
    {
        SetCentryBallWatchEnemy(getCol);
        sparkImage.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        sparkImage.SetActive(false);
    }

    //쏠때 적 바라보는 함수
    void SetCentryBallWatchEnemy(Collider2D getCol)
    {
        Transform enemyTransform = getCol.transform;
        float angle;

        watchDir = enemyTransform.position - centryBallImage.transform.position;

        angle = Mathf.Atan2(watchDir.y, watchDir.x) * Mathf.Rad2Deg;
        centryBallImage.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }

    public bool StopCentryBall()
    {
        if (isStop)
            return false;
        else
            return false;
    }

    IEnumerator StopCentryBallCoroutine()
    {
        isStop = true;
        yield return new WaitForSeconds(0.2f);
        isStop = false;
    }
}
