using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotoFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.3f;

    public IEnumerator FadeIn()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0, 1, time / fadeDuration);

            fadeImage.color = c;

            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(1, 0, time / fadeDuration);

            fadeImage.color = c;

            yield return null;
        }
    }
}