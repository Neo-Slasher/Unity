using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    // Start is called before the first frame update
    public CompareItem CompareItem;
    public ItemSlot ItemSlot;
    public GameObject back;

    public void purchase()
    {
        CompareItem.Purchase();
    }
    public void cancel()
    {
        CompareItem.Cancel();
    }
}
