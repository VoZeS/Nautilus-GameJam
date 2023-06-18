using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectorEchoAttack : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem EchoParticles;

    public bool echoInput;
    public bool echoReady;

    // Start is called before the first frame update
    void Start()
    {
        echoReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (echoInput && echoReady)
        {
            animator.SetTrigger("Attack");
            echoReady = false;
            Invoke("EchoReadyAgain", 2.0f);
        }
    }

    public void OnEcho(InputAction.CallbackContext context)
    {
        echoInput = context.action.triggered;
    }

    void EchoReadyAgain()
    {
        echoReady = true;
    }

    public void CastAttack()
    {
        EchoParticles.transform.rotation = Quaternion.identity;
        EchoParticles.Play();
    }
}
