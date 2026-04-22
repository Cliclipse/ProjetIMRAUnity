using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameStateMachine gameStateMachine;

    void Start()
    {
        gameStateMachine = new GameStateMachine(this);
        gameStateMachine.Initialize();
    }

    void Update()
    {
        gameStateMachine.Update();
    }
}
