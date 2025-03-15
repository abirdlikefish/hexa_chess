using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRound : GameState
{
    public PlayerStateMachine playerRoundStateMachine;

    public PlayerRound(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine,
        _whichState)
    {
        playerRoundStateMachine = new PlayerStateMachine();
        playerRoundStateMachine.BuildState();
        playerRoundStateMachine.Initialize(MyEnum.PlayerRoundState.Idle);
    }

    public override void Update()
    {
        playerRoundStateMachine.currentState.Update();
    }

    public override void Enter()
    {
        base.Enter();
        playerRoundStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
        MyEvent.OnClick_nextBtn += NextBtnClick;
    }

    public override void Exit()
    {
        base.Exit();
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        playerRoundStateMachine.Exit();
        MyEvent.OnClick_nextBtn -= NextBtnClick;
    }

    public override void PressTestButton()
    {
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }
    private void NextBtnClick()
    {
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }
}