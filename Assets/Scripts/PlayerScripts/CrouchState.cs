using UnityEngine;

public class CrouchState : State
{
    private Vector3 currVelocity;
    private float playerSpeed;
    private float gravityValue;
    private bool isOnGround;
    private bool holdingCrouch;
    private bool belowCeiling;

    public CrouchState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        character.anim.SetTrigger("Crouching");
        this.belowCeiling = false;
        this.holdingCrouch = false;
        gravityVelocity.y = 0f;

        this.playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight;   //when crouching, change the character height to the height charcter when crouching
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        isOnGround = character.controller.isGrounded;   // player is on the ground
        gravityValue = character.gravity;
    }

    public override void InputHandling()
    {
        base.InputHandling();

        // check if the crouch button is pressed on the keyboard AND if there is anything above the character
        if (crouchAction.triggered && !belowCeiling)
            this.holdingCrouch = true;

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = (velocity.x * character.cameraTransform.right.normalized) + (velocity.z * character.cameraTransform.forward.normalized);
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.anim.SetFloat("Speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (holdingCrouch)
            state.ChangeState(character.standing);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        this.belowCeiling = CheckCollision(character.transform.position + (Vector3.up * character.colliderHeight)); // check if there's something above character
        gravityVelocity.y += gravityValue * Time.deltaTime;
        isOnGround = character.controller.isGrounded;

        if(isOnGround && (gravityVelocity.y < 0))
            gravityVelocity.y = 0f;

        this.currVelocity = Vector3.Lerp(this.currVelocity, velocity, character.velocityDampTime);
        character.controller.Move((playerSpeed * Time.deltaTime * this.currVelocity) + (gravityVelocity * Time.deltaTime));

        if (velocity.magnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotatingDampTime);
    }

    public override void ExitState()
    {
        base.ExitState();

        character.controller.height = character.colliderHeight;     // when exiting crouch state, change character height back to the normal height
        character.controller.center = new Vector3(0f, character.colliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);
        character.anim.SetTrigger("Moving");    
    }

    public bool CheckCollision(Vector3 position)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        Vector3 direction = position - character.transform.position;

        if(Physics.Raycast(character.transform.position, direction, out hit, character.colliderHeight, layerMask))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.colliderHeight, Color.green);
            return false;
        }
    }
}
