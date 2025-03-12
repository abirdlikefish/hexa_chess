using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "SO/MapSO", order = 1)]
public class MapSO : ScriptableObject
{
    public int grid_x;
    // public List<MyEnum.GridType> grid;
    public MyEnum.GridType[] grid;
    public MyEnum.GridType GetGridType(int x, int y)
    {
        return grid[x * grid_x + y];
    }
    public void SetGridType(int x, int y, MyEnum.GridType type)
    {
        grid[x * grid_x + y] = type;
    }
}
