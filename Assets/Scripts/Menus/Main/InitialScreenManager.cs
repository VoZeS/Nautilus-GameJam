using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ACTIVE_MENU
{
    MAIN,
    SETTINGS,
    CREDITS
}

public class InitialScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject mainMenuFirstButton;
    public GameObject settingsMenuFirstButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu(ACTIVE_MENU menuToOpen)
    {
        CloseAllMenus();
        switch (menuToOpen)
        {
            case ACTIVE_MENU.MAIN:
                mainMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                break;
            case ACTIVE_MENU.SETTINGS:
                settingsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(settingsMenuFirstButton);
                break;
            case ACTIVE_MENU.CREDITS:
                creditsMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
