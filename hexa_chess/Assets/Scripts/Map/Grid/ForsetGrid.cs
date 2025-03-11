using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Forest;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 2.0f;
        atkOffset = 0.0f;
        defOffset = 1.0f;
        return true;
    }
}
