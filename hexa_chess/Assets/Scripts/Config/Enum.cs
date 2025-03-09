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

}
