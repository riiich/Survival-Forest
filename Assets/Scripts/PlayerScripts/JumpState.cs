using UnityEngine;

public class JumpState : State
{
    private float jumpHeight;
    private float playerSpeed;
    private float gravityValue;
    private bool isOnGround;
    private Vector3 airVelocity;

    public JumpState(PlayerMovement _character, TrackOfState _state) :base(_character, _state) 
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        isOnGround = false;
        gravityValue = character.gravity;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.movementSpeed;
        gravityVelocity.y = 0f;

        character.anim.SetFloat("Speed", 0);
        character.anim.SetTrigger("Jumping");
        Jump();
    }

    public override void InputHandling()
    {
        base.InputHandling();

        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // checks if character is on the ground or not
        if (isOnGround)
            state.ChangeState(character.landFromJump);  // change the state to landing
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // the player is in the air
        if(!isOnGround)
        {
            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;

            // be able to move the player while in the air (depending on what the value of controlInAir is)
            character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.controlInAir + 
                                           velocity * (1 - character.controlInAir))* playerSpeed * Time.deltaTime);    
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        isOnGround = character.controller.isGrounded;   // character is now on the ground
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    // character jumps
    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}
