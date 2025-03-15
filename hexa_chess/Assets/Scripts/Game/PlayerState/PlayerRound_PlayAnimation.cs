using UnityEngine;

public class PlayerRound_PlayAnimation : PlayerRoundState
{
    public PlayerRound_PlayAnimation(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.AnimaEnd += EndAnimation;
    }

    public override void Exit()
    {
        base.Exit();
        MyEvent.AnimaEnd -= EndAnimation;
    }

    private void EndAnimation()
    {
        GameManager.instance.JudgeShouldEndGame();
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }
}