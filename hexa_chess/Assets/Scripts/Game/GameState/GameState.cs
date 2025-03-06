using UnityEngine;

public class GameState
{
    protected GameStateMachine gameStateMachine;
    private string stateName;

    public GameState(GameStateMachine _gameStateMachine, string _stateName)
    {
        gameStateMachine = _gameStateMachine;
        stateName = _stateName;
    }

    public virtual void Enter()
    {
        Debug.Log("Entered Game State: " + stateName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exited Game State: " + stateName);
    }

}