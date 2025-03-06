using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    public GameState currentState { get; private set; }
    [Header("Game Data")] 
    public int playerCurrentHp;
    public int enemyCurrentHp;
    
    public void Initializate(GameState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    /// <summary>
    /// 同步StateMachine和GameManager里面的数值
    /// </summary>
    /// <param name="newPlayerHp">更新的PlayerHp</param>
    /// <param name="newEnemyHp">更新的EnemyHp</param>
    public void SynchronousHp(int newPlayerHp, int newEnemyHp)
    {
        playerCurrentHp = newPlayerHp;
        enemyCurrentHp = newEnemyHp;
    }

    public void ChangeState(GameState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    
}
