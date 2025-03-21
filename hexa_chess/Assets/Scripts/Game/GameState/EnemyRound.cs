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
        MyEvent.AnimaEnd += EndAnimation;
        MyEvent.EnemyRoundEnd += EnemyRoundEnd;
        UnitManager.Instance.RoundBeginOperation(MyEnum.TheOperator.Enemy);
        AIManager.Instance.Action(GameManager.instance.CurrentRoundsCounter());
    }

    public override void Exit()
    {
        base.Exit();
        AIManager.Instance.Stop();
        MyEvent.AnimaEnd -= EndAnimation;
        MyEvent.EnemyRoundEnd -= EnemyRoundEnd;
        GameManager.instance.IncreaseRoundsCounter();
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

    private void EndAnimation()
    {
        GameManager.instance.JudgeShouldEndGame();
    }
}
