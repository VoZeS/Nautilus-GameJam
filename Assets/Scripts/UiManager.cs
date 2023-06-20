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


    public float desiredNumb=3000;
    public float initialNumb=3000;
    public float currentNumb=3000;
    public float animationTime = 0.5f;


    public void SetNumber(float value)
    {
        initialNumb = currentNumb;
        desiredNumb = value;
    }



    // Start is called before the first frame update
    void Start()
    {
        detectorEchoAttack = GameObject.Find("PlayerBoy").GetComponent<DetectorEchoAttack>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (levelind)
        {
            case 1:
                setnumber(3000);
                break;

            case 2:
                setnumber(1600);
                break;

            case 3:
                setnumber(500);
                break;

            case 4:
                setnumber(1600);
                break;

            case 5:
                setnumber(3000);
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
                currentNumb -= (animationTime * Time.deltaTime) * (desiredNumb - initialNumb);
                if (currentNumb <= desiredNumb)
                {
                    currentNumb = desiredNumb;
                    
                }

            }
            yearsText.text = "Actual Year: "+currentNumb.ToString()+" AC" ;
        }
        

        if (detectorEchoAttack.echoReady)
        {
            guitar.SetActive(true);
            
        }
            

        else
        {
            guitar.SetActive(false);
        }
            

        

    }
    
    
}
