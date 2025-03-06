using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrid : BaseGrid
{
    public override bool Init()
    {
        parentGO = new GameObject(Enum.GridType.Grass.ToString());
        gridType = Enum.GridType.Grass;
        goMap = new Dictionary<Vector2Int, GameObject>();
        gridPicture = Resources.Load<GameObject>(Const.GridPicturePath[gridType]);
        return true;
    }
}
