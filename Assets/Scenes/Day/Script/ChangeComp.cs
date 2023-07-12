using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeComp : MonoBehaviour
{
    // Start is called before the first frame update
    public Image change_img;
    Image thisimg;

    void Start()
    {
        thisimg = GetComponent<Image>();
    }

    // Update is called once per frame
    public void ChangeImage()
    {
        change_img.sprite = thisimg.sprite;
    }
}
