using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private static PlayerStateMachine instance;

    public static PlayerStateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerStateMachine();
            }

            return instance;
        }
    }
    public PlayerRoundState currentState { get; private set; } = null;
    private Dictionary<MyEnum.PlayerRoundState, PlayerRoundState> stateList = null;

    public void BuildState()
    {
        if (stateList == null)
        {
            stateList = new Dictionary<MyEnum.PlayerRoundState, PlayerRoundState>();
        }
        stateList.Add(MyEnum.PlayerRoundState.Idle, new PlayerRound_IdleState(this,MyEnum.PlayerRoundState.Idle));
        stateList.Add(MyEnum.PlayerRoundState.WaitInput_WhichAction, new PlayerRound_WaitInputAction(this,MyEnum.PlayerRoundState.WaitInput_WhichAction));
    }

    public void Initialize(MyEnum.PlayerRoundState _startState)
    {
        currentState = null;
        ChangeState(_startState);
    }

    public void ChangeState(MyEnum.PlayerRoundState _newState)
    {
        currentState.Exit();
        currentState = stateList[_newState];
        currentState.Enter();
    }
    
}