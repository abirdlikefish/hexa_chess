using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGrid : BaseGrid
{
    public override bool Init()
    {
        parentGO = new GameObject(Enum.GridType.Empty.ToString());
        gridType = Enum.GridType.Empty;
        goMap = new Dictionary<Vector2Int, GameObject>();
        gridPicture = Resources.Load<GameObject>(Const.GridPicturePath[gridType]);
        return true;
    }
}
