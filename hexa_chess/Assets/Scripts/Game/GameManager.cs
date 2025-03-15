using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameStateMachine gameStateMachine;

    /// <summary>
    /// 记录回合数
    /// </summary>
    private int roundsCounter;


    [Header("Game Data")] [SerializeField] private int PlayerHP;
    [SerializeField] private int EnemyHP;

    /// <summary>
    /// 初始化所有数值
    /// </summary>
    private void InitializeAllValue()
    {
        PlayerHP = 10;
        EnemyHP = 10;
        roundsCounter = 0;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // gameStateMachine = new GameStateMachine();
        gameStateMachine = GameStateMachine.Instance;

        InitializeAllValue();

        MapManager.Init();
        UIManager.Init();
    }


    protected void Start()
    {
        // gameStateMachine.BuildState();
        // gameStateMachine.Initializate(MyEnum.GameState.PlayerRound);
        gameStateMachine.Initializate();
        gameStateMachine.ChangeState(MyEnum.GameState.PlayerRound);
        gameStateMachine.SynchronousHp(PlayerHP, EnemyHP);


        MapManager.Instance.CreateMap(10);
        UIManager.Instance.ShowView(MyEnum.UIView.PlayView);
        // MyEvent.OnClick_testBtn += ChangeGameState;
    }

    /// <summary>
    /// 判断是否需要结束游戏，每次更新大本营血量的时候都调用
    /// </summary>
    public bool JudgeShouldEndGame()
    {
        if (PlayerHP <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameLose);
            gameStateMachine.ChangeState(MyEnum.GameState.GameLose);
            return true;
        }

        if (EnemyHP <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameWin);
            gameStateMachine.ChangeState(MyEnum.GameState.GameWin);
            return true;
        }

        return false;
    }

    public void Update()
    {
        gameStateMachine.currentState.Update();
    }

    public void IncreaseRoundsCounter()
    {
        roundsCounter++;
    }

    // public void ChangeGameState()
    // {
    //     gameStateMachine.currentState.PressTestButton();
    // }
}