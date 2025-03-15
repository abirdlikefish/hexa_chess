using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRound_WaitInputAction : PlayerRoundState
{
    public PlayerRound_WaitInputAction(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
        MyEvent.OnClick_attackBtn += SelectedAttack;
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.OnGridClick_left += SelectGrid_left;
        MyEvent.OnGridClick_right += SelectGrid_right;
        MyEvent.OpenUnitUI?.Invoke(playerStateMachine.selectedUnit);
MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Player, playerStateMachine.selectedGrid.Value, 5);
        MyEvent.AnimaEnd += EndMove;
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void Exit()
    {
        MyEvent.OnGridClick_right -= SelectGrid_right;
        MyEvent.OnGridClick_left -= SelectGrid_left;
        MyEvent.OpenUnitUI?.Invoke(null);
        MyEvent.AnimaEnd -= EndMove;
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        base.Exit();
    }
    public override void Cancel()
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }

    public override void Update()
    {
        base.Update();
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
        playerStateMachine.selectedUnit = MapManager.Instance.GetUnit(coord.Value);
        if(playerStateMachine.selectedUnit == null || playerStateMachine.selectedUnit.isFriendUnit() == false)
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
playerStateMachine.selectedUnit.Move(5);
    }

    private void EndMove()
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }
    
}