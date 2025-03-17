using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmy : IUnit
{
    MyEnum.ArmyType ArmyType { get; }
    void Move(List<Vector2Int> path,float cost);//移动
    void Station();//驻扎

    void Rest();//休息

    void Dismiss();//解散

    void Skip();//跳过

}
