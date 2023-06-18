using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    bool onGroundLastFrame = true;
    [SerializeField] Vector3 groundBoxSize;
    [SerializeField] float groundBoxDistance;
    [SerializeField] LayerMask groundLayerMask;

    [Header("Rotation")]
    public float rotationSpeed = 100.0f;
    public bool lookingRight;
    [HideInInspector] public int rotationDirection; // no rotation --> 0, right --> 1, left --> 2
    float rotationAngle;

    Rigidbody rb;
    [SerializeField] Animator animator;

    [Header("KeyAttached")]
    [NonEditable] public Key keyAttached;

    private void Start()
    {
        // controller
        if (player1)
        {
            int playerIndex = GetComponent<PlayerInput>().playerIndex;
            int controllerType1 = SettingsManager.instance.controllerType1;
            if (controllerType1 == 0) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
            else if (controllerType1 == 1) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
            else if (Gamepad.all.Count > 0) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
            else PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
        }
        else
        {
            int playerIndex = GetComponent<PlayerInput>().playerIndex;
            int controllerType2 = SettingsManager.instance.controllerType2;
            if (controllerType2 == 0) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
            else if (controllerType2 == 1) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
            else
            {
                int controllerType1 = SettingsManager.instance.controllerType1;
                if (controllerType1 == 2)
                {
                    if (Gamepad.all.Count > 1) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("Controller", Gamepad.all[1]);
                    else if (Gamepad.all.Count > 0) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
                    else PlayerInput.all[playerIndex].SwitchCurrentControlScheme("KeyboardARROWS", Keyboard.current);
                }
                else if (Gamepad.all.Count > 0) PlayerInput.all[playerIndex].SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
            }
        }

        rb = GetComponent<Rigidbody>();

        if (lookingRight) { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); rotationAngle = 0; }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 180, 0); rotationAngle = 180; }
        rotationDirection = 0;

        keyAttached = null;
    }

    void Update()
    {
        rb.angularVelocity = new Vector3(0, 0, 0);

        onGround = Physics.BoxCast(transform.position, groundBoxSize / 2.0f, Vector3.down, Quaternion.identity, groundBoxDistance, groundLayerMask);

        if (onGround)
        {
            airMovement = 0.0f;
            if (!onGroundLastFrame) animator.SetTrigger("OnGround");
            else animator.ResetTrigger("OnGround");

            // rotation
            if (lookingRight && movementInput < 0 && rotationDirection != -1)
            {
                StopAllCoroutines();
                StartCoroutine("RotateToLeft");
            }
            else if (!lookingRight && movementInput > 0 && rotationDirection != 1)
            {
                StopAllCoroutines();
                StartCoroutine("RotateToRight");
            }

            // movement
            rb.velocity = new Vector3(movementInput * speed, rb.velocity.y, 0);

            // jump
            if (jumpInput && !jumping)
            {
                //rb.AddForce(new Vector3(0, jumpForce, 0));
                jumping = true;
                speedAtJump = rb.velocity.x;
                animator.SetTrigger("Jump");
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

        animator.SetFloat("Speed", rb.velocity.x);
        animator.SetFloat("Vertical_Speed", rb.velocity.y);

        onGroundLastFrame = onGround;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (keyAttached != null && collision.gameObject.CompareTag("KeyDoor"))
        {
            keyAttached.UseKey(collision.gameObject);
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
    public void JumpForce()
    {
        if (onGround) rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    IEnumerator RotateToRight()
    {
        lookingRight = true;
        rotationDirection = 1;
        while (rotationAngle > 0.0f)
        {
            rotationAngle -= rotationSpeed * Time.deltaTime;
            rb.rotation = Quaternion.Euler(0, rotationAngle, 0);
            yield return null;
        }
        rotationDirection = 0;
        rb.rotation = Quaternion.Euler(0, 0.0f, 0);
    }

    IEnumerator RotateToLeft()
    {
        lookingRight = false;
        rotationDirection = -1;
        while (rotationAngle < 180.0f)
        {
            rotationAngle += rotationSpeed * Time.deltaTime;
            rb.rotation = Quaternion.Euler(0, rotationAngle, 0);
            yield return null;
        }
        rotationDirection = 0;
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
