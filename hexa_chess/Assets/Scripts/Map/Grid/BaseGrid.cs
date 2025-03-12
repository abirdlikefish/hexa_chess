using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    // protected Dictionary<Vector2Int, GameObject> goMap;
    protected Sprite gridPicture;
    public Sprite GridPicture { get { return gridPicture; } }
    // protected GameObject parentGO;
    public Enum.GridType gridType;
    public float moveCost;
    public float atkOffset;
    public float defOffset;
    public virtual bool Init()
    {
        gridType = Enum.GridType.Empty;
        return true;
    }
}
