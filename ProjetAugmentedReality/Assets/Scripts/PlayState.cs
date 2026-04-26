using UnityEngine;

public class PlayState : IState
{
    private readonly LevelController _levelController;
    private GameStateMachine _gameStateMachine;
    
    public int Player;
    

    public PlayState(LevelController levelController , int Player)
    {
        _levelController = levelController;
        this.Player = Player;
        _gameStateMachine = _levelController.gameStateMachine;
        
    }

    public void Enter()
    {
        Debug.Log("Enter Turn of the player" + Player);
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // J'mets juste ça pour tester les transitions d'état
        {
            Exit();
            if (Player == 1) _levelController.gameStateMachine.TransitionTo(_levelController.gameStateMachine.TurnP2State);
            else _levelController.gameStateMachine.TransitionTo(_levelController.gameStateMachine.TurnP1State);
        }
        if (Input.GetKeyDown(KeyCode.B)) // J'mets juste ça pour tester les transitions d'état
        {
            Exit();
            _levelController.gameStateMachine.EndState.Winner = Player;
            _levelController.gameStateMachine.TransitionTo(_levelController.gameStateMachine.EndState);
        }
    }

    public void Exit()
    {
        Debug.Log("End Turn of the player" + Player);

    }

}
