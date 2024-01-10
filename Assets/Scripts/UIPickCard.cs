using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPickCard : UIBase
{
    [SerializeField] UICardPickingButton cardPrefab;
    [SerializeField] Transform center;
    [SerializeField] Button turnLeftButton;
    [SerializeField] Button turnRightButton;
    [SerializeField] Button resetButton;


    public int numberOfCards = 24;
    public float radius = 5f;
    public float angleSpeed = 5f;
    List<UICardPickingButton> cards = new List<UICardPickingButton>();
    bool isRotating;
    void Start()
    {
        ArrangeCards();
        turnLeftButton.onClick.AddListener(OnTurnLeftButtonClicked);
        turnRightButton.onClick.AddListener(OnTurnRightButtonClicked);
        resetButton.onClick.AddListener(() => GameManager.instance.Reset(this));
        graphics.Clear();
        GetAllGraphics(transform);
    }
    private void OnEnable()
    {
        foreach (var card in cards)
        {
            card.LowLight();
        }
    }

    private void OnTurnRightButtonClicked()
    {
        center.DORotate(center.eulerAngles + new Vector3(0, 0, 30), 1f);
    }

    private void OnTurnLeftButtonClicked()
    {
        center.DORotate(center.eulerAngles - new Vector3(0, 0, 30), 1f);
    }

    void ArrangeCards()
    {
        Vector3 centerPos = center.position; // Trung tâm của vòng tròn

        for (int i = numberOfCards - 1; i >= 0; i--)
        {
            float angle = i * (360f / numberOfCards) + 90;
            float radians = angle * Mathf.Deg2Rad;

            float x = centerPos.x - radius * Mathf.Cos(radians);
            float y = centerPos.y - radius * Mathf.Sin(radians);

            Vector3 cardPosition = new Vector3(x, y, 0f);
            Quaternion cardRotation = Quaternion.LookRotation(Vector3.forward, cardPosition - centerPos);

            UICardPickingButton card = Instantiate(cardPrefab, cardPosition, cardRotation, center);
            card.gameObject.SetActive(true);
            // card.transform.eulerAngles = new Vector3(0, 0, card.transform.eulerAngles.z + 10);
            cards.Add(card);
        }
    }

    private void Update()
    {
        foreach (var card in cards)
        {
            card.name = card.transform.eulerAngles.z.ToString();
            if (card.transform.eulerAngles.z > 165 && card.transform.eulerAngles.z < 180)
            {
                card.transform.SetAsFirstSibling();
            }
            else if (card.transform.eulerAngles.z >= 180 && card.transform.eulerAngles.z < 195)
            {
                card.transform.SetAsLastSibling();
            }
        }
    }
}
