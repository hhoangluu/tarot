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
    private CardInfo cardInfo;

    private void OnEnable()
    {
        GameManager.onLanguageChanged += OnLanguageChanged;
    }
    private void OnDisable()
    {
        GameManager.onLanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(GameManager.Language language)
    {
        cardDescription.text = GameManager.language == GameManager.Language.English ? cardInfo.predictions : cardInfo.predictions_vi;
    }

    internal void Init(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        cardName.text = cardInfo.name;
        cardDescription.text = GameManager.language == GameManager.Language.English ? cardInfo.predictions : cardInfo.predictions_vi;
        cardImage.sprite = Resources.Load<Sprite>($"Cards/{cardInfo.name.ToLower()}");
    }

    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(() => GameManager.instance.Reset(this));
    }

}
