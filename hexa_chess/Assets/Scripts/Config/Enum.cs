using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enum
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
    }
    public enum TheOperator
    {
        Player,
        Enemy
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

}
