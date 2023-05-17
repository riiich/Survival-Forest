using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.MovementActions movement ;
    private Vector2 movementInput;
    private PlayerMotors motor;
    private PlayerLook look;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        movement = playerInput.Movement;
        motor = GetComponent<PlayerMotors>();
        look = GetComponent<PlayerLook>();
        movement.Jumping.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.processMove(movement.Walking.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        look.ProcessLook(movement.Looking.ReadValue<Vector2>());
    }
    private void OnEnable(){
        movement.Enable();
    }
    private void OnDisable(){
        movement.Disable();
    }
}
