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
    private string aCrist = " AC";
    private DetectorEchoAttack detectorEchoAttack;
    private ProjectileEchoAttack projectileEchoAttack;
    public int levelInd;
    private bool inL1 = true;
    private bool inL2 = false;
    private bool inL3 = false;
    private bool inL4 = false;
    private bool inL5 = false;



    public float desiredNumb;
    public float initialNumb=3000;
    public float currentNumb;
    public float animationTime = 1f;


    public void SetNumber(float value)
    {
        initialNumb = currentNumb;
        desiredNumb = value;
    }



    // Start is called before the first frame update
    void Start()
    {
        detectorEchoAttack = GameObject.Find("PlayerBoy").GetComponent<DetectorEchoAttack>();
        projectileEchoAttack = GameObject.Find("PlayerGirl").GetComponent<ProjectileEchoAttack>();

        animationTime = 1f;
        initialNumb = 3000;
        inL1 = true;
        inL2 = false;
        inL3 = false;
        inL4 = false;
        inL5 = false;
        aCrist = " AC";
    }

    // Update is called once per frame
    void Update()
    {

        switch (levelInd)
        {
            case 1:
                if (inL1)
                {
                    SetNumber(3000);
                    inL1 = false;
                    inL2 = true;
                    aCrist = " AC";
                }
                
                break;

            case 2:
                if (inL2)
                {
                    SetNumber(1492);
                    inL2 = false;
                    inL1 = true;
                    inL3 = true;
                    aCrist = " AC";
                }
                
                break;

            case 3:
                if(inL3)
                {
                    SetNumber(2181);
                    inL3 = false;
                    inL2 = true;
                    inL4 = true;
                    aCrist = " BC";
                }
                
                break;

            case 4:
                if (inL4)
                {
                    SetNumber(1492);
                    inL4 = false;
                    inL3 = true;
                    inL5 = true;
                    aCrist = " AC";

                }
                
                break;

            case 5:
                if (inL5)
                {
                    SetNumber(3000);
                    inL5 = false;
                    inL4 = true;
                    aCrist = " AC";
                }
                
                break;

        }

        if (currentNumb != desiredNumb)
        {
            
            if (initialNumb < desiredNumb)
            {
                currentNumb += (animationTime * Time.deltaTime) * (desiredNumb - initialNumb);
                if (currentNumb >= desiredNumb)
                {
                    currentNumb = desiredNumb;

                    
                }
                  
               
            }
            else
            {
                currentNumb -= (animationTime * Time.deltaTime) * (initialNumb - desiredNumb);
                if (currentNumb <= desiredNumb)
                {
                    currentNumb = desiredNumb;
                    
                }

            }
            yearsText.text = "Actual Year: "+currentNumb.ToString("0")+ aCrist  ;
        }
        

        if (detectorEchoAttack.echoReady)
        {
            guitar.SetActive(true);
            
        }
        else
        {
            guitar.SetActive(false);
        }

        if (projectileEchoAttack.echoReady)
        {
            microphone.SetActive(true);

        }
        else
        {
            microphone.SetActive(false);
        }




    }
    
    
}
