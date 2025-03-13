using UnityEngine;

public class PlayerRound_IdleState : PlayerRoundState
{
    public PlayerRound_IdleState(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState):base(_playerStateMachine, _playerState)
    {
    }
    
    
    public override void Enter()
    {
        Debug.Log("Now in PlayerRound, State is: " + playerState);
    }

    public override void ShowUI()
    {
        
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if (MyEvent.OnGridClick_left != null)
        {
            
        }
    }
}