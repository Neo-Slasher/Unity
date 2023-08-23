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
            Vector3 currVector = eventData.position;

            Vector3 touchPos = eventData.position - (Vector2)startPos;

            Vector3 clampedPos = touchPos.magnitude < joyStickRange ?
                touchPos : touchPos.normalized * joyStickRange;


            joyStickRecTransform.anchoredPosition = clampedPos;
            
            moveVector = (currVector - startPos).normalized;

            character.StartMove(moveVector);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
