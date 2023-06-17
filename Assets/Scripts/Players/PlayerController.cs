using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    bool jumpInput = false;
    bool jumping = false;
    [SerializeField][Range(0.0f, 1.0f)] float airMovementSpeed = 0.2f;
    [SerializeField] float airMovementIncrement = 1.0f;
    [NonEditable][SerializeField] float airMovement = 0.0f;
    float speedAtJump = 0.0f;

    [Header("Ground")]
    [NonEditable][SerializeField] bool onGround;
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
        onGround = Physics.BoxCast(transform.position, groundBoxSize / 2.0f, Vector3.down, Quaternion.identity, groundBoxDistance, groundLayerMask);

        if (onGround)
        {
            airMovement = 0.0f;

            // rotation
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

            // movement
            rb.velocity = new Vector3(movementInput * speed, 0, 0);

            // jump
            if (jumpInput && !jumping)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0));
                jumping = true;
                speedAtJump = rb.velocity.x;
                Invoke("JumpDone", 0.5f);
            }
        }
        else if (airMovement < 1.0f)
        {
            rb.velocity = new Vector3(Mathf.Lerp(speedAtJump, movementInput * speed * airMovementSpeed, airMovement), rb.velocity.y, 0);
            airMovement += Time.deltaTime * airMovementIncrement;
        }
        else if ((rb.velocity.x > 0 && movementInput < 0) || (rb.velocity.x < 0 && movementInput > 0))
        {
            speedAtJump = rb.velocity.x;
            airMovement = 0.0f;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.action.triggered;
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

    void JumpDone()
    {
        jumping = false;
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(transform.position, groundBoxSize / 2, -transform.up, out hit, Quaternion.identity, groundBoxDistance, groundLayerMask);
        if (isHit)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -transform.up * groundBoxDistance);
            Gizmos.DrawWireCube(transform.position - transform.up * groundBoxDistance, groundBoxSize);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * groundBoxDistance);
            Gizmos.DrawWireCube(transform.position - transform.up * groundBoxDistance, groundBoxSize);
        }
    }
}
