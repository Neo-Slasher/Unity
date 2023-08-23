using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    NightManager nightManager;

    [SerializeField]
    RectTransform joystick;
    [SerializeField]
    RectTransform rectTransform;    //조이스틱 기본 위치

    [SerializeField, Range(10f, 150f)]
    float joystickRange;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            Vector3 touchPos = eventData.position;
            Vector3 joystickPos = rectTransform.anchoredPosition;
            Vector3 currPos = touchPos - joystickPos;

            Vector3 clampedPos = currPos.magnitude < joystickRange ?
                currPos : currPos.normalized * joystickRange;

            joystick.anchoredPosition = clampedPos;
        }
        else
            joystick.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            Vector3 touchPos = eventData.position;
            Vector3 joystickPos = rectTransform.anchoredPosition;
            Vector3 currPos = touchPos - joystickPos;

            Vector3 clampedPos = currPos.magnitude < joystickRange ?
                currPos : currPos.normalized * joystickRange;

            joystick.anchoredPosition = clampedPos;
        }
        else
            joystick.anchoredPosition = Vector3.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            joystick.anchoredPosition = Vector3.zero;
        }
        else
            joystick.anchoredPosition = Vector3.zero;
    }
}
