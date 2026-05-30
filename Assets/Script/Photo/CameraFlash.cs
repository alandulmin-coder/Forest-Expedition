using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour
{
    public Image flashImage;

    public IEnumerator Flash()
    {
        Color c = flashImage.color;

        c.a = 1;
        flashImage.color = c;

        yield return new WaitForSeconds(0.1f);

        c.a = 0;
        flashImage.color = c;
    }
}