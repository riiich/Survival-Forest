using UnityEngine;

public class LandState : State
{
    private float landTime;
    private float timePassed;

    public LandState(PlayerMovement _character, TrackOfState _state) : base(_character, _state)
    {
        character = _character;
        state = _state;
    }

    public override void EnterState()
    {
        base.EnterState();

        timePassed = 0.0f;
        character.anim.SetTrigger("Landing");
        landTime = 0.5f;
        character.JumpAudio.enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(timePassed > landTime)
        {
            character.anim.SetTrigger("Moving");
            state.ChangeState(character.standing);
            character.JumpAudio.enabled = false;
        }
        timePassed += Time.deltaTime;
    }
}