using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Hill;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = 1.5f;
        atkOffset = 1.0f;
        defOffset = 1.0f;
        return true;
    }
}
