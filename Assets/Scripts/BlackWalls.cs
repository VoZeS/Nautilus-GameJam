using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWalls : MonoBehaviour
{
    [NonEditable][SerializeField] int playersOnZone;
    [SerializeField] GameObject container;
    [SerializeField] GameObject prevContainer;
    [SerializeField] GameObject nextContainer;
    [SerializeField] GameObject prevLevel;
    [SerializeField] GameObject prevPrevLevel;
    [SerializeField] GameObject nextLevel;
    [SerializeField] GameObject nextNextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersOnZone++;
            if (playersOnZone == 1) CallAllChilds(container, false);
            else if (playersOnZone == 2)
            {
                if (prevContainer) CallAllChilds(prevContainer, true);
                if (nextContainer) CallAllChilds(nextContainer, true);
                if (nextLevel) nextLevel.SetActive(true);
                if (nextNextLevel) nextNextLevel.SetActive(false);
                if (prevLevel) prevLevel.SetActive(true);
                if (prevPrevLevel) prevPrevLevel.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersOnZone--;
        }
    }

    void CallAllChilds(GameObject container, bool fadeIn)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            if (fadeIn) container.transform.GetChild(i).GetComponent<BlackWallFade>().FadeIn();
            else container.transform.GetChild(i).GetComponent<BlackWallFade>().FadeOut();
        }
    }
}
