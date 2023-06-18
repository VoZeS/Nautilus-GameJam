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

    // Start is called before the first frame update
    void Start()
    {
        detectorEchoAttack = GameObject.Find("PlayerBoy").GetComponent<DetectorEchoAttack>();
        years = 3000f;
        yearsText.text = years.ToString() + " A.C"; 
    }

    // Update is called once per frame
    void Update()
    {
        if (detectorEchoAttack.echoReady)
            guitar.SetActive(true);
        else
            guitar.SetActive(false);

    }
}
