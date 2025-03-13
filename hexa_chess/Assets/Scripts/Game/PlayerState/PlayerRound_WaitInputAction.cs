using System;
using UnityEngine;


public class PlayerRound_WaitInputAction : PlayerRoundState
{
    public PlayerRound_WaitInputAction(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void Exit()
    {
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

    public void SelectedMove()//点击了移动按钮
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_Position);
    }

    public void SelectedAttack()//点击了攻击按钮
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_Enemy);
    }
}