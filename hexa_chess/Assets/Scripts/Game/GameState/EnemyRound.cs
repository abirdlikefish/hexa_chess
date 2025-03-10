using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRound : GameState
{
    public EnemyRound(GameStateMachine _gameStateMachine, Enum.GameState _whichState) : base(_gameStateMachine, _whichState)
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
    
    public override void PressTestButton()
    {
        gameStateMachine.ChangeState(gameStateMachine.EnemyRound);
    }
}
