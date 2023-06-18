using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;  

    [SerializeField] GameObject playerBoy;
    [SerializeField] GameObject playerGirl;

    [NonEditable] public GameObject currentCheckpoint;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentSpawn(GameObject go)
    {
        currentCheckpoint = go;
    }

    void Respawn()
    {
        playerBoy.transform.position = currentCheckpoint.transform.Find("BoyPoint").position;
        playerBoy.transform.rotation = currentCheckpoint.transform.Find("BoyPoint").rotation;

        playerGirl.transform.position = currentCheckpoint.transform.Find("GirlPoint").position;
        playerGirl.transform.rotation = currentCheckpoint.transform.Find("GirlPoint").rotation;
    }

    public void FadeInFinish()
    {
        Respawn();
        FadeManager.instance.FadeOut();
    }
}
