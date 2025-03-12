using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyConst
{
    static MyConst()
    {
        GridSize = 1.0f;
        GridPicturePath = new Dictionary<MyEnum.GridType, string>
        {
            { MyEnum.GridType.Empty, "Sprite/Map/Grid/EmptyGrid" },
            { MyEnum.GridType.Plain, "Sprite/Map/Grid/PlainGrid" },
            { MyEnum.GridType.River, "Sprite/Map/Grid/RiverGrid" },
            { MyEnum.GridType.Road, "Sprite/Map/Grid/RoadGrid" },
            { MyEnum.GridType.Hill, "Sprite/Map/Grid/HillGrid" },
            { MyEnum.GridType.Swamp, "Sprite/Map/Grid/SwampGrid" },
            { MyEnum.GridType.Forest, "Sprite/Map/Grid/ForestGrid" },
            { MyEnum.GridType.Mount, "Sprite/Map/Grid/MountGrid" }
        };
        MoveStep = new Dictionary<MyEnum.MoveDirection, Vector2Int>
        {
            { MyEnum.MoveDirection.Up, new Vector2Int(0, 1) },
            { MyEnum.MoveDirection.Down, new Vector2Int(0, -1) },
            { MyEnum.MoveDirection.LefU, new Vector2Int(-1, 1) },
            { MyEnum.MoveDirection.RigU, new Vector2Int(1, 0) },
            { MyEnum.MoveDirection.LefD, new Vector2Int(-1, 0) },
            { MyEnum.MoveDirection.RigD, new Vector2Int(1, -1) }
        };
        GridUIColor = new Dictionary<MyEnum.GridUIState, Color>
        {
            { MyEnum.GridUIState.Hide, new Color(0, 0, 0, 0) },
            { MyEnum.GridUIState.Legal, new Color(0, 1, 0, 0.5f) },
            { MyEnum.GridUIState.Illegal, new Color(1, 0, 0, 0.5f) },
            { MyEnum.GridUIState.Empty, new Color(1, 1, 1, 0.5f) },
            { MyEnum.GridUIState.HighLight, new Color(1, 1, 1, 0.5f) }
        };
        GridSpriteSortingLayer = new Dictionary<MyEnum.GridSpriteLayer, string>
        {
            { MyEnum.GridSpriteLayer.Base, "Grid_base" },
            { MyEnum.GridSpriteLayer.Fog, "Grid_fog" },
            { MyEnum.GridSpriteLayer.UI, "Grid_ui" }
        };

    }
    public static float GridSize;
    public static Dictionary<MyEnum.GridType , string> GridPicturePath;
    public static Dictionary<MyEnum.MoveDirection, Vector2Int> MoveStep;
    public static Dictionary<MyEnum.GridUIState, Color> GridUIColor;
    public static Dictionary<MyEnum.GridSpriteLayer, string> GridSpriteSortingLayer;
}
