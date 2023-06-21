using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagePause : MonoBehaviour
{
    public static ManagePause instance;

    [NonEditable] public bool paused;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject firstButton;

    GameObject lastSelect;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(lastSelect);
        else lastSelect = EventSystem.current.currentSelectedGameObject;

        if (Input.GetButtonDown("Pause") && !pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstButton);
            PauseGame(true);
        }
    }

    public void PauseGame(bool pause)
    {
        paused = pause;
        if (paused) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}
