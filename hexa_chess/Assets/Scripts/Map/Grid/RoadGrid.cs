using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Road;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 0.5f;
        atkOffset = 0.0f;
        defOffset = 0.0f;
        return true;
    }
}
