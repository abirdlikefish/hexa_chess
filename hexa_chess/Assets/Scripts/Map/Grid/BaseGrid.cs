using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    private static GridConfigSO gridConfigSO;
    protected static MyStruct.GridConfig GetGridConfig(MyEnum.GridType gridType)
    {
        if (gridConfigSO == null)
        {
            gridConfigSO = Resources.Load<GridConfigSO>("SO/GridConfigSO");
        }
        return gridConfigSO.gridConfigDict[gridType];
    }
    // protected Dictionary<Vector2Int, GameObject> goMap;
    protected Sprite gridPicture;
    public Sprite GridPicture { get { return gridPicture; } }
    // protected GameObject parentGO;
    public MyEnum.GridType gridType;
    public string gridName;
    public float moveCost;
    public int atkOffset;
    public int defOffset;
    public int height;
    public virtual bool Init()
    {
        return Init(MyEnum.GridType.Empty);
    }
    public bool Init(MyEnum.GridType gridType)
    {
        // gridType = MyEnum.GridType.Empty;
        InitConfig(gridType);
        return true;
    }
    public void InitConfig(MyEnum.GridType gridType)
    {
        this.gridType = gridType;
        gridPicture = Resources.Load<Sprite>(MyConst.GridPicturePath[gridType]);
        MyStruct.GridConfig gridConfig = GetGridConfig(gridType);
        gridName = gridConfig.gridName;
        moveCost = gridConfig.moveCost;
        atkOffset = gridConfig.atkOffset;
        defOffset = gridConfig.defOffset;
        height = gridConfig.height;
    }
}
