using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRound_WaitInputAction : PlayerRoundState
{
    public PlayerRound_WaitInputAction(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //每个单位有五种操作
        MyEvent.OnClick_attackBtn += SelectedAttack;
        MyEvent.OnClick_restBtn += SelectedRest;
        MyEvent.OnClick_stationBtn += SelectedStation;
        MyEvent.OnClick_dismissBtn += SelectedDismiss;
        //左键取消选中
        MyEvent.OnGridClick_left += SelectGrid_left;
        MyEvent.OnGridClick_right += SelectGrid_right;
        MyEvent.OpenUnitUI?.Invoke(playerStateMachine.selectedUnit);
        MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Player, playerStateMachine.selectedGrid.Value, playerStateMachine.selectedUnit.MoveForce);
        // Debug.Log("unit moveForce:" + playerStateMachine.selectedUnit.MoveForce);
    }
    
    public override void Exit()
    {
        MyEvent.OnClick_restBtn -= SelectedRest;
        MyEvent.OnClick_stationBtn -= SelectedStation;
        MyEvent.OnClick_dismissBtn -= SelectedDismiss;
        MyEvent.OnGridClick_right -= SelectGrid_right;
        MyEvent.OnGridClick_left -= SelectGrid_left;
        MyEvent.OpenUnitUI?.Invoke(null);
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        base.Exit();
    }
    
    private void SelectedAttack()//点击了攻击按钮
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_Enemy);
    }

    private void SelectGrid_left(Vector2Int? coord)
    {
        if (coord == null)
        {
            Cancel();
            return;
        }
        playerStateMachine.selectedUnit = MapManager.Instance.GetUnit(coord.Value , MyEnum.UnitType.City) ?? MapManager.Instance.GetUnit(coord.Value , MyEnum.UnitType.Army);
        if(playerStateMachine.selectedUnit == null || playerStateMachine.selectedUnit.TheOperator != MyEnum.TheOperator.Player)
        {
            Cancel();
            return;
        }
        playerStateMachine.selectedGrid = coord;
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_WhichAction);
    }
    private void SelectGrid_right(Vector2Int? coord)
    {
        if (coord == null)
        {
            Cancel();
            return;
        }
        float moveCost = 0;
        List<Vector2Int> path = MapManager.Instance.GetMovePath(coord.Value ,out moveCost);
        if (path == null)
        {
            Cancel();
            return;
        }
        (playerStateMachine.selectedUnit as IArmy).Move(path , moveCost);
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
    }

    private void SelectedDismiss()
    {
        (playerStateMachine.selectedUnit as IArmy).Dismiss();
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
    }

    private void SelectedStation()
    {
        (playerStateMachine.selectedUnit as IArmy).Station();
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
    }

    private void SelectedRest()
    {
        (playerStateMachine.selectedUnit as IArmy).Rest();
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
    }
    
}