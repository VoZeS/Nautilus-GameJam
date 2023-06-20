using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class InvisibleObject : MonoBehaviour
{
    Material mat;
    [SerializeField] int zone;
    [SerializeField] GameObject riftFuture;
    [SerializeField] GameObject riftMedieval;
    [SerializeField] GameObject riftEgipt;
    GameObject riftCurrent;
    Material riftMat;
    float riftStartAlpha;
    [SerializeField] float fadeSpeed;
    [SerializeField] float errorFadeSpeed;
    [SerializeField] int state; // 0 --> none, 1 --> visible, 2 --> error

    [SerializeField] CountPlayersOnCollider playersOnCollider;

    // Start is called before the first frame update
    void Start()
    {
        // object
        GetComponent<Renderer>().material.SetFloat("_DisolveAmount", 0.0f);
        Material instance = new Material(Shader.Find("Shader Graphs/InvisibleObjectShader"));
        Material currentMat = GetComponent<Renderer>().material;
        instance.CopyPropertiesFromMaterial(currentMat);
        mat = GetComponent<Renderer>().material = instance;
        mat.SetInt("_Error", 0);

        gameObject.layer = LayerMask.NameToLayer("Invisible");
        state = 0;

        // rift
        if (zone == 0) riftCurrent = riftFuture;
        else if (zone == 1) riftCurrent = riftMedieval;
        else if (zone == 2) riftCurrent = riftEgipt;
        Material riftInstance = new Material(Shader.Find("Shader Graphs/RiftShader"));
        Material riftCurrentMat = riftCurrent.GetComponent<Renderer>().material;
        riftInstance.CopyPropertiesFromMaterial(riftCurrentMat);
        riftMat = riftCurrent.GetComponent<Renderer>().material = riftInstance;
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
        float disolveAmount = mat.GetFloat("_DisolveAmount");
        while (disolveAmount < 1.0f)
        {
            disolveAmount += Time.deltaTime * fadeSpeed;
            mat.SetFloat("_DisolveAmount", disolveAmount);
            riftMat.SetFloat("_Alpha", (1 - disolveAmount) * riftStartAlpha);
            yield return null;
        }
        mat.SetFloat("_DisolveAmount", 1.0f);
        riftMat.SetFloat("_Alpha", 0.0f);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeOutCoroutine()
    {
        float disolveAmount = mat.GetFloat("_DisolveAmount");
        while (disolveAmount > 0.0f)
        {
            disolveAmount -= Time.deltaTime * fadeSpeed;
            mat.SetFloat("_DisolveAmount", disolveAmount);
            riftMat.SetFloat("_Alpha", (1 - disolveAmount) * riftStartAlpha);
            yield return null;
        }
        mat.SetFloat("_DisolveAmount", 0.0f);
        riftMat.SetFloat("_Alpha", riftStartAlpha);
        gameObject.layer = LayerMask.NameToLayer("Invisible");
        state = 0;
    }

    IEnumerator ErrorCoroutine()
    {
        mat.SetInt("_Error", 1);
        float disolveAmount = mat.GetFloat("_DisolveAmount");
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                while (disolveAmount < 0.6f)
                {
                    disolveAmount += Time.deltaTime * errorFadeSpeed;
                    mat.SetFloat("_DisolveAmount", disolveAmount);
                    yield return null;
                }
            }
            else
            {
                while (disolveAmount > 0.0f)
                {
                    disolveAmount -= Time.deltaTime * errorFadeSpeed;
                    mat.SetFloat("_DisolveAmount", disolveAmount);
                    yield return null;
                }
            }
        }
        mat.SetFloat("_DisolveAmount", disolveAmount);
        mat.SetInt("_Error", 0);
        state = 0;
    }
}
