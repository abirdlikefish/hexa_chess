using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Plain;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 1.0f;
        atkOffset = 0.0f;
        defOffset = 0.0f;
        return true;
    }
}
