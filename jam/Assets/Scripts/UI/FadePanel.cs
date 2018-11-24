using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField]
    private Image panel;

    public void FadeOut(Action callback)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine(callback));
    }

    public void FadeIn(Action callback)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine(callback));
    }

    private IEnumerator FadeOutCoroutine(Action callback)
    {
        panel.color = Color.clear;
        while(panel.color.a < 0.95f)
        {
            panel.color = Color.Lerp(panel.color, Color.black, Time.deltaTime * 5);
            yield return null;
        }

        panel.color = Color.black;

        if (callback != null)
            callback.Invoke();
    }

    private IEnumerator FadeInCoroutine(Action callback)
    {
        panel.color = Color.black;
        while (panel.color.a > 0.05f)
        {
            panel.color = Color.Lerp(panel.color, Color.clear, Time.deltaTime * 5);
            yield return null;
        }

        panel.color = Color.clear;

        if (callback != null)
            callback.Invoke();

        gameObject.SetActive(false);
    }
}
