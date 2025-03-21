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

    /// <summary>
    /// 停止回合
    /// </summary>
    public void Stop();
    public float AIOperatorationInterval { get; }
}

public class AIManager : MonoBehaviour,IAIManager
{
    private static AIManager instance;
    public static IAIManager Instance
    {
        get => instance;
    }

    public float AIOperatorationInterval => GameManager.instance.globalSettingSO.AIOperatorationInterval;

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
        var ai = Resources.Load<AICreateUnitSO>("SO/AICreateUnitSO");
        aiPlayer = new AIPlayer(ai);
        aiPlayer.OnInit();

        MyEvent.OnEnemyRoundBegin += Action;
    }

    public AIPlayer GetAIPlayer()
    {
        return aiPlayer;
    }
    public void Action(int round)
    {
         StartCoroutine(aiPlayer.Turn(round));
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    public void EndTurn()
    {
        MyEvent.EnemyRoundEnd?.Invoke();
    }
}
