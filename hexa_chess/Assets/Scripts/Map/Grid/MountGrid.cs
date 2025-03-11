using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountGrid : BaseGrid
{
    public override bool Init()
    {
        gridType = Enum.GridType.Mount;
        gridPicture = Resources.Load<Sprite>(Const.GridPicturePath[gridType]);
        moveCost = -1;
        atkOffset = 0.0f;
        defOffset = 0.0f;
        return true;
    }
}
