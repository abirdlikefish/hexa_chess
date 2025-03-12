using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Empty;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 1.0f;
        atkOffset = 0.0f;
        defOffset = 0.0f;
        return true;
    }
}
