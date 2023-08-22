using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    NightManager nightManager;

    [SerializeField]
    Character character;
    [SerializeField]
    GameObject joyStick;
    [SerializeField]
    GameObject handle;
    [SerializeField]
    RectTransform joyStickRecTransform;

    Vector3 startPos;
    Vector3 moveVector;

    [SerializeField]
    float joyStickRange = 10;
    void Start()
    {
        joyStickRecTransform = handle.GetComponent<RectTransform>();
        moveVector = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(1);
        if (!nightManager.isStageEnd)
        {
            startPos = eventData.position;
            joyStick.transform.position = startPos;
            joyStick.SetActive(true);
        }
        else
            joyStickRecTransform.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!nightManager.isStageEnd)
        {
            Debug.Log(2);
            Vector3 currVector = eventData.position;

            Vector3 touchPos = eventData.position - (Vector2)startPos;

            Vector3 joystickPos = joyStickRecTransform.anchoredPosition;
            Vector3 currPos = touchPos - joystickPos;

            Vector3 clampedPos = currPos.magnitude < joyStickRange ?
                currPos : currPos.normalized * joyStickRange;
            if (currPos.magnitude < joyStickRange)
                Debug.Log("in");
            else Debug.Log("Out");


            joyStickRecTransform.anchoredPosition = clampedPos;
            
            moveVector = (currVector - startPos).normalized;

            character.StartMove(moveVector);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(3);
        if (!nightManager.isStageEnd)
        {
            joyStick.SetActive(false);
            character.EndMove();
            startPos = Vector3.zero;
            moveVector = Vector3.zero;
        }
        else
            joyStickRecTransform.anchoredPosition = Vector3.zero;
    }
}
