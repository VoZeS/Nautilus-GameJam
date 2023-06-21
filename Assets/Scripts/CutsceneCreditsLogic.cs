using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CutsceneCreditsLogic : MonoBehaviour
{
    [Header("Credits")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI madeBy;
    public TextMeshProUGUI names;
    public TextMeshProUGUI thanks;

    [Header("HUD")]
    public TextMeshProUGUI years;
    public Image guitar;
    public Image micro;
    public Image panel;

    private float alphaOn;
    private float alphaOff;
    private float alphaYearPanelOff;

    private float timer;

    [Header("Fade")]
    public GameObject fadeIn;

    [Header("Audio")]
    public AudioSource sourceFinalMusic;
    private bool audioOn;

    // Start is called before the first frame update
    void Start()
    {
        alphaOn = 0.0f;
        alphaOff = 1.0f;
        alphaYearPanelOff = 0.5f;
        audioOn = false;

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.cutsceneOn)
        {
            if (alphaOn <= 1.0f)
                alphaOn += 0.002f;

            if (alphaOff >= 0.0f)
                alphaOff -= 0.01f;

            if (alphaYearPanelOff >= 0.0f)
                alphaYearPanelOff -= 0.01f;

            timer += Time.deltaTime;

            if(timer >= 16.0f)
                fadeIn.SetActive(true);

            if (!audioOn)
            {
                sourceFinalMusic.Play();
                audioOn = true;
            }
        }

        title.color = new Color(title.color.r, title.color.g, title.color.b, alphaOn);
        madeBy.color = new Color(madeBy.color.r, madeBy.color.g, madeBy.color.b, alphaOn);
        names.color = new Color(names.color.r, names.color.g, names.color.b, alphaOn);
        thanks.color = new Color(thanks.color.r, thanks.color.g, thanks.color.b, alphaOn);

        years.color = new Color(years.color.r, years.color.g, years.color.b, alphaOff);
        guitar.color = new Color(guitar.color.r, guitar.color.g, guitar.color.b, alphaOff);
        micro.color = new Color(micro.color.r, micro.color.g, micro.color.b, alphaOff);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alphaYearPanelOff);
    }
}
