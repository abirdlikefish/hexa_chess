using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRound : GameState
{
    public EnemyRound(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine, _whichState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.EnemyRoundEnd += EnemyRoundEnd;
        AIManager.Instance.Action(GameManager.instance.CurrentRoundsCounter());
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void PressTestButton()
    {
        // gameStateMachine.ChangeState(gameStateMachine.EnemyRound);
        gameStateMachine.ChangeState(MyEnum.GameState.EnemyRound);
    }

    public void EnemyRoundEnd()
    {
        gameStateMachine.ChangeState(MyEnum.GameState.PlayerRound);
    }
}
