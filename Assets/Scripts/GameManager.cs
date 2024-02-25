using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardInfo
{
    public int id;
    public string name;
    public string predictions;
    public string predictions_vi;

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
    public enum Language
    {
        English,
        Vietnamese
    }
    public static GameManager instance { get; private set; }
    private static Language _language;
    public static Language language
    {
        get => _language;
        private set
        {
            if (value != language)
            {
                _language = value;
                onLanguageChanged?.Invoke(value);
            }
        }
    }
    public static event System.Action<Language> onLanguageChanged;
    [SerializeField] Platform pc;
    [SerializeField] Platform mobile;

    private Platform currentPlatform;
    string endpoint = "https://widget-api.gamemondi.co/api/v1/tarots/";

    private CardInfo cardInfo;
    [DllImport("__Internal")]
    private static extern bool IsMobile();
    public bool cheatMB;
    public bool isMobile()
    {
#if UNITY_EDITOR
        return cheatMB;
#endif
        //    return true;
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
    public Dropdown dropdown;

    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }
    static void LocaleSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        if (index == 0)
        {
            language = Language.English;
        }
        else {
            language = Language.Vietnamese;
        }
    }
}
