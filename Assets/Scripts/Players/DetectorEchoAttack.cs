using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectorEchoAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem EchoParticles;

    bool echoInput;
    bool echoReady;

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
            EchoParticles.transform.rotation = Quaternion.identity;
            EchoParticles.Play();
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
}
