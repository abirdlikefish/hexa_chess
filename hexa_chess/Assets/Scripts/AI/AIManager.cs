using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIManger
{
    public AIPlayer GetAIPlayer();
    /// <summary>
    /// 轮到aiplayer行动
    /// </summary>
    public void Action();
}

public class AIManager : MonoBehaviour,IAIManger
{
    private static AIManager instance;
    public static AIManager Instance
    {
        get => instance;
    }

    private AIPlayer aiPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogError("AIManager has been created");
    }

    public void OnInit()
    {
        aiPlayer = new AIPlayer();
        aiPlayer.OnInit();
    }

    public AIPlayer GetAIPlayer()
    {
        return aiPlayer;
    }

    public void Action()
    {
        aiPlayer.Turn();
    }
}
