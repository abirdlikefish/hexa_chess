using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerRound_SelectedBase : PlayerRoundState
{
    public PlayerRound_SelectedBase(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

    private MyEnum.ArmyType selectedType;

    public override void Enter()
    {
        base.Enter();
        // MyEvent.OnClick_GenerateBtn += CreateUnit;
        MyEvent.OnClick_skipBtn += PressSkip;
        MyEvent.OnGridClick_left += SelectGrid;
        MyEvent.SelectArmyToCreate += SelectArmyToCreate;
        MyEvent.OpenUnitUI?.Invoke(playerStateMachine.selectedUnit);
        // MapManager.Instance.SearchCreateArmyArea(MyEnum.TheOperator.Player , playerStateMachine.selectedGrid.Value, (playerStateMachine.selectedUnit as ICity));
MapManager.Instance.SearchCreateArmyArea(MyEnum.TheOperator.Player , playerStateMachine.selectedGrid.Value, 5);
    }

    public override void Exit()
    {
        base.Exit();
        // MyEvent.OnClick_GenerateBtn -= CreateUnit;
        MyEvent.OnClick_skipBtn -= PressSkip;
        MyEvent.OnGridClick_left -= SelectGrid;
        MyEvent.SelectArmyToCreate -= SelectArmyToCreate;
        MyEvent.OpenUnitUI?.Invoke(null);
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
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

    public void SelectGrid(Vector2Int? coord)
    {        
        if (coord == null) return;
        Vector2Int? selectedUnit = MapManager.Instance.GetCreateArmyArea(coord.Value);
        if (selectedUnit == null || selectedType == MyEnum.ArmyType.None)
        {
            Cancel();
            return;
        }
        // if(selectedType == MyEnum.ArmyType.None) return;
        UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Player, selectedUnit.Value, selectedType);
        // playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }

    private void SelectArmyToCreate(MyEnum.ArmyType armyType)
    {
        selectedType = armyType;
        if(selectedType == MyEnum.ArmyType.None) return;
    }
}