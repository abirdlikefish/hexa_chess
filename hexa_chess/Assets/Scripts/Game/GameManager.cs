using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameStateMachine gameStateMachine;
    
    
    [Header("Game Data")] 
    [SerializeField] private int PlayerHP;
    [SerializeField] private int EnemyHP;

    /*
     [Header("Unit Management")]
    [SerializeField] private List<Unit_new> PlayerUnits;//场上所有我方单位的集合
    [SerializeField] private List<Unit_new> EnemyUnits;//场上所有敌方单位的集合
    */
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
        
        InitializeAllValue();

        MapManager.Init();
        UIManager.Init();

    }
    

    protected void Start()
    {
        gameStateMachine.BuildState();
        gameStateMachine.Initializate(MyEnum.GameState.PlayerRound);
        gameStateMachine.SynchronousHp(PlayerHP, EnemyHP);
        
        
        MapManager.Instance.CreateMap(10);
        UIManager.Instance.ShowView(MyEnum.UIView.PlayView);
        MyEvent.OnClick_testBtn += ChangeGameState;
    }
    /// <summary>
    /// 判断是否需要结束游戏，每次更新大本营血量的时候都调用
    /// </summary>
    private void JudgeShouldEndGame()
    {
        if (PlayerHP <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameLose);
            gameStateMachine.ChangeState(MyEnum.GameState.GameLose);
        }
        else if (EnemyHP <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameWin);
            gameStateMachine.ChangeState(MyEnum.GameState.GameWin);
        }
    }

    public void Update()
    {
        gameStateMachine.currentState.Update();
    }

    public void ChangeGameState()
    {
        gameStateMachine.currentState.PressTestButton();
    }
}

