using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLogic : MonoBehaviour
{
    public bool isLeverActivated = false;

    AudioSource audioSource;
    public Animator animatorLever;
    // Start is called before the first frame update
    void Start()
    {
        isLeverActivated = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EchoProjectile")
        {
            isLeverActivated = true;
            animatorLever.SetBool("isActivated", true);
            audioSource.Play();

        }
    }
}
