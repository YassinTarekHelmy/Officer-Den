using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition Instance { get; private set; }
    public Image image;
    public float fadeDuration = 1f;
    
    [SerializeField] private Canvas _screenCanvas;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeIn(Action OnFadeAction)
    {
        StartCoroutine(FadeCoroutine(1, 0, OnFadeAction, () => _screenCanvas.gameObject.SetActive(false)));
    }

    public void FadeOut(Action OnFadeAction)
    {
        _screenCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeCoroutine(0, 1, OnFadeAction));
    }

    IEnumerator FadeCoroutine(float startAlpha, float endAlpha, Action OnFadeAction, Action OnFadeEnd = null)
    {
        float timer = 0;
        while (timer < fadeDuration) {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            SetAlpha(alpha);
            yield return null;
            timer += Time.deltaTime;
        }
        OnFadeAction?.Invoke();
        OnFadeEnd?.Invoke();
    }

    private void SetAlpha(float alpha) {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
