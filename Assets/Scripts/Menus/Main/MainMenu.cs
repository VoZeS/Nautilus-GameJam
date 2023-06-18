using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InitialScreenManager manager;
    [SerializeField] GameObject fadeGo;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            manager.OpenMenu(ACTIVE_MENU.MAIN);
        }
    }

    public void Play()
    {
        manager.CloseAllMenus();
        fadeGo.SetActive(true);
    }

    public void Settings()
    {
        manager.OpenMenu(ACTIVE_MENU.SETTINGS);
    }

    public void Credits()
    {
        manager.OpenMenu(ACTIVE_MENU.CREDITS);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
