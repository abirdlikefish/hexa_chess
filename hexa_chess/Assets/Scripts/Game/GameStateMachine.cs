using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private GameStateMachine(){}
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
    private Dictionary<MyEnum.GameState, GameState> stateList;

    [Header("Game Data")] 
    public int playerCurrentHp;
    public int enemyCurrentHp;
    
    // public void Initializate(MyEnum.GameState startState)
    public void Initializate()
    {
        // currentState = null;
        // ChangeState(startState);
        BuildState();
    }
    
    /// <summary>
    /// 实例化所有Game State
    /// </summary>
    // public void BuildState()
    private void BuildState()
    {
        stateList = new Dictionary<MyEnum.GameState, GameState>();
        stateList.Add(MyEnum.GameState.PlayerRound, new PlayerRound(this, MyEnum.GameState.PlayerRound));
        stateList.Add(MyEnum.GameState.EnemyRound, new EnemyRound(this, MyEnum.GameState.EnemyRound));
        stateList.Add(MyEnum.GameState.GameWin, new GameWin(this, MyEnum.GameState.GameWin));
        stateList.Add(MyEnum.GameState.GameLose, new GameLose(this, MyEnum.GameState.GameLose));
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
    

    // public void ChangeState(GameState newState)
    public void ChangeState(MyEnum.GameState newState)
    {
        currentState?.Exit();
        currentState = stateList[newState];
        currentState.Enter();
    }
}