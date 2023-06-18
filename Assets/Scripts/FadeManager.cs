using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    [SerializeField] int fadePhase; // 0 --> no fade, 1 --> fade in, 2 --> fade out

    [SerializeField] Material mat;
    [SerializeField] float disolveSpeed;
    float disolveAmount;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fadePhase == 2) disolveAmount = 1.0f;
        else disolveAmount = 0.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadePhase == 0) return;
        else if (fadePhase == 1)
        {
            disolveAmount += disolveSpeed * Time.deltaTime;
            if (disolveAmount >= 1.0f)
            {
                disolveAmount = 1.0f;
                fadePhase = 0;
            }
        }
        else
        {
            disolveAmount -= disolveSpeed * Time.deltaTime;
            if (disolveAmount <= 0.0f)
            {
                disolveAmount = 0.0f;
                fadePhase = 0;
            }
        }
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }

    public void FadeIn()
    {
        fadePhase = 1;
    }

    public void FadeOut()
    {
        fadePhase = 2;
    }
}
