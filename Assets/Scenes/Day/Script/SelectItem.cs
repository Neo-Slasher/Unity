using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    //아이템 슬롯 클릭시 정보 변경 

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
