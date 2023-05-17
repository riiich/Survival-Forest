using UnityEngine;

public class StandState : State
{
    Vector3 currVelocity;
    Vector3 cVelocity;
    private float playerSpeed;
    private float gravity;
    private bool isOnGround;
    private bool isJumping;
    private bool isSprinting;
    private bool isCrouching;   //implement later
    private bool isDrawWeapon;

    // constructor
    public StandState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)    // base(character, state) is for the base class (State class)
    {
        // setting the character and state from the State class to _character and _state
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        gravityVelocity.y = 0;
        velocity = character.playerVelocity;
        input = Vector2.zero;
        this.currVelocity = Vector3.zero;
        this.playerSpeed = character.movementSpeed;
        this.gravity = character.gravity;
        this.isOnGround = character.controller.isGrounded;
        this.isJumping = false;
        this.isSprinting = false;
        this.isCrouching = false;
        this.isDrawWeapon = false;
    }

    public override void InputHandling()
    {
        base.InputHandling();

        // check if the different actions are pressed on the keyboard
        if (jumpAction.triggered)
            this.isJumping = true;
        if (sprintAction.triggered)
            this.isSprinting = true;
        if (crouchAction.triggered)
            this.isCrouching = true;
        if (drawWeaponAction.triggered)
            this.isDrawWeapon = true;

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = (velocity.x * character.cameraTransform.right.normalized) + (velocity.z * character.cameraTransform.forward.normalized);
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.anim.SetFloat("Speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (isJumping)
            state.ChangeState(character.jump);
        if (isSprinting)
            state.ChangeState(character.sprint);
        if (isCrouching)
            state.ChangeState(character.crouch);
        if (isDrawWeapon)
        {
            state.ChangeState(character.combat);
            character.anim.SetTrigger("UnsheathSword");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravity * Time.deltaTime;
        isOnGround = character.controller.isGrounded;
        
        // applying gravity
        if (isOnGround && (gravityVelocity.y < 0))
            gravityVelocity.y = 0f;

        currVelocity = Vector3.SmoothDamp(currVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move((currVelocity * Time.deltaTime * playerSpeed) + (gravityVelocity * Time.deltaTime));  // move character

        // rotating player
        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotatingDampTime);
    }

    public override void ExitState()
    {
        base.ExitState();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        // rotate player to correct direction
        if(velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.LookRotation(velocity);
    }
}