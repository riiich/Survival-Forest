public class TrackOfState
{
    public State currentState;

    public void InitializeState(State startState)
    {
        this.currentState = startState;
        startState.EnterState();
    }

    public void ChangeState(State newState)
    {
        currentState.ExitState();

        currentState = newState;
        newState.EnterState();
    }
}
