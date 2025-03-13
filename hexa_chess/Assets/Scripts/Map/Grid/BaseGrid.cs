using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    // protected Dictionary<Vector2Int, GameObject> goMap;
    protected Sprite gridPicture;
    public Sprite GridPicture { get { return gridPicture; } }
    // protected GameObject parentGO;
    public MyEnum.GridType gridType;
    public float moveCost;
    public float atkOffset;
    public float defOffset;
    public int height;
    public virtual bool Init()
    {
        gridType = MyEnum.GridType.Empty;
        return true;
    }
}
