using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    // private static PlayerStateMachine instance;

    // public static PlayerStateMachine Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = new PlayerStateMachine();
    //         }

    //         return instance;
    //     }
    // }
    public PlayerRoundState currentState { get; private set; } = null;
    private Dictionary<MyEnum.PlayerRoundState, PlayerRoundState> stateList = null;
    public Vector2Int? selectedGrid;
    public IUnit selectedUnit;

    // public void BuildState()
    private void BuildState()
    {
        if (stateList == null)
        {
            stateList = new Dictionary<MyEnum.PlayerRoundState, PlayerRoundState>();
        }
        stateList.Add(MyEnum.PlayerRoundState.Idle, new PlayerRound_IdleState(this,MyEnum.PlayerRoundState.Idle));
        stateList.Add(MyEnum.PlayerRoundState.WaitInput_WhichAction, new PlayerRound_WaitInputAction(this,MyEnum.PlayerRoundState.WaitInput_WhichAction));
        stateList.Add(MyEnum.PlayerRoundState.WaitInput_Enemy, new PlayerRound_WaitInput_Enemy(this,MyEnum.PlayerRoundState.WaitInput_Enemy));
    }

    // public void Initialize(MyEnum.PlayerRoundState _startState)
    public void Initialize()
    {
        // currentState = null;
        // ChangeState(_startState);
        BuildState();
    }

    public void ChangeState(MyEnum.PlayerRoundState _newState)
    {
        currentState?.Exit();
        currentState = stateList[_newState];
        currentState.Enter();
    }
    public void Exit()
    {
        currentState?.Exit();
        currentState = null;
    }
    
}