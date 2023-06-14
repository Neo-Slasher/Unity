using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : EnemyParent
{
    [SerializeField]
    GameObject projectileObject;
    [SerializeField]
    GameObject[] projectilesPulling;
    int pullingScale = 10;
    int nowPullingIndex = 0;

    bool isShoot = false;

    LayerMask characterLayer;
    [SerializeField]
    float detectRadius;

    private void Start()
    {
        base.Start();
        SetProjectile();

    }
    //공격 함수 들어갈 예정 + 범위는 overlap
    void SetProjectile()
    {
        if(enemyTrashData.monProj)
        {
            projectilesPulling = new GameObject[pullingScale];

            //투사체 준비
            for(int i =0; i< pullingScale; i++)
            {
                GameObject nowProj = Instantiate(projectileObject, this.transform);
                nowProj.transform.SetParent(this.transform);
                nowProj.transform.position = this.transform.position;
                nowProj.SetActive(false);
                projectilesPulling[i] = nowProj;
            }    
            DetectCharacter();
        }
    }

    void ShootProjectile()
    {
        StartCoroutine(ShootProjectileCoroutine());
    }

    IEnumerator ShootProjectileCoroutine()
    {
        if (!isShoot)
        {
            isShoot = true;
            projectilesPulling[nowPullingIndex].SetActive(true);

            projectilesPulling[nowPullingIndex].GetComponent<Rigidbody2D>().velocity
                = moveDir.normalized * SetMoveSpeed(enemyTrashData.moveSpeed * 2);

            yield return new WaitForSeconds(2f);

            if (nowPullingIndex < 10)
                nowPullingIndex++;
            else
                nowPullingIndex = 0;

            isShoot = false;
        }
    }

    void DetectCharacter()
    {
        characterLayer = LayerMask.NameToLayer("Character");
        StartCoroutine(DetectCharacterCoroutine());
    }

    IEnumerator DetectCharacterCoroutine()
    {
        int layerMask = (1 << characterLayer);
        while (!isStageEnd)
        {
            Collider2D collider = Physics2D.OverlapCircle(this.transform.position, detectRadius, layerMask);
            
            if(collider != null)
            {
                Debug.Log(collider.name);
                //투사체 발사
                ShootProjectile();
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
