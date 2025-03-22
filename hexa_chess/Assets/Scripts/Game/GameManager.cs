using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public GlobalSettingSO globalSettingSO;
    public static GameManager instance = null;
    public GameStateMachine gameStateMachine;
    /// <summary>
    /// 每回合回复的金币数量
    /// </summary>
    // private int CurrentCost;

    /// <summary>
    /// 记录回合数
    /// </summary>
    private int roundsCounter;

    /// <summary>
    /// 单位数量上限
    /// </summary>
    public int maxUnitNumber = 0;
    
    public int CurrentRoundsCounter() => roundsCounter;


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
        globalSettingSO = Resources.Load<GlobalSettingSO>("SO/GlobalSettingSO");
        // CurrentCost = 0;
        // maxUnitNumber = 10;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        InitializeAllValue();
        UnitManager.Init();
        MapManager.Init();
        UIManager.Init();
        // gameStateMachine = new GameStateMachine();

        gameStateMachine = GameStateMachine.Instance;



    }


    protected void Start()
    {
        MapManager.Instance.CreateMap(GameManager.instance.globalSettingSO.mapSize);
        UIManager.Instance.ShowView(MyEnum.UIView.PlayView);
        UIManager.Instance.ShowWin(MyEnum.UIWin.GridInfoWin, true);

        gameStateMachine.Initialize();
        gameStateMachine.ChangeState(MyEnum.GameState.PlayerRound);
        gameStateMachine.SynchronousHp(PlayerHP, EnemyHP);


        MyEvent.SetGlobalInfo?.Invoke(new Vector2(UnitManager.Instance.GetCoin(MyEnum.TheOperator.Player), GameManager.instance.globalSettingSO.Income),
                                new Vector2(UnitManager.Instance.GetPopulation(MyEnum.TheOperator.Player), GameManager.instance.globalSettingSO.InitialPopulation),
                                new Vector2(GameManager.instance.CurrentRoundsCounter(), 100));


        // MyEvent.OnClick_testBtn += ChangeGameState;
    }

    /// <summary>
    /// 判断是否需要结束游戏，每次更新大本营血量的时候都调用
    /// </summary>
    public bool JudgeShouldEndGame()
    {
        Debug.Log("JudgeShouldEndGame");
        //todo: 这边要改一下，要把Base的血量同步过来
        if (UnitManager.Instance.GetHomeHP(MyEnum.TheOperator.Player) <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameLose);
            gameStateMachine.ChangeState(MyEnum.GameState.GameLose);
            Debug.Log("Game Lose");
            return true;
        }

        if (UnitManager.Instance.GetHomeHP(MyEnum.TheOperator.Enemy) <= 0)
        {
            // gameStateMachine.ChangeState(gameStateMachine.GameWin);
            gameStateMachine.ChangeState(MyEnum.GameState.GameWin);
            Debug.Log("Game Win");
            return true;
        }

        return false;
    }

    public void Update()
    {
        gameStateMachine.currentState.Update();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogWarning("quit game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit()
#endif
        }
    }

    public void IncreaseRoundsCounter()
    {
        roundsCounter++;
        Debug.Log($"roundsCounter:{roundsCounter}");
        MyEvent.SetGlobalInfo?.Invoke(new Vector2(UnitManager.Instance.GetCoin(MyEnum.TheOperator.Player),GameManager.instance.globalSettingSO.Income) , 
                                new Vector2(UnitManager.Instance.GetPopulation(MyEnum.TheOperator.Player),GameManager.instance.globalSettingSO.InitialPopulation) , 
                                new Vector2(GameManager.instance.CurrentRoundsCounter(),100));
    }
    
    

    // public void ChangeGameState()
    // {
    //     gameStateMachine.currentState.PressTestButton();
    // }
}