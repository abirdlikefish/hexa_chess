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

    [Header("Unit Management")]
    [SerializeField] private List<Unit> PlayerUnits;//场上所有我方单位的集合
    [SerializeField] private List<Unit> EnemyUnits;//场上所有敌方单位的集合
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

    /// <summary>
    /// 当我们创建友方单位的时候，把他加入GameManage的List里面管理
    /// </summary>
    /// <param name="the new player unit we build"></param>
    public void AddUnitIntoPlayerUnits(Unit newPlayerUnit)
    {
        PlayerUnits.Add(newPlayerUnit);
    }
    
    /// <summary>
    /// 当友方单位寄了，需要在List中删除
    /// </summary>
    /// <param name="The player unit which has diec"></param>
    public void RemoveUnitInPlayerUnits(Unit playerUnit)
    {
        PlayerUnits.Remove(playerUnit);
    }
}