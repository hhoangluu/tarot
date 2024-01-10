using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIPickCategory : UIBase
{
    [SerializeField] ButtonCategory buttonLove;
    [SerializeField] ButtonCategory buttonCarrer;
    [SerializeField] ButtonCategory buttonFinace;
    private void Start()
    {
        buttonLove.onClick += (e) => OnButtonClicked("love");
        buttonCarrer.onClick += (e) => OnButtonClicked("career"); ;
        buttonFinace.onClick += (e) => OnButtonClicked("finance"); ;
    }

    private void OnButtonClicked(string type)
    {
        GameManager.instance.MoveToPickCard(type);
    }
}
