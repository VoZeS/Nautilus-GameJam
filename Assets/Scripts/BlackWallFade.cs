using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWallFade : MonoBehaviour
{
    Material mat;
    [SerializeField] float fadeSpeed;
    [SerializeField] float initialAlpha;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, initialAlpha);
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine("FadeInCoroutine");
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeInCoroutine()
    {
        Color color = mat.color;
        float alpha = mat.color.a;
        while (alpha < 1.0f)
        {
            alpha += Time.unscaledDeltaTime * fadeSpeed;
            mat.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        mat.color = new Color(color.r, color.g, color.b, 1.0f);
    }

    IEnumerator FadeOutCoroutine()
    {
        Color color = mat.color;
        float alpha = mat.color.a;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            mat.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        mat.color = new Color(color.r, color.g, color.b, 0.0f);
    }
}
