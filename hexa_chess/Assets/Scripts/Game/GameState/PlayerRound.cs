using UnityEngine;

public class PlayerRound : GameState
{
    public PlayerRound(GameStateMachine _gameStateMachine, Enum.GameState _whichState) : base(_gameStateMachine, _whichState)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PressTestButton()
    {
        gameStateMachine.ChangeState(GameStateMachine.Instance.EnemyRound);
    }
    
}