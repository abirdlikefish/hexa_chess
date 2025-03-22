using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmy : IUnit
{
    MyEnum.ArmyType ArmyType { get; }
    bool Move(List<Vector2Int> path,float cost);//移动
    bool Station();//驻扎

    bool Rest();//休息

    bool Dismiss();//解散

    bool Skip();//跳过


}
