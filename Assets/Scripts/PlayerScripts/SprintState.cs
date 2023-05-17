using UnityEngine;

public class SprintState : State
{
    private Vector3 currVelocity;
    private Vector3 cVelocity;
    private float gravityValue;
    private float playerSpeed;
    private bool isOnGround;
    private bool isSprinting;
    private bool jumpWhileSprinting;    // can implement later

    public SprintState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        input = Vector2.zero;
        velocity = Vector3.zero;
        gravityVelocity.y = 0f;

        this.currVelocity = Vector3.zero;
        this.gravityValue = character.gravity;
        this.playerSpeed = character.sprintSpeed;
        this.isOnGround = character.controller.isGrounded;
        this.isSprinting = false;
        this.jumpWhileSprinting = false;
    }

    public override void InputHandling()
    {
        base.EnterState();

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = (velocity.x * character.cameraTransform.right.normalized) + (velocity.z * character.cameraTransform.forward.normalized);
        velocity.y = 0f;

        // check if sprinting key is pressed or if the character is moving or not
        if (sprintAction.triggered || (input.sqrMagnitude == 0f))
            this.isSprinting = false;
        else
            this.isSprinting = true;
        
        // jump while sprinting (if player clicks jump while sprinting, character will jump while sprinting) (add later)
        /*
        if(jumpAction.triggered)
            jumpWhileSprinting = true;

        */
    }

    public override void LogicUpdate()
    {
        if (isSprinting)
        {
            character.FootStepAudio.enabled = true; // play sprinting footstep audio when player starts sprinting
            character.anim.SetFloat("Speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
        }
        else
        {
            state.ChangeState(character.standing);
            character.FootStepAudio.enabled = false;    // turn off sprinting footstep audio when player stops sprinting
        }
            

        /*  //jump while sprinting (add later)
        if (jumpWhileSprinting)
            state.ChangeState(character.sprintJumping);
        */
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += this.gravityValue * Time.deltaTime;
        this.isOnGround = character.controller.isGrounded;

        if (isOnGround && (gravityVelocity.y < 0))
            gravityVelocity.y = 0f;

        this.currVelocity = Vector3.SmoothDamp(this.currVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move((playerSpeed * Time.deltaTime * this.currVelocity) + (gravityVelocity * Time.deltaTime));

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotatingDampTime);
    }
}
