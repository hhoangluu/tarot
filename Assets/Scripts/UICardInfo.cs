using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardInfo : UIBase
{
    [SerializeField] Button resetButton;
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] Image cardImage;

    internal void Init(CardInfo cardInfo)
    {
        cardName.text = cardInfo.name;
        cardDescription.text = cardInfo.predictions;
        cardImage.sprite = Resources.Load<Sprite>($"Cards/{cardInfo.name.ToLower()}");
    }

    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(()=> GameManager.instance.Reset(this));

    }

    
}
