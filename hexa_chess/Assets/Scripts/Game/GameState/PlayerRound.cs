using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRound : GameState
{
    public PlayerStateMachine playerRoundStateMachine;
    public IUnit DefaultSelectedUnit = null;

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

        if(GameManager.instance.CurrentRoundsCounter() == 0)
        {
            UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Player, new Vector2Int(5, 4), MyEnum.CityType.Home);
            gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
            return;
        }
        playerRoundStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
        MyEvent.OnClick_nextBtn += NextBtnClick;
        //UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Player , new Vector2Int(5, 5), MyEnum.ArmyType.Tank);
        UnitManager.Instance.RoundBeginOperation(MyEnum.TheOperator.Player);
        MyEvent.OnPress += ShowGridInfoWin;
        MyEvent.OnPressEnd += CloseGridInfoWin;
        MyEvent.EnterPlayerRound?.Invoke(true);

        DefaultSelectedUnit = UnitManager.Instance.GetAbleUnit(MyEnum.TheOperator.Player);
    }

    public override void Exit()
    {
        base.Exit();
        // MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        playerRoundStateMachine.Exit();
        MyEvent.OnClick_nextBtn -= NextBtnClick;
        //GameManager.instance.IncreaseRoundsCounter();
        MyEvent.OnPress -= ShowGridInfoWin;
        MyEvent.OnPressEnd -= CloseGridInfoWin;
        MyEvent.EnterPlayerRound?.Invoke(false);
    }

    // public override void PressTestButton()
    // {
    //     gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    // }
    private void NextBtnClick()
    {
        //todo:这里要执行Skip
        // DefaultSelectedUnit?.Skip();
        /*if (playerRoundStateMachine.selectedUnit != null)
        {
            MyEvent.OnClick_skipBtn?.Invoke();
            return;
        }
        DefaultSelectedUnit = UnitManager.Instance.GetAbleUnit(MyEnum.TheOperator.Player);
        if (DefaultSelectedUnit != null)
        {
            playerRoundStateMachine.selectedUnit = DefaultSelectedUnit;
            playerRoundStateMachine.selectedGrid = DefaultSelectedUnit.Coord;
            playerRoundStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_WhichAction);
        }
        else
        {
            gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
        }*/
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }
    
    private void ShowGridInfoWin(Vector2Int coord)
    {
        int atk = MapManager.Instance.GetAtkOffset(MyEnum.TheOperator.Player, coord);
        int def = MapManager.Instance.GetDefOffset(MyEnum.TheOperator.Player, coord);
        float moveCost = MapManager.Instance.GetMoveCost(MyEnum.TheOperator.Player, coord);
        MyEvent.ShowGridInfoWin?.Invoke(atk, def, moveCost);
    }
    private void CloseGridInfoWin()
    {
        MyEvent.HideGridInfoWin?.Invoke();
    }

}