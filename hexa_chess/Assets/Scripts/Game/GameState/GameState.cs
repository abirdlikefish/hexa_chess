using UnityEngine;

public class GameState
{
    protected GameStateMachine gameStateMachine;
    protected MyEnum.GameState whichState;

    public GameState(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState)
    {
        gameStateMachine = _gameStateMachine;
        whichState = _whichState;
    }

    public virtual void Enter()
    {
        Debug.Log("Entered Game State: " + whichState.ToString());
    }

    public virtual void Exit()
    {
        Debug.Log("Exited Game State: " + whichState.ToString());
    }

    public virtual void Update()
    {
    }

    public virtual void PressTestButton()
    {
    }

}