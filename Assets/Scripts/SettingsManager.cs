using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public int controllerType1; // 0 --> WASD, 1 --> ARROWS, 2 --> Gamepad
    public int controllerType2;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SettingsManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
