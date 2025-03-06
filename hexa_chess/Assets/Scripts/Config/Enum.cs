using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enum
{
    public enum GridType
    {
        Empty,
        Grass
    }

    public enum GameState
    {
        PlayerRound,
        EnemyRound,
        GameWin,
        GameLose,
    }

}
