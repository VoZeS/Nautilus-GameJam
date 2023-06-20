using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScreen : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float disolveSpeed;
    float disolveAmount;

    // Start is called before the first frame update
    void Start()
    {
        disolveAmount = 1.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        while (disolveAmount > 0.0f)
        {
            disolveAmount -= disolveSpeed * Time.deltaTime;
            if (disolveAmount < 0.0f) disolveAmount = 0.0f;
            mat.SetFloat("_DisolveAmount", disolveAmount);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        while (disolveAmount < 1.0f)
        {
            disolveAmount += disolveSpeed * Time.deltaTime;
            if (disolveAmount > 1.0f) disolveAmount = 1.0f;
            mat.SetFloat("_DisolveAmount", disolveAmount);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
