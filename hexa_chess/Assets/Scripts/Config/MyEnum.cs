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
    /// 三种单位：步兵，炮兵，坦克
    /// </summary>
    public enum UnitType
    {
        Infantry,
        Artillery,
        Tank,
        Base,
    }

    public enum UnitStates
    {
        Able,//可操作
        Disable,//不可操作
    }

    public enum OprationBuff
    {
        Normal,//常态
        Station,//驻扎
        Rest,//休整
    }

    public enum PlayerRoundState
    {
        Idle,//默认等待
        WaitInput_WhichAction,//选中兵之后等待选哪种行动
        WaitInput_Enemy,//选中攻击的话需要选打谁
        PlayingAnimation,//正在播放对应的动作动画
        
        SelectedFactory,//选的不是常规单位而是生产单位的工厂
    }

}
