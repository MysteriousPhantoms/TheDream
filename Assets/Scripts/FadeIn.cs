using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;   // Assign FadePanel Image here
    public float fadeTime = 1f;

    private void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / fadeTime); // Alpha 1 → 0
            fadeImage.color = c;
            yield return null;
        }
    }
}