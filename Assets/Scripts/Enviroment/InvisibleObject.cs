using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    Material mat;
    [SerializeField] float fadeSpeed;

    [SerializeField] CountPlayersOnCollider playersOnCollider;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        // check if player in zone
        if (playersOnCollider.playersOnCollider > 0)
        {
            Debug.Log("PlayerOnZone");
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
}
