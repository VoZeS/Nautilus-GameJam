using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectorEchoAttack : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem EchoParticles;

    public bool echoInput;
    public bool echoReady;

    [NonEditable][SerializeField] bool upgraded;
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] float upgradeCooldown = 1.5f;

    [SerializeField] AudioSource attack_audiosource;
    [SerializeField] AudioClip accord1;
    [SerializeField] AudioClip accord2;
    [SerializeField] AudioClip accord3;

    // Start is called before the first frame update
    void Start()
    {
        echoReady = true;

        upgraded = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagePause.instance.paused) return;

        if (echoInput && echoReady)
        {
            Invoke("PlayAccord", 0.3f);

            animator.SetTrigger("Attack");
            echoReady = false;
            if (upgraded) Invoke("EchoReadyAgain", upgradeCooldown);
            else Invoke("EchoReadyAgain", cooldown);
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

    public void Upgrade()
    {
        upgraded = true;
    }

    void PlayAccord()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                attack_audiosource.clip = accord1;
                break;
            case 1:
                attack_audiosource.clip = accord2;
                break;
            case 2:
                attack_audiosource.clip = accord3;
                break;
        }

        attack_audiosource.Play();
    }
    
}
