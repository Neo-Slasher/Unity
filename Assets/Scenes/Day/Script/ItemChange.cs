using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChange : MonoBehaviour
{
    public CompareItem CompareItem;
    public int Inum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNum()
    {
        CompareItem.chosen = Inum;
        CompareItem.ItemPrinting();
    }
}
