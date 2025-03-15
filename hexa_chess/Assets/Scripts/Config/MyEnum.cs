using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEnum
{
    public enum GridType
    {
        Empty,
        Plain,
        River,
        Road,
        Hill,
        Swamp,
        Forest,
        Mount
    }
    public enum MoveDirection
    {
        Up,
        Down,
        LefU,
        RigU,
        LefD,
        RigD
    }
    public enum GridSpriteLayer
    {
        Base,
        Fog,
        UI,
    }
    public enum GridState
    {
        UnInit,
        Fog,
        Show,
        Hide
    }
    public enum GridUIState
    {
        Hide,
        Legal,
        Illegal,
        Empty,
        HighLight,
    }
    public enum TheOperator
    {
        Player,
        Enemy
    }
    public enum UIView
    {
        Empty,
        PlayView,
        MapEditView,
    }
    public enum GameState
    {
        PlayerRound,
        EnemyRound,
        GameWin,
        GameLose
    }

    /// <summary>
    /// 有三种单位：步兵，炮兵，坦克
    /// </summary>
    public enum UnitType
    {
        Infantry,
        Artillery,
        Tank
    }

    public enum UnitStates
    {
        Action,//到自己的回合
        Move,//移动
        Attack,//攻击
        Waiting,//到别人的回合
        Die,//死了
    }

    public enum PlayerRoundState
    {
        Idle,//默认等待
        WaitInput_WhichAction,//选中兵之后等待选哪种行动
        WaitInput_Enemy,//选中攻击的话需要选打谁
        UnitActing,//兵在行动
        
        SelectedFactory,//选的不是常规单位而是生产单位的工厂
    }

}
