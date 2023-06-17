using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InitialScreenManager manager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Play()
    {
        // fade in
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
