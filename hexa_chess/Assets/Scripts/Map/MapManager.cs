using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private static MapManager instance;
    public static MapManager Instance{
        get{
            if(instance == null)
            {
                MapManager.Init();
            }
            return instance;
        }
    }
    public static void Init()
    {
        instance = new MapManager();
        instance.InitGridList();
        instance.CreateMap(10);
    }
    private BaseGrid[,] gridMap;
    private Dictionary<Enum.GridType, BaseGrid> gridList;
    private int mapSize;

    public void CreateMap(int size)
    {
        mapSize = size;
        CreateMap();
    }
    public void CreateMap()
    {
        gridMap = new BaseGrid[mapSize * 2 - 1, mapSize * 2 - 1];
        for (int x = 0; x < mapSize * 2 - 1; x++)
        {
            for (int y = 0; y < mapSize * 2 - 1; y++)
            {
                if(IsInMap(new Vector2Int(x, y)) )
                {
                    gridMap[x, y] = gridList[Enum.GridType.Grass];
                    gridList[Enum.GridType.Grass].AddGrid(new Vector2Int(x, y));
                }

                // if(x + y < mapSize - 1 || x + y > mapSize * 2 - 1 - 1 + mapSize - 1)
                // {
                //     gridMap[x, y] = gridList[Enum.GridType.Empty];
                //     gridList[Enum.GridType.Empty].AddGrid(new Vector2Int(x, y));
                // }
                // else
                // {
                //     gridMap[x, y] = gridList[Enum.GridType.Grass];
                //     gridList[Enum.GridType.Grass].AddGrid(new Vector2Int(x, y));
                // }
            }
        }
    }

    public void InitGridList()
    {
        gridList = new Dictionary<Enum.GridType, BaseGrid>();
        BaseGrid midGrid ;

        midGrid = new EmptyGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Empty, midGrid);
        
        midGrid = new GrassGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Grass, midGrid);
    }
    public static Vector2Int Pos_To_Coord(Vector2 pos)
    {
        float x = pos.x / Const.GridSize;
        float y = pos.y / Const.GridSize;
        x *= Mathf.Tan(30 * Mathf.Deg2Rad);
        y -= x;
        x = x / Mathf.Sin(30 * Mathf.Deg2Rad);
        return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    } 
    public static Vector2 Coord_To_Pos(Vector2Int coord)
    {
        float x = coord.x * Mathf.Sin(30 * Mathf.Deg2Rad);
        float y = coord.y + x;
        x /= Mathf.Tan(30 * Mathf.Deg2Rad);
        return new Vector2(x, y) * Const.GridSize;
    }
    public bool IsInMap(Vector2Int coord)
    {
        // x + y < mapSize - 1 || x + y > mapSize * 2 - 1 - 1 + mapSize - 1
        return  coord.x >= 0 && coord.x < mapSize * 2 - 1 &&
                coord.y >= 0 && coord.y < mapSize * 2 - 1 &&
                coord.x + coord.y >= mapSize - 1 && coord.x + coord.y <= mapSize * 3 - 3;
    }

    public bool SetGrid(Vector2Int coord, Enum.GridType gridType)
    {
        if(!IsInMap(coord))
        {
            return false;
        }
        if(gridMap[coord.x, coord.y].gridType == gridType)
        {
            return false;
        }
        gridMap[coord.x, coord.y].RemoveGrid(coord);
        gridMap[coord.x, coord.y] = gridList[gridType];
        gridMap[coord.x, coord.y].AddGrid(coord);
        return true;
    }

}

