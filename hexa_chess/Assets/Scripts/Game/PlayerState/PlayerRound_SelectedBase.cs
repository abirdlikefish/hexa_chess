using UnityEngine;

public class PlayerRound_SelectedBase : PlayerRoundState
{
    public PlayerRound_SelectedBase(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CreateUnit()
    {
        //todo:创造Unit
    }

    private void PressSkip()
    {
        
    }
}