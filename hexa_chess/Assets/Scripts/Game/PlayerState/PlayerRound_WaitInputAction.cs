using System;
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
        MyEvent.OnGridClick_left += SelectGrid;
        MyEvent.OpenUnitUI?.Invoke(playerStateMachine.selectedUnit);
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void Exit()
    {
        MyEvent.OnGridClick_left -= SelectGrid;
        MyEvent.OpenUnitUI?.Invoke(null);
        base.Exit();
    }
    public override void Cansel()
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }

    public override void Update()
    {
        base.Update();
    }

    private void SelectedMove()//点击了移动按钮
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_Position);
    }

    private void SelectedAttack()//点击了攻击按钮
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_Enemy);
    }

    private void SelectGrid(Vector2Int? coord)
    {
        if (coord == null)
        {
            Cansel();
            return;
        }
        playerStateMachine.selectedUnit = MapManager.Instance.GetUnit(coord.Value);
        if(playerStateMachine.selectedUnit == null)
        {
            Cansel();
            return;
        }
        playerStateMachine.selectedGrid = coord;
        MyEvent.OpenUnitUI?.Invoke(playerStateMachine.selectedUnit);
    }
    
}