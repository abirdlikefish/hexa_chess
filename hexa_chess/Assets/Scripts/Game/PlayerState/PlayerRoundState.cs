using Unity.VisualScripting;
using UnityEngine;

public class PlayerRoundState
{
    public PlayerStateMachine playerStateMachine;
    public MyEnum.PlayerRoundState playerState;

    public PlayerRoundState(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState)
    {
        playerStateMachine = _playerStateMachine;
        playerState = _playerState;
    }

    public virtual void Enter()
    {
        Debug.Log("Now in PlayerRound, State is: " + playerState);
    }

    public virtual void ShowUI()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Cansel()
    {
    }

    public virtual void Update()
    {
    }

    
}