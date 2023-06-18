using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    Material mat;
    [SerializeField] float fadeSpeed;
    [SerializeField] float errorFadeSpeed;
    bool onError;

    [SerializeField] CountPlayersOnCollider playersOnCollider;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);

        gameObject.layer = LayerMask.NameToLayer("Invisible");
        onError = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        if (onError) return;
        // check if player in zone
        if (playersOnCollider.playersOnCollider > 0)
        {
            onError = true;
            StartCoroutine("ErrorCoroutine");
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine("FadeInCoroutine");
            StartCoroutine("FadeOut");
            gameObject.layer = LayerMask.NameToLayer("Outline");
        }
    }

    IEnumerator FadeInCoroutine()
    {
        Color color = mat.color;
        float alpha = mat.color.a;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            mat.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        mat.color = new Color(color.r, color.g, color.b, 1.0f);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("FadeOutCoroutine");
        gameObject.layer = LayerMask.NameToLayer("Invisible");
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

    IEnumerator ErrorCoroutine()
    {
        Color color = mat.color;
        float alpha = mat.color.a;
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                while (alpha < 0.3f)
                {
                    alpha += Time.deltaTime * errorFadeSpeed;
                    mat.color = new Color(1, 0, 0, alpha);
                    yield return null;
                }
            }
            else
            {
                while (alpha > 0.0f)
                {
                    alpha -= Time.deltaTime * errorFadeSpeed;
                    mat.color = new Color(1, 0, 0, alpha);
                    yield return null;
                }
            }
        }
        mat.color = new Color(color.r, color.g, color.b, 0.0f);
        onError = false;
    }
}
