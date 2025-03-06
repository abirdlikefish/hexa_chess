using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    protected Dictionary<Vector2Int, GameObject> goMap;
    protected GameObject gridPicture;
    protected GameObject parentGO;
    public Enum.GridType gridType;
    public virtual bool Init()
    {
        gridType = Enum.GridType.Empty;
        goMap = new Dictionary<Vector2Int, GameObject>();
        gridPicture = Resources.Load<GameObject>(Const.GridPicturePath[gridType]);
        return true;
    }

    public virtual bool AddGrid(Vector2Int coord)
    {
        if(goMap.ContainsKey(coord))
        {
            return false;
        }
        GameObject go = GameObject.Instantiate(gridPicture , parentGO.transform);
        go.transform.position = MapManager.Coord_To_Pos(coord);
        goMap.Add(coord, go);
        return true;
    }
    public virtual bool RemoveGrid(Vector2Int coord)
    {
        if(goMap.ContainsKey(coord))
        {
            GameObject.Destroy(goMap[coord]);
            goMap.Remove(coord);
            return true;
        }
        return false;
    }
    public virtual bool IsEmpty(Vector2Int coord)
    {
        return !goMap.ContainsKey(coord);
    }
}
