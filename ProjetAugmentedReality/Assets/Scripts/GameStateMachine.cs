using UnityEngine;
using UnityEngine.Events;

public class GameStateMachine
{
    public IState CurrentState { get; private set; }

    public PlayState TurnP1State;
    public PlayState TurnP2State;
    public EndState EndState;

    private UnityEvent<IState> _onStateChanged = new();

    public GameStateMachine(LevelController levelController)
    {
        Debug.Log("gameStatMachine creation");
        TurnP1State = new PlayState(levelController, 1);
        TurnP2State = new PlayState(levelController, 2);
        EndState = new EndState(levelController, 0);
    }

    public void AddStateChangedListener(UnityAction<IState> listener) => _onStateChanged.AddListener(listener);
    public void RemoveStateChangedListener(UnityAction<IState> listener) => _onStateChanged.RemoveListener(listener);

    public void Initialize()
    {
        CurrentState = TurnP1State;
        TurnP1State.Enter();

        _onStateChanged.Invoke(CurrentState);
        
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        _onStateChanged.Invoke(nextState);
    }

    public void Update()
    {
        CurrentState?.Execute();
    }
}
