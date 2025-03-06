using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRound : GameState
{
    public EnemyRound(GameStateMachine _gameStateMachine, string _stateName) : base(_gameStateMachine, _stateName)
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

    public IEnumerator TestForStateMachine()
    {
        Debug.Log("Enemies are in action");
        yield return new WaitForSeconds(3.0f);
    }
}
