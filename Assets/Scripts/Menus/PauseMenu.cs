using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    // variables
    bool fullscreen = true;
    bool vsync = true;
    int controllerType1 = 2;
    int controllerType2 = 2;

    // ui elements
    public Image imageFullscreen;
    public Image imageVsync;
    public TextMeshProUGUI textControllerType1;
    public TextMeshProUGUI textControllerType2;
    public GameObject buttonControllerType1;
    public GameObject buttonControllerType2;
    bool noHorizontalInputLastFrame;

    public float errorFadeSpeed;
    public Image errorPanel;
    public GameObject errorWASD;
    public GameObject errorARROWS;
    TextMeshProUGUI errorText;
    bool onError;

    // audio
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        CalculateFullscreen();
        CalculateVsync();
        CalculateController(true);
        CalculateController(false);

        noHorizontalInputLastFrame = true;
        onError = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (controllerType1 == controllerType2 && controllerType1 != 2)
            {
                if (controllerType1 == 0) errorText = errorWASD.GetComponent<TextMeshProUGUI>();
                else if (controllerType1 == 1) errorText = errorARROWS.GetComponent<TextMeshProUGUI>();
                if (!onError) StartCoroutine("ErrorCoroutine");
            }
            else
            {
                gameObject.SetActive(false);
                if (onError)
                {
                    errorPanel.color = new Color(errorPanel.color.r, errorPanel.color.g, errorPanel.color.b, 0.0f);
                    errorText.color = new Color(errorText.color.r, errorText.color.g, errorText.color.b, 0.0f);
                    onError = false;
                }
                ManagePause.instance.PauseGame(false);
            }
        }

        float input = Input.GetAxisRaw("Horizontal");

        if (EventSystem.current.currentSelectedGameObject == buttonControllerType1)
        {
            if (noHorizontalInputLastFrame)
            {
                if (input > 0) IncreaseController(true);
                else if (input < 0) DecreaseController(true);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject == buttonControllerType2)
        {
            if (noHorizontalInputLastFrame)
            {
                if (input > 0) IncreaseController(false);
                else if (input < 0) DecreaseController(false);
            }
        }

        if (input == 0) noHorizontalInputLastFrame = true;
        else noHorizontalInputLastFrame = false;
    }

    public void ToggleFullscreen()
    {
        fullscreen = !fullscreen;
        CalculateFullscreen();
    }

    void CalculateFullscreen()
    {
        Screen.fullScreen = fullscreen;

        if (fullscreen)
        {
            Screen.SetResolution(1920, 1080, true);
            imageFullscreen.enabled = true;
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
            imageFullscreen.enabled = false;
        }
    }

    public void ToggleVsync()
    {
        vsync = !vsync;
        CalculateVsync();
    }

    void CalculateVsync()
    {
        if (vsync)
        {
            QualitySettings.vSyncCount = 1;
            imageVsync.enabled = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            imageVsync.enabled = false;
        }
    }

    public void DecreaseController(bool player1)
    {
        if (player1)
        {
            controllerType1--;
            if (controllerType1 < 0) controllerType1 = 2;
        }
        else
        {
            controllerType2--;
            if (controllerType2 < 0) controllerType2 = 2;
        }
        CalculateController(player1);
    }

    public void IncreaseController(bool player1)
    {
        if (player1)
        {
            controllerType1++;
            if (controllerType1 > 2) controllerType1 = 0;
        }
        else
        {
            controllerType2++;
            if (controllerType2 > 2) controllerType2 = 0;
        }
        CalculateController(player1);
    }

    void CalculateController(bool player1)
    {
        if (player1)
        {
            SettingsManager.instance.controllerType1 = controllerType1;
            switch (controllerType1)
            {
                case 0:
                    textControllerType1.text = "Keyboard WASD";
                    break;
                case 1:
                    textControllerType1.text = "Keyboard ARROWS";
                    break;
                case 2:
                    textControllerType1.text = "Controller";
                    break;
            }
        }
        else
        {
            SettingsManager.instance.controllerType2 = controllerType2;
            switch (controllerType2)
            {
                case 0:
                    textControllerType2.text = "Keyboard WASD";
                    break;
                case 1:
                    textControllerType2.text = "Keyboard ARROWS";
                    break;
                case 2:
                    textControllerType2.text = "Controller";
                    break;
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume * 10.0f);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume * 10.0f);
    }

    public void SetFxVolume(float volume)
    {
        audioMixer.SetFloat("FxVolume", volume * 10.0f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator ErrorCoroutine()
    {
        onError = true;
        Color errorPanelColor = errorPanel.color;
        Color errorTextColor = errorText.color;
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += errorFadeSpeed * Time.deltaTime;
            errorPanel.color = new Color(errorPanelColor.r, errorPanelColor.g, errorPanelColor.b, alpha);
            errorText.color = new Color(errorTextColor.r, errorTextColor.g, errorTextColor.b, alpha);
            yield return null;
        }
        alpha = 1.0f;
        errorPanel.color = new Color(errorPanelColor.r, errorPanelColor.g, errorPanelColor.b, alpha);
        errorText.color = new Color(errorTextColor.r, errorTextColor.g, errorTextColor.b, alpha);
        yield return new WaitForSeconds(1.0f);
        while (alpha > 0.0f)
        {
            alpha -= errorFadeSpeed * Time.deltaTime;
            errorPanel.color = new Color(errorPanelColor.r, errorPanelColor.g, errorPanelColor.b, alpha);
            errorText.color = new Color(errorTextColor.r, errorTextColor.g, errorTextColor.b, alpha);
            yield return null;
        }
        alpha = 0.0f;
        errorPanel.color = new Color(errorPanelColor.r, errorPanelColor.g, errorPanelColor.b, alpha);
        errorText.color = new Color(errorTextColor.r, errorTextColor.g, errorTextColor.b, alpha);
        onError = false;
    }
}
