using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIntroLogic : MonoBehaviour
{
    public Image image;
    public Image imageFinalPanel;

    private float timer;

    float alpha1 = 0;
    float alpha2 = 0;

    private void Start()
    {
        timer = 0.0f;
        alpha1 = 0.0f;
        alpha2 = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= 3.0f && alpha2 >= 0.0f)
            alpha2 -= 0.003f;

        if (timer >= 15.0f && alpha1 <= 1.0f)
            alpha1 += 0.001f;

        if (timer >= 23.0f && alpha2 <= 1.0f)
            alpha2 += 0.005f;

        if(timer >= 25.0f)
            SceneManager.LoadScene(2);

        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha1);

        imageFinalPanel.color = new Color(imageFinalPanel.color.r, imageFinalPanel.color.g, imageFinalPanel.color.b, alpha2);

    }
}