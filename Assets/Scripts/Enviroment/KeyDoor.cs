using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Transform keyPoint;
    Animator animator;
    [SerializeField] Animator secondAnimator;
    [SerializeField] float delay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyOn()
    {
        Invoke("PlayAnimation", delay);
        if (secondAnimator) secondAnimator.SetTrigger("Open");
        audioSource.Play();
    }

    void PlayAnimation()
    {
        animator.SetTrigger("Open");
    }
}
