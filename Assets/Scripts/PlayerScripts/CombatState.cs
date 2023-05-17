using UnityEngine;

public class CombatState : State
{
    private Vector3 currVelocity;
    private Vector3 cVelocity;
    private float gravityValue;
    private float playerSpeed;
    private bool isOnGround;
    private bool isSheathWeapon;
    private bool attack;

    public CombatState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        this.currVelocity = Vector3.zero;
        input = Vector2.zero;
        gravityVelocity.y = 0;
        velocity = character.playerVelocity;
        playerSpeed = character.movementSpeed;
        this.isOnGround = character.controller.isGrounded;
        this.gravityValue = character.gravity;
        this.isSheathWeapon = false;
        this.attack = false;
    }

    public override void InputHandling()
    {
        base.InputHandling();

        if (drawWeaponAction.triggered)
            this.isSheathWeapon = true;

        if (attackAction.triggered)
            this.attack = true;

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = (velocity.x * character.cameraTransform.right.normalized) + (velocity.z * character.cameraTransform.forward.normalized);
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.anim.SetFloat("Speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if(this.isSheathWeapon)
        {
            character.anim.SetTrigger("SheathSword");
            state.ChangeState(character.standing);
        }

        if(this.attack)
        {
            character.anim.SetTrigger("Attacking");
            state.ChangeState(character.attack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        this.isOnGround = character.controller.isGrounded;

        if (this.isOnGround && (gravityVelocity.y < 0))
            gravityVelocity.y = 0f;

        this.currVelocity = Vector3.SmoothDamp(this.currVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move((this.playerSpeed * Time.deltaTime * this.currVelocity) + (gravityVelocity * Time.deltaTime));

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotatingDampTime);

    }

    public override void ExitState()
    {
        base.ExitState();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
            character.transform.rotation = Quaternion.LookRotation(velocity);
    }
}
