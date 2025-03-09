using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Swamp;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 1.0f;
        atkOffset = 0.0f;
        defOffset = -1.0f;
        return true;
    }
}
