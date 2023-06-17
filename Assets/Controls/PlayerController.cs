using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool player1;

    [Header("Movement")]
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpForce = 1.0f;
    float movementInput = 0.0f;
    bool jumped = false;

    [Header("Ground")]
    [NonEditable] bool onGround;
    [SerializeField] Vector3 groundBoxSize;
    [SerializeField] float groundBoxDistance;
    [SerializeField] LayerMask groundLayerMask;

    [Header("Rotation")]
    [SerializeField] bool lookingRight;
    int rotatingPhase;
    int necesaryRotatingPhases;

    Rigidbody rb;

    private void Start()
    {
        // controller
        if (player1)
        {
            int controllerType1 = SettingsManager.instance.controllerType1;
            if (controllerType1 == 0) PlayerInput.all[0].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
            else if (controllerType1 == 1) PlayerInput.all[0].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
            else if (Gamepad.all.Count > 0) PlayerInput.all[0].SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
            else PlayerInput.all[0].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
        }
        else
        {
            int controllerType2 = SettingsManager.instance.controllerType2;
            if (controllerType2 == 0) PlayerInput.all[1].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
            else if (controllerType2 == 1) PlayerInput.all[1].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
            else
            {
                int controllerType1 = SettingsManager.instance.controllerType1;
                if (controllerType1 == 2)
                {
                    if (Gamepad.all.Count > 1) PlayerInput.all[1].SwitchCurrentControlScheme("Controller", Gamepad.all[1]);
                    else if (Gamepad.all.Count > 0) PlayerInput.all[1].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
                    else PlayerInput.all[1].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
                }
                else if (Gamepad.all.Count > 0) PlayerInput.all[1].SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
            }
        }

        rb = GetComponent<Rigidbody>();

        if (lookingRight) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        else gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        rotatingPhase = 0;
    }

    void Update()
    {
        onGround = Physics.BoxCast(transform.position, groundBoxSize, -transform.up, Quaternion.identity, groundBoxDistance, groundLayerMask);

        if (onGround)
        {
            if (lookingRight && movementInput < 0 && rotatingPhase >= 0)
            {
                if (rotatingPhase == 0) necesaryRotatingPhases = 181;
                else necesaryRotatingPhases = Mathf.Abs(rotatingPhase);
                StartCoroutine("RotateToLeft");
                StopCoroutine("RotateToRight");
            }
            else if (!lookingRight && movementInput > 0 && rotatingPhase <= 0)
            {
                if (rotatingPhase == 0) necesaryRotatingPhases = 181;
                else necesaryRotatingPhases = Mathf.Abs(rotatingPhase);
                StartCoroutine("RotateToRight");
                StopCoroutine("RotateToLeft");
            }
            rb.velocity = new Vector3(movementInput * speed, 0, 0);

            if (jumped)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0));
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    IEnumerator RotateToRight()
    {
        lookingRight = true;
        for (int i = 0; i < necesaryRotatingPhases - 1; i++)
        {
            rb.rotation *= Quaternion.Euler(0, -1.0f, 0);
            rotatingPhase = i + 1;
            yield return null;
        }
        rotatingPhase = 0;
        rb.rotation = Quaternion.Euler(0, 0.0f, 0);
    }

    IEnumerator RotateToLeft()
    {
        lookingRight = false;
        for (int i = 0; i < necesaryRotatingPhases - 1; i++)
        {
            rb.rotation *= Quaternion.Euler(0, 1.0f, 0);
            rotatingPhase = -i - 1;
            yield return null;
        }
        rotatingPhase = 0;
        rb.rotation = Quaternion.Euler(0, 180.0f, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * groundBoxDistance, groundBoxSize);
    }
}
