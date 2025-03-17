using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRound : GameState
{
    public PlayerStateMachine playerRoundStateMachine;

    public PlayerRound(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine,
        _whichState)
    {
        playerRoundStateMachine = new PlayerStateMachine();
        // playerRoundStateMachine.BuildState();
        // playerRoundStateMachine.Initialize(MyEnum.PlayerRoundState.Idle);
        playerRoundStateMachine.Initialize();
        playerRoundStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
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
        UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Player , new Vector2Int(10, 10), MyEnum.ArmyType.Tank);
        UnitManager.Instance.RoundBeginOperation(MyEnum.TheOperator.Player);
    }

    public override void Exit()
    {
        base.Exit();
        // MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        playerRoundStateMachine.Exit();
        MyEvent.OnClick_nextBtn -= NextBtnClick;
        GameManager.instance.IncreaseRoundsCounter();
        MyEvent.SetGlobalInfo(new Vector2(1,1) , new Vector2(1,1) , new Vector2(1,1));
    }

    // public override void PressTestButton()
    // {
    //     gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    // }
    private void NextBtnClick()
    {
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }
}