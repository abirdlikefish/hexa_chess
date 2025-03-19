using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIManager
{
    public AIPlayer GetAIPlayer();
    /// <summary>
    /// 轮到aiplayer行动
    /// </summary>
    public void Action(int round);

    /// <summary>
    /// 结束回合
    /// </summary>
    public void EndTurn();
}

public class AIManager : MonoBehaviour,IAIManager
{
    private static AIManager instance;
    public static IAIManager Instance
    {
        get => instance;
    }

    private AIPlayer aiPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            OnInit();
        }
        else
            Debug.LogError("AIManager has been created");
    }

    public void OnInit()
    {
        aiPlayer = new AIPlayer();
        aiPlayer.OnInit();

        MyEvent.OnEnemyRoundBegin += Action;
    }

    public AIPlayer GetAIPlayer()
    {
        return aiPlayer;
    }

    public void Action(int round)
    {
        aiPlayer.Turn(round);
    }

    public void EndTurn()
    {
        MyEvent.EnemyRoundEnd?.Invoke();
    }
}
