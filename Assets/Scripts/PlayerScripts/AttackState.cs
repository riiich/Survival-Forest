using UnityEngine;

public class AttackState : State
{
    private bool isAttacking;
    private float timePassed;   // time within the attack animation
    private float attackTimeLength; // length of the attack animation
    private float attackTimeSpeed;  // used to change speed of animation

    public AttackState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        this.isAttacking = false;
        character.anim.applyRootMotion = true;
        this.timePassed = 0f;
        character.anim.SetTrigger("Attacking");
        character.anim.SetFloat("Speed", 0f);
        character.WeaponAttackAudio.enabled = true; // play sword swinging audioa when player attacks
    }

    public override void InputHandling()
    {
        base.InputHandling();

        if (attackAction.triggered)
            this.isAttacking = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        this.timePassed += Time.deltaTime;
        this.attackTimeLength = character.anim.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        this.attackTimeSpeed = character.anim.GetCurrentAnimatorStateInfo(1).speed;
        
        if((this.timePassed >= this.attackTimeLength / this.attackTimeSpeed) && this.isAttacking)
        {

            state.ChangeState(character.attack);
            
        }
            

        if(this.timePassed >= this.attackTimeLength / this.attackTimeSpeed)
        {
            
            state.ChangeState(character.combat);
            character.anim.SetTrigger("Moving");
            
        }
    }

    public override void ExitState()
    {
        character.WeaponAttackAudio.enabled = false;    // when player stops attacking, turn off the sword swinging audio
        base.ExitState();
        character.anim.applyRootMotion = false;
    }
}