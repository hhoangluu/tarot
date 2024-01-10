using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCategory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector2 startPos;
    private Tween tween;
    public System.Action<PointerEventData> onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
       onClick?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tween != null && tween.active)
        {
            tween.Kill();
            tween = null;
        }
        tween = transform.DOMoveY(startPos.y + 30, 0.5f);
        Debug.Log("startPos.y + 5" + (startPos.y +20));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tween != null && tween.active)
        {
            tween.Kill();
            tween = null;
        }
        tween = transform.DOMoveY(startPos.y, 0.5f);
        Debug.Log("startPos.y " + startPos.y);

    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }
}