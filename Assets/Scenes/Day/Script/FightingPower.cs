using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingPower : MonoBehaviour
{
    public int hitPoint = 20;
    public int moveSpeed = 10;
    public int attackPower = 5;
    public int attackSpeed = 10;
    public int attackRange = 10;

    public int currentCP;

    // Start is called before the first frame update
    void Start()
    {
        currentCP = (int)((hitPoint * 1.5) + (moveSpeed * 3) + (attackPower * attackSpeed * attackRange * 0.02));
        Debug.Log("currentCP "+currentCP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
