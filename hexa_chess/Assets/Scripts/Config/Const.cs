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
            { Enum.GridType.Empty, "Sprite/Map/Grid/EmptyGrid" },
            { Enum.GridType.Plain, "Sprite/Map/Grid/PlainGrid" },
            { Enum.GridType.River, "Sprite/Map/Grid/RiverGrid" },
            { Enum.GridType.Road, "Sprite/Map/Grid/RoadGrid" },
            { Enum.GridType.Hill, "Sprite/Map/Grid/HillGrid" },
            { Enum.GridType.Swamp, "Sprite/Map/Grid/SwampGrid" },
            { Enum.GridType.Forest, "Sprite/Map/Grid/ForestGrid" },
            { Enum.GridType.Mount, "Sprite/Map/Grid/MountGrid" }
        };
        MoveStep = new Dictionary<Enum.MoveDirection, Vector2Int>
        {
            { Enum.MoveDirection.Up, new Vector2Int(0, 1) },
            { Enum.MoveDirection.Down, new Vector2Int(0, -1) },
            { Enum.MoveDirection.LefU, new Vector2Int(-1, 1) },
            { Enum.MoveDirection.RigU, new Vector2Int(1, 0) },
            { Enum.MoveDirection.LefD, new Vector2Int(-1, 0) },
            { Enum.MoveDirection.RigD, new Vector2Int(1, -1) }
        };
        GridUIColor = new Dictionary<Enum.GridUIState, Color>
        {
            { Enum.GridUIState.Hide, new Color(0, 0, 0, 0) },
            { Enum.GridUIState.Legal, new Color(0, 1, 0, 0.5f) },
            { Enum.GridUIState.Illegal, new Color(1, 0, 0, 0.5f) },
            { Enum.GridUIState.Empty, new Color(1, 1, 1, 0.5f) }
        };
        GridSpriteSortingLayer = new Dictionary<Enum.GridSpriteLayer, string>
        {
            { Enum.GridSpriteLayer.Base, "Grid_base" },
            { Enum.GridSpriteLayer.Fog, "Grid_fog" },
            { Enum.GridSpriteLayer.UI, "Grid_ui" }
        };

    }
    public static float GridSize;
    public static Dictionary<Enum.GridType , string> GridPicturePath;
    public static Dictionary<Enum.MoveDirection, Vector2Int> MoveStep;
    public static Dictionary<Enum.GridUIState, Color> GridUIColor;
    public static Dictionary<Enum.GridSpriteLayer, string> GridSpriteSortingLayer;
}
