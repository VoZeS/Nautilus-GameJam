using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutDisolve : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float disolveSpeed;
    float disolveAmount;

    // Start is called before the first frame update
    void Start()
    {
        disolveAmount = 1.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (disolveAmount <= 0.0f)
        {
            gameObject.SetActive(false);
        }

        disolveAmount -= disolveSpeed * Time.deltaTime;
        if (disolveAmount < 0.0f) disolveAmount = 0.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }
}
