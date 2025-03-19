using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerRound_SelectedBase : PlayerRoundState
{
    public PlayerRound_SelectedBase(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

    public MyEnum.ArmyType selectedType;

    public override void Enter()
    {
        base.Enter();
        MyEvent.OnClick_GenerateBtn += CreateUnit;
        MyEvent.OnClick_skipBtn += PressSkip;
    }

    public override void Exit()
    {
        base.Exit();
        MyEvent.OnClick_GenerateBtn -= CreateUnit;
        MyEvent.OnClick_skipBtn -= PressSkip;
    }

    private void CreateUnit(MyEnum.ArmyType armyType)
    {
        Vector2Int? createGrid = playerStateMachine.selectedGrid;
        MyEnum.MoveDirection randomDir= (MyEnum.MoveDirection)Random.Range(0, 6);
        createGrid = createGrid ?? createGrid.Value + MyConst.MoveStep[randomDir];
        UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Player,createGrid.Value, armyType);
    }

    private void PressSkip()
    {
        IUnit ableUnit = UnitManager.Instance.GetAbleUnit(MyEnum.TheOperator.Player);
        if (ableUnit == null)//如果没有可以动的单元了 就换边
        {
            GameStateMachine.Instance.ChangeState(MyEnum.GameState.EnemyRound);
        }
        else
        {
            playerStateMachine.selectedUnit = ableUnit;
            playerStateMachine.selectedGrid = ableUnit.Coord;
            playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_WhichAction);
        }
    }
}