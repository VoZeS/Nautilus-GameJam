using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIVE_MENU
{
    MAIN,
    SETTINGS,
    CREDITS
}

public class InitialScreenManager : MonoBehaviour
{
    [NonEditable] ACTIVE_MENU activeMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        activeMenu = ACTIVE_MENU.MAIN;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OpenMenu(ACTIVE_MENU.MAIN);
        }
    }

    public void OpenMenu(ACTIVE_MENU menuToOpen)
    {
        CloseAllMenus();
        switch (menuToOpen)
        {
            case ACTIVE_MENU.MAIN:
                mainMenu.SetActive(true);
                break;
            case ACTIVE_MENU.SETTINGS:
                settingsMenu.SetActive(true);
                break;
            case ACTIVE_MENU.CREDITS:
                creditsMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
