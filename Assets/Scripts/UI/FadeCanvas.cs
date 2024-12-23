using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System.Security.Cryptography;

public class FadeCanvas : MonoBehaviour
{
    public float fadeInDuration = 1.25f;
    public float stableDuration = 1f;
    public float fadeOutDuration = 1.25f;
    [SerializeField] CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
    }
    public void ShowFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void ShowFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeInDuration);

            canvasGroup.alpha = alpha;

            time += Time.unscaledDeltaTime;

            yield return null;
        }
        if (time > fadeInDuration) canvasGroup.alpha = 1f;
    }
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeInDuration + stableDuration);

        float time = 0f;

        while (time < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeOutDuration);

            canvasGroup.alpha = alpha;

            time += Time.unscaledDeltaTime;
            
            yield return null;
        }
        if (time > fadeOutDuration) canvasGroup.alpha = 0f;
    }
}
