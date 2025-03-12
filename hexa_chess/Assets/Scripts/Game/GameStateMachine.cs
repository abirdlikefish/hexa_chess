using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private static GameStateMachine instance;

    public static GameStateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameStateMachine();
            }

            return instance;
        }
    }

    public GameState currentState { get; private set; } = null;

    [Header("Game Data")] 
    public int playerCurrentHp;
    public int enemyCurrentHp;
    
    [Header("Game States")] 
    public PlayerRound PlayerRound; //玩家回合
    public EnemyRound EnemyRound; //敌人回合
    public GameWin GameWin;
    public GameLose GameLose;
    
    public void Initializate(GameState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    
    /// <summary>
    /// 实例化所有Game State
    /// </summary>
    public void BuildState()
    {
        PlayerRound = new PlayerRound(this, Enum.GameState.PlayerRound);
        EnemyRound = new EnemyRound(this, Enum.GameState.EnemyRound);
        GameWin = new GameWin(this, Enum.GameState.GameWin);
        GameLose = new GameLose(this, Enum.GameState.GameLose);
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