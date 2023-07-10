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

    Vector3 startPos;
    Vector3 moveVector;

    void Start()
    {
        moveVector = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            startPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            Vector3 currVector = eventData.position;

            moveVector = (currVector - startPos).normalized;

            
            character.CharacterMove(moveVector);
            //character.SetHitbox();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!nightManager.isStageEnd)
        {
            character.CharacterStop(Vector3.zero);
            startPos = Vector3.zero;
            moveVector = Vector3.zero;
        }
    }
}
