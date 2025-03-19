using UnityEngine;

public class PlayerRound_SelectedBase : PlayerRoundState
{
    public PlayerRound_SelectedBase(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

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

    private void CreateUnit()
    {
        //todo:创造Unit
    }

    private void PressSkip()
    {
        
    }
}