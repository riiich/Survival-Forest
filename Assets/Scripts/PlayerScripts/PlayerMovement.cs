using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 

public class PlayerMovement : MonoBehaviour
{
    // different states
    public TrackOfState movementState;
    public StandState standing;
    public JumpState jump;
    public LandState landFromJump;
    public SprintState sprint;
    public CrouchState crouch;
    public CombatState combat;
    public AttackState attack;

    // character variables
    public float gravityMultiplier = 1.0f;
    public float rotationSpeed = 3.0f;
    public float crouchColliderHeight = 1.5f;

    // Animations configurations
    public float speedDampTime = 0.5f;
    public float velocityDampTime = 0.1f;
    public float rotatingDampTime = 0.1f;
    public float controlInAir = 0.2f;

    // SerializeField variables
    [SerializeField] public float movementSpeed = 3.0f;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float turnSpeed = 0.1f;
    [SerializeField] public float jumpHeight = 2.0f;
    [SerializeField] public float crouchSpeed = 1.0f;

    // HideInInspector variables
    [HideInInspector] public float gravity = -9.81f;
    [HideInInspector] public Vector3 playerVelocity;
    [HideInInspector] public float colliderHeight;  // height of the character
    [HideInInspector] public Animator anim;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Transform cameraTransform;

    // Audio
    public AudioSource FootStepAudio;  // better to be private, but needs to be public because it needs to be used in other classes
    public AudioSource WeaponAttackAudio;
    public AudioSource JumpAudio;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        // initialize states
        this.movementState = new TrackOfState();
        this.standing = new StandState(this, movementState);
        this.jump = new JumpState(this, movementState);
        this.landFromJump = new LandState(this, movementState);
        this.sprint = new SprintState(this, movementState);
        this.crouch = new CrouchState(this, movementState);
        this.combat = new CombatState(this, movementState);
        this.attack = new AttackState(this, movementState);

        movementState.InitializeState(standing);

        gravity *= gravityMultiplier;
        colliderHeight = controller.height;
        this.sprintSpeed = this.movementSpeed * 2f;
    }

    private void Update()
    {
        movementState.currentState.InputHandling();
        movementState.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementState.currentState.PhysicsUpdate();
    }
}