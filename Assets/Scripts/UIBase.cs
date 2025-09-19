using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    protected List<Graphic> graphics = new List<Graphic>();
    // Start is called before the first frame update
    void Awake()
    {
        GetAllGraphics(transform);
    }

    protected void GetAllGraphics(Transform parent)
    {
        var childGraphics = parent.GetComponentsInChildren<Graphic>(true);
        graphics.AddRange(childGraphics);
        if (parent.childCount != 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GetAllGraphics(parent.GetChild(i));
            }
        }
    }

    public virtual IEnumerator Show()
    {
        Tween tween = null;
        gameObject.SetActive(true);
        foreach (var graphic in graphics)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);

            tween = graphic.DOFade(1, 1);
        }
        yield return tween.WaitForCompletion();
    }

    public virtual IEnumerator Hide()
    {
        Tween tween = null;
        foreach (var graphic in graphics)
        {
            tween = graphic.DOFade(0, 1);
        }
        yield return tween.WaitForCompletion();
        gameObject.SetActive(false);
    }
}
