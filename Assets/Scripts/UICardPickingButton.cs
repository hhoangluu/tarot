using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICardPickingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public void HightLight()
    {
        cardTransform.DOLocalMove(originTransform.localPosition + Vector3.up * 50, 0.5f);
        cardHightLight.gameObject.SetActive(true);
    }
    public void LowLight()
    {
        cardTransform.DOLocalMove(originTransform.localPosition, 0.5f);
        cardHightLight.gameObject.SetActive(false);
    }

    [SerializeField] Transform originTransform;
    [SerializeField] Transform cardTransform;
    [SerializeField] Transform cardHightLight;


    public void OnPointerEnter(PointerEventData eventData)
    {
        HightLight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LowLight();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.ShowCardInfo();
    }
}
