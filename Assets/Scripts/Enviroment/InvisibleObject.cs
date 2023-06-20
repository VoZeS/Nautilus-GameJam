using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    Material mat;
    [SerializeField] int zone;
    [SerializeField] GameObject riftFuture;
    [SerializeField] GameObject riftMedieval;
    [SerializeField] GameObject riftEgipt;
    GameObject riftCurrent;
    Material riftMat;
    [SerializeField] float riftStartAlpha;
    [SerializeField] float fadeSpeed;
    [SerializeField] float errorFadeSpeed;
    int state; // 0 --> none, 1 --> visible, 2 --> error

    [SerializeField] CountPlayersOnCollider playersOnCollider;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);

        gameObject.layer = LayerMask.NameToLayer("Invisible");
        state = 0;

        // rift
        if (zone == 0) riftCurrent = riftFuture;
        else if (zone == 1) riftCurrent = riftMedieval;
        else if (zone == 2) riftCurrent = riftEgipt;
        Material instance = new Material(Shader.Find("Shader Graphs/RiftShader"));
        Material currentMat = riftCurrent.GetComponent<Renderer>().material;
        instance.CopyPropertiesFromMaterial(currentMat);
        riftMat = riftCurrent.GetComponent<Renderer>().material = instance;
        riftStartAlpha = riftMat.GetFloat("_Alpha");
        riftCurrent.SetActive(true);
        riftCurrent.transform.localScale = new Vector3(0.3f / transform.lossyScale.x, 0.3f / transform.lossyScale.y, 0.3f / transform.lossyScale.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        if (state == 2) return;
        else if (state == 1)
        {
            state = 1;
            StopAllCoroutines();
            StartCoroutine("FadeInCoroutine");
            StartCoroutine("FadeOut");
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        else
        {
            // check if player in zone
            if (playersOnCollider.playersOnCollider > 0)
            {
                state = 2;
                StartCoroutine("ErrorCoroutine");
            }
            else
            {
                state = 1;
                StopAllCoroutines();
                StartCoroutine("FadeInCoroutine");
                StartCoroutine("FadeOut");
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
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
            riftMat.SetFloat("_Alpha", (1 - alpha) * riftStartAlpha);
            yield return null;
        }
        mat.color = new Color(color.r, color.g, color.b, 1.0f);
        riftMat.SetFloat("_Alpha", 0.0f);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeOutCoroutine()
    {
        Color color = mat.color;
        float alpha = mat.color.a;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            mat.color = new Color(color.r, color.g, color.b, alpha);
            riftMat.SetFloat("_Alpha", (1 - alpha) * riftStartAlpha);
            yield return null;
        }
        mat.color = new Color(color.r, color.g, color.b, 0.0f);
        riftMat.SetFloat("_Alpha", riftStartAlpha);
        gameObject.layer = LayerMask.NameToLayer("Invisible");
        state = 0;
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
        state = 0;
    }
}
