using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] Transform keyPoint;
    Animator animator;
    [SerializeField] Animator secondAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyOn()
    {
        animator.SetTrigger("Open");
        if (secondAnimator) secondAnimator.SetTrigger("Open");
    }
}
