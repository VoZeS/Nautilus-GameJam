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
            if (playersOnZone == 1) container.SetActive(false);
            else if (playersOnZone == 2)
            {
                if (prevContainer) prevContainer.SetActive(true);
                if (nextContainer) nextContainer.SetActive(true);
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
}
