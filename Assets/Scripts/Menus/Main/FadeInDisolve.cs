using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInDisolve : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float disolveSpeed;
    float disolveAmount;
    
    public int goToScene;

    // Start is called before the first frame update
    void Start()
    {
        disolveAmount = 0.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (disolveAmount >= 1.0f)
        {
            SceneManager.LoadScene(goToScene);
        }

        disolveAmount += disolveSpeed * Time.unscaledDeltaTime;
        if (disolveAmount > 1.0f) disolveAmount = 1.0f;
        mat.SetFloat("_DisolveAmount", disolveAmount);
    }
}
