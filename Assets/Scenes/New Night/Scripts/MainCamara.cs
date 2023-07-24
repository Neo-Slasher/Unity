using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamara : MonoBehaviour
{
    public GameObject player;


    Vector3 camaraPosition = new Vector3(0, 0, -10);
    float camaraMoveSpeed = 1.0f;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + camaraPosition, Time.deltaTime * camaraMoveSpeed);
    }
}
