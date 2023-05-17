using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public PlayerMovement character;
    public TrackOfState state;

    // different input actions of the character
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction lookAction;
    public InputAction sprintAction;
    public InputAction crouchAction;    // not implemented yet, will do it later
    public InputAction drawWeaponAction;
    public InputAction attackAction;

    // used protected to access these data from an inherited class(PlayerMovement), (Note: protected can only access data from inherited class)
    protected Vector2 input;
    protected Vector3 velocity;
    protected Vector3 gravityVelocity;

    // constructor to set state of character
    public State(PlayerMovement _character, TrackOfState _state)
    {
        this.character = _character;
        this.state = _state;

        this.moveAction = character.playerInput.actions["Move"];
        this.jumpAction = character.playerInput.actions["Jump"];
        this.lookAction = character.playerInput.actions["Look"];
        this.sprintAction = character.playerInput.actions["Sprint"];
        this.crouchAction = character.playerInput.actions["Crouch"];
        this.drawWeaponAction = character.playerInput.actions["DrawWeapon"];
        this.attackAction = character.playerInput.actions["Attack"];
    }

    // set these functions to be virtual so it can be override'd in the different state classes
    public virtual void EnterState()
    {
        Debug.Log("Enter State: " + this.ToString());
    }

    public virtual void ExitState()
    {
        Debug.Log("Exit State: " + this.ToString());
    }

    public virtual void InputHandling()
    {}

    public virtual void LogicUpdate()
    {}

    public virtual void PhysicsUpdate()
    {}
}
