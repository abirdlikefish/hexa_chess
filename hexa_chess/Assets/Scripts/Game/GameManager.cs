using System;
using Unity.VisualScripting;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameStateMachine gameStateMachine;



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
        if (instance == null)
        {
            instance = this;
        }
        gameStateMachine = new GameStateMachine();
        gameStateMachine.Initializate(gameStateMachine.PlayerRound);
        InitializeAllValue();
    }

    protected void Start()
    {
        
        gameStateMachine.SynchronousHp(PlayerHP, EnemyHP);
    }
    /// <summary>
    /// 判断是否需要结束游戏，每次更新大本营血量的时候都调用
    /// </summary>
    private void JudgeShouldEndGame()
    {
        if (PlayerHP <= 0)
        {
            gameStateMachine.ChangeState(gameStateMachine.GameLose);
        }
        else if (EnemyHP <= 0)
        {
            gameStateMachine.ChangeState(gameStateMachine.GameWin);
        }
    }
    /// <summary>
    /// 如果在玩家环节按下结束键，那么自动跳到敌人环节
    /// </summary>
    public void PressEndRoundButton()
    {
        gameStateMachine.ChangeState(gameStateMachine.EnemyRound);
    }
}