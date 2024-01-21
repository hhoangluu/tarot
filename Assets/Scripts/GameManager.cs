using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CardInfo
{
    public int id;
    public string name;
    public string predictions;
}
[System.Serializable]
public class Platform
{
    public GameObject parent;
    public UIStart uiStart;
    public UIPickCategory uiPickCategory;
    public UIPickCard uiPickCard;
    public UICardInfo uICardInfo;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] Platform pc;
    [SerializeField] Platform mobile;

    private Platform currentPlatform;
    string endpoint = "https://widget-api.gamemondi.co/api/v1/tarots/";
    
    private CardInfo cardInfo;
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
       // return true;
#if !UNITY_EDITOR && UNITY_WEBGL
        return IsMobile();
#endif
        return false;
    }
    private void Awake()
    {
        instance = this;
        if (isMobile())
        currentPlatform = mobile;
        else 
            currentPlatform = pc;
        currentPlatform.parent.SetActive(true);
    }

    public void MoveToPickCategory()
    {
        StartCoroutine(CR_MoveToPickCategory());
        IEnumerator CR_MoveToPickCategory()
        {
            yield return currentPlatform.uiStart.Hide();
            yield return currentPlatform.uiPickCategory.Show();
        }
    }

    public void MoveToPickCard(string type)
    {
        StartCoroutine(CR_MoveToPickCard());
        IEnumerator CR_MoveToPickCard()
        {
            yield return currentPlatform.uiPickCategory.Hide();
            yield return currentPlatform.uiPickCard.Show();
        }
        int id = Random.Range(1, 79);
        StartCoroutine(GetCardData());

        IEnumerator GetCardData()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{endpoint}{id}?type={type}"))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();


                if (webRequest.isNetworkError)
                {
                    Debug.LogError("Network Error");
                }
                else
                {

                    Debug.Log($"{endpoint}{id}?{type}");

                    Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                    cardInfo = JsonUtility.FromJson<CardInfo>(webRequest.downloadHandler.text);
                }
            }
        }
    }

    public void ShowCardInfo()
    {
        StartCoroutine(CR_ShowCardInfo());
        IEnumerator CR_ShowCardInfo()
        {
            yield return currentPlatform.uiPickCard.Hide();
            while (cardInfo == null)
                yield return null;
            currentPlatform.uICardInfo.Init(cardInfo);
            yield return currentPlatform.uICardInfo.Show();
        }
    }

    internal void Reset(UIBase from)
    {
        StartCoroutine(CR_Reset());
        IEnumerator CR_Reset()
        {
            yield return from.Hide();
            yield return currentPlatform.uiStart.Show();
        }
    }
}
