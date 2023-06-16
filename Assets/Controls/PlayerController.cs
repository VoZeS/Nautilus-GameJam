using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool player1;

    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpForce = 1.0f;
    float movementInput = 0.0f;
    bool jumped = false;
    bool onGround;

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
                    else PlayerInput.all[1].SwitchCurrentControlScheme("KeyboardWASD", Keyboard.current);
                }
                else if (Gamepad.all.Count > 0) PlayerInput.all[1].SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
            }
        }

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (onGround)
        {
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
}
