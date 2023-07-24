using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject normal1;


    void Start() {
        StartCoroutine(SpawnNormal());
    }

    IEnumerator SpawnNormal() {
        while (true) {
            yield return new WaitForSeconds(3.0f);
            Instantiate(normal1);
        }
    }

}
