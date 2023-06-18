using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileEchoAttack : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] GameObject echoPrefab;
    PlayerController playerController;

    bool echoInput;
    bool echoReady;
    bool aimingUp;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        echoReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (echoInput && echoReady)
        {
            if (aimingUp)
            {
                direction = new Vector3(0, 0, 90);
                animator.SetTrigger("Attack_Up");
            }
            else
            {
                if (playerController.rotationDirection == -1) direction = new Vector3(0, 180, 0);
                else if (playerController.rotationDirection == 1) direction = new Vector3(0, 0, 0);
                else if (playerController.lookingRight) direction = new Vector3(0, 0, 0);
                else direction = new Vector3(0, 180, 0);
                animator.SetTrigger("Attack_Front");
            }
            echoReady = false;
            Invoke("EchoReadyAgain", 2.0f);
        }
    }

    public void OnEcho(InputAction.CallbackContext context)
    {
        echoInput = context.action.triggered;
    }

    public void OnAimUp(InputAction.CallbackContext context)
    {
        aimingUp = context.action.triggered;
    }

    void EchoReadyAgain()
    {
        echoReady = true;
    }

    public void SpawnProjectile()
    {
        Instantiate(echoPrefab, transform.position, Quaternion.Euler(direction));
    }
}