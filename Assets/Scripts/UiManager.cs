using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text yearsText;
    public float years;
    public float actualYears;
    public GameObject microphone;
    public GameObject guitar;
    private DetectorEchoAttack detectorEchoAttack;
    public int levelInd;
    public int CountFPS = 30;
    public float duration = 1f;
    private Coroutine CountingCoroutine;

    private void UpdateText(int newValue)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }
        CountingCoroutine = StartCoroutine(CountText(newValue));

    }

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
                UpdateText(3000);
                break;

            case 2:
                UpdateText(1600);
                break;

            case 3:
                UpdateText(500);
                break;

            case 4:
                UpdateText(1600);
                break;

            case 5:
                UpdateText(3000);
                break;

        }
        

        if (detectorEchoAttack.echoReady)
            guitar.SetActive(true);
        else
            guitar.SetActive(false);

        

    }
    
    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS) ;
        float previousYears = years;
        float stepAmount;

        if (newValue - previousYears < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousYears) / (CountFPS * duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousYears) / (CountFPS * duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }

        if (previousYears < newValue)
        {
            while(previousYears < newValue)
            {
                previousYears += stepAmount;
                if (previousYears > newValue)
                {
                    previousYears = newValue;
                }
                yearsText.text = previousYears.ToString() + " A.C";
                yield return Wait;
            }
        }
        else
        {
            while (previousYears > newValue)
            {
                previousYears += stepAmount;
                if (previousYears < newValue)
                {
                    previousYears = newValue;
                }
                yearsText.text = previousYears.ToString() + " A.C";
                yield return Wait;
            }
        }
       
    }
}
