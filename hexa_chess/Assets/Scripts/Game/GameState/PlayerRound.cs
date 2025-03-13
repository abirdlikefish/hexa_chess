using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRound : GameState
{
    public PlayerStateMachine playerRoundStateMachine;

    public PlayerRound(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine,
        _whichState)
    {
    }

    public override void Update()
    {
        playerRoundStateMachine.currentState.Update();
    }

    public override void Enter()
    {
        base.Enter();
        playerRoundStateMachine = new PlayerStateMachine();
        playerRoundStateMachine.BuildState();
        playerRoundStateMachine.Initialize(MyEnum.PlayerRoundState.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PressTestButton()
    {
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }
}