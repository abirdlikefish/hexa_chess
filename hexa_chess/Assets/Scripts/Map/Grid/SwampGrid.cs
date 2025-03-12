using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = MyEnum.GridType.Swamp;
        gridPicture = Resources.Load<Sprite>(MyConst.GridPicturePath[gridType]);
        moveCost = 1.0f;
        atkOffset = 0.0f;
        defOffset = -1.0f;
        return true;
    }
}
