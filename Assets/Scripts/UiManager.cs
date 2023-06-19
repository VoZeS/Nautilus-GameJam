using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text yearsText;
    public float years;
    public GameObject microphone;
    public GameObject guitar;
    private DetectorEchoAttack detectorEchoAttack;
    public int levelInd;


    // Start is called before the first frame update
    void Start()
    {
        detectorEchoAttack = GameObject.Find("PlayerBoy").GetComponent<DetectorEchoAttack>();
        
        years = 3000f;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(levelInd)
        {
            case 1:
                years = 3000f;
                break;

            case 2:
                years = 1600f;
                break;

            case 3:
                years = 500f;
                break;

            case 4:
                years = 1600f;
                break;

            case 5:
                years = 3000f;
                break;

        }
        yearsText.text = years.ToString() + " A.C";

        if (detectorEchoAttack.echoReady)
            guitar.SetActive(true);
        else
            guitar.SetActive(false);

        

    }
}
