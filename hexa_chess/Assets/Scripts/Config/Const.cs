using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
    static Const()
    {
        GridSize = 1.0f;
        GridPicturePath = new Dictionary<Enum.GridType, string>
        {
            { Enum.GridType.Empty, "Prefabs/Grid/EmptyGrid" },
            { Enum.GridType.Grass, "Prefabs/Grid/GrassGrid" }
        };
    }
    public static float GridSize;
    public static Dictionary<Enum.GridType , string> GridPicturePath; 
}
