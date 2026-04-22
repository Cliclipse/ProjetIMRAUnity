using UnityEngine;
using UnityEngine.UI;

public class EndState : IState
{
    private readonly LevelController _levelController;
    public int Winner;
    

    public EndState(LevelController levelController , int player)
    {
        _levelController = levelController;
        Winner = player;
    }

    public void Enter()
    {
        Debug.Log("Victory of the player" + Winner);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }

}
