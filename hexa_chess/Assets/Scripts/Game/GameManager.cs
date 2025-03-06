using System;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameStateMachine gameStateMachine;

    [Header("Game States")] 
    [SerializeField]protected PlayerRound PlayerRound;//玩家回合
    [SerializeField]protected EnemyRound EnemyRound;//敌人回合
    [SerializeField]protected GameWin GameWin;
    [SerializeField]protected GameLose GameLose;

    [Header("Game Data")] 
    [SerializeField] private int PlayerHP;
    [SerializeField] private int EnemyHP;

    /// <summary>
    /// 初始化所有数值
    /// </summary>
    private void InitializeAllValue()
    {
        PlayerHP = 10;
        EnemyHP = 10;
    }
    private void Awake()
    {
        //实例化所有状态
        PlayerRound = new PlayerRound (gameStateMachine,"Player Round");
        EnemyRound = new EnemyRound (gameStateMachine, "Enemy Round");
        GameWin = new GameWin (gameStateMachine, "Game Win");
        GameLose = new GameLose (gameStateMachine, "Game Lose");
        
        gameStateMachine = new GameStateMachine();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected void Start()
    {
        gameStateMachine.Initializate(PlayerRound);
        gameStateMachine.SynchronousHp(PlayerHP, EnemyHP);
    }
    /// <summary>
    /// 判断是否需要结束游戏，每次更新大本营血量的时候都调用
    /// </summary>
    private void JudgeShouldEndGame()
    {
        if (PlayerHP <= 0)
        {
            gameStateMachine.ChangeState(GameLose);
        }
        else if (EnemyHP <= 0)
        {
            gameStateMachine.ChangeState(GameWin);
        }
    }

    public void PressEndRoundButton()
    {
        gameStateMachine.ChangeState(EnemyRound);
    }
}