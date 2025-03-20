using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICity : IUnit
{
    MyEnum.CityType CityType { get; }
    List<MyEnum.ArmyType> CreatableArmy { get; }
}
