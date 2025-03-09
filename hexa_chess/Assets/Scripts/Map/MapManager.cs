using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IMapManager
{
    public bool IsInMap(Vector2Int coord);
    public bool RemoveUnit(Vector2Int coord);
    public bool AddUnit(Vector2Int coord, GameObject unit);
    public bool ChangeControlArea( Enum.TheOperator theOperator , Vector2Int coord, bool isAdd);
    public bool ChangeVirtualField( Enum.TheOperator theOperator , Vector2Int coord, bool isAdd);
    public float GetAtkOffset(Enum.TheOperator theOperator , Vector2Int coord);
    public float GetDefOffset(Enum.TheOperator theOperator , Vector2Int coord);
    public List<Vector2Int> SearchMovableArea(Enum.TheOperator theOperator , Vector2Int coord , float moveForce);
    public void CloseMapUI();
}
public class MapManager : IMapManager
{
    private static MapManager instance;
    public static IMapManager Instance{
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
        GridInfo.Init();
        instance = new MapManager();
        instance.InitGridList();
        instance.CreateMap(10);
    }
    private GridInfo[,] gridMap;
    struct SearchMovableAreaGridInfo
    {
        public Vector2Int lastCoord;
        public float moveCost;
        public Enum.MoveDirection lastMoveDirection;
        public int setFrame;
        public void SetGrid(Vector2Int lastCoord, float moveCost, Enum.MoveDirection lastMoveDirection)
        {
            this.lastCoord = lastCoord;
            this.moveCost = moveCost;
            this.lastMoveDirection = lastMoveDirection;
            setFrame = Time.frameCount;
        }
    }
    private SearchMovableAreaGridInfo[,] searchMovableAreaGridInfoMap; 
    private Dictionary<Enum.GridType, BaseGrid> gridList;
    private int mapSize;

    public void CreateMap(int size)
    {
        mapSize = size;
        CreateMap();
    }
    public void CreateMap()
    {
        gridMap = new GridInfo[mapSize * 2 - 1, mapSize * 2 - 1];
        searchMovableAreaGridInfoMap = new SearchMovableAreaGridInfo[mapSize * 2 - 1, mapSize * 2 - 1];
        for (int x = 0; x < mapSize * 2 - 1; x++)
        {
            for (int y = 0; y < mapSize * 2 - 1; y++)
            {
                if(IsInMap(new Vector2Int(x, y)) )
                {
                    gridMap[x, y] = GridInfo.CreateGrid(gridList[Enum.GridType.Hill], new Vector2Int(x, y));
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
        
        midGrid = new PlainGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Plain, midGrid);

        midGrid = new RiverGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.River, midGrid);

        midGrid = new RoadGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Road, midGrid);

        midGrid = new HillGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Hill, midGrid);

        midGrid = new SwampGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Swamp, midGrid);

        midGrid = new ForestGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Forest, midGrid);

        midGrid = new MountGrid();
        midGrid.Init();
        gridList.Add(Enum.GridType.Mount, midGrid);

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

    // public bool SetGrid(Vector2Int coord, Enum.GridState gridState)
    // {
    //     if(!IsInMap(coord))
    //     {
    //         return false;
    //     }
    //     gridMap[coord.x, coord.y].ChangeState(Enum.TheOperator.Player , gridState);
    //     return true;
    // }
    public bool RemoveUnit(Vector2Int coord)
    {
        if(!IsInMap(coord))
        {
            return false;
        }
        return gridMap[coord.x, coord.y].RemoveUnit();
    }
    public bool AddUnit(Vector2Int coord, GameObject unit)
    {
        if(!IsInMap(coord))
        {
            return false;
        }
        return gridMap[coord.x, coord.y].AddUnit(unit, Enum.TheOperator.Player);
    }
    public bool ChangeControlArea( Enum.TheOperator theOperator , Vector2Int coord, bool isAdd)
    {
        if(!IsInMap(coord))
        {
            return false;
        }
        return gridMap[coord.x, coord.y].ChangeControlArea(theOperator, isAdd);
    }
    public bool ChangeVirtualField( Enum.TheOperator theOperator , Vector2Int coord, bool isAdd)
    {
        if(!IsInMap(coord))
        {
            return false;
        }
        return gridMap[coord.x, coord.y].ChangeVirtualField(theOperator, isAdd);
    }
    public float GetAtkOffset(Enum.TheOperator theOperator , Vector2Int coord) => gridMap[coord.x, coord.y].GetAtkOffset(theOperator);
    public float GetDefOffset(Enum.TheOperator theOperator , Vector2Int coord) => gridMap[coord.x, coord.y].GetDefOffset(theOperator);
    // {
    //     return gridMap[coord.x, coord.y].GetAtkOffset(theOperator);
    // }

    public void CloseMapUI()
    {
        for (int x = 0; x < mapSize * 2 - 1; x++)
        {
            for (int y = 0; y < mapSize * 2 - 1; y++)
            {
                if(gridMap[x, y] != null)
                {
                    gridMap[x, y].ChangeUIState(Enum.GridUIState.Hide);
                }
            }
        }
    }
    private void OpenMapUI()
    {
        for (int x = 0; x < mapSize * 2 - 1; x++)
        {
            for (int y = 0; y < mapSize * 2 - 1; y++)
            {
                if(gridMap[x, y] != null)
                {
                    gridMap[x, y].ChangeUIState(Enum.GridUIState.Empty);
                }
            }
        }
    }
    public List<Vector2Int> SearchMovableArea(Enum.TheOperator theOperator , Vector2Int coord , float moveForce)
    {
        if(!IsInMap(coord))
        {
            Debug.LogError("SearchMovableArea: Out of Map");
            return null;
        }
        OpenMapUI();
        // SortedDictionary<int , Vector2Int> searchQueue = new SortedDictionary<int, Vector2Int>();
        GridHeap searchQueue = new GridHeap();
        searchMovableAreaGridInfoMap[coord.x, coord.y].SetGrid(coord, 0, Enum.MoveDirection.Up);
        searchQueue.Add(new Vector3(0, coord.x, coord.y));
        List<Vector2Int> movableGridList = new List<Vector2Int>();
        while(searchQueue.Count > 0)
        {
            Vector3 mid = searchQueue.Pop();
            float midCost = mid.x;
            Vector2Int midCoord = new Vector2Int((int)mid.y, (int)mid.z);
            if(midCost != searchMovableAreaGridInfoMap[midCoord.x, midCoord.y].moveCost) continue;
            // if(midCost > moveForce) continue;
            movableGridList.Add(midCoord);
            foreach(Enum.MoveDirection moveDirection in Enum.MoveDirection.GetValues(typeof(Enum.MoveDirection)))
            {
                Vector2Int nextCoord = midCoord + Const.MoveStep[moveDirection];
                if(!IsInMap(nextCoord)) continue;
                float nextCost = midCost + gridMap[nextCoord.x, nextCoord.y].GetMoveCost(theOperator);
                if(nextCost > moveForce)  continue;
                if(gridMap[nextCoord.x, nextCoord.y].GetMoveCost(theOperator) < 0)
                {
                    gridMap[nextCoord.x, nextCoord.y].ChangeUIState(Enum.GridUIState.Illegal);
                    continue;
                }
                if(searchMovableAreaGridInfoMap[nextCoord.x, nextCoord.y].setFrame < Time.frameCount || searchMovableAreaGridInfoMap[nextCoord.x, nextCoord.y].moveCost > nextCost)
                {
                    searchMovableAreaGridInfoMap[nextCoord.x, nextCoord.y].SetGrid(midCoord, nextCost, moveDirection);
                    searchQueue.Add(new Vector3(nextCost, nextCoord.x, nextCoord.y));
                    gridMap[nextCoord.x, nextCoord.y].ChangeUIState(Enum.GridUIState.Legal);
                }
            }
        }
        return movableGridList;
    }
    public class GridHeap
    {
        private List<Vector3> heap;
        private void UpModify(int index)
        {
            while(index > 1)
            {
                if(heap[index / 2].x <= heap[index].x)
                {
                    break;
                }
                else
                {
                    Vector3 mid = heap[index / 2];
                    heap[index / 2] = heap[index];
                    heap[index] = mid;
                    index /= 2;
                }
            }
        }
        private void DownModify(int index)
        {
            while(index * 2 < heap.Count)
            {
                float lef, rig;
                if(index * 2 == heap.Count - 1)
                {
                    lef = heap[index * 2].x;
                    rig = 1e9f;
                }
                else
                {
                    lef = heap[index * 2].x;
                    rig = heap[index * 2 + 1].x;
                }
                if(heap[index].x < lef && heap[index].x < rig)
                {
                    break;
                }
                else
                {
                    if(lef < rig)
                    {
                        Vector3 mid = heap[index];
                        heap[index] = heap[index * 2];
                        heap[index * 2] = mid;
                        index = index * 2;
                    }
                    else
                    {
                        Vector3 mid = heap[index];
                        heap[index] = heap[index * 2 + 1];
                        heap[index * 2 + 1] = mid;
                        index = index * 2 + 1;
                    }
                }
            }
        }
        public void Add(Vector3 value)
        {
            heap.Add(value);
            UpModify(heap.Count - 1);
        }
        public Vector3 Pop()
        {
            Vector3 ret = heap[1];
            heap[1] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            DownModify(1);
            return ret;
        }
        public Vector3 Top()
        {
            return heap[1];
        }   
        public GridHeap()
        {
            heap = new List<Vector3>();
            heap.Add(new Vector3(0, 0, 0));
        }
        public int Count => heap.Count - 1;
    }
    


}

