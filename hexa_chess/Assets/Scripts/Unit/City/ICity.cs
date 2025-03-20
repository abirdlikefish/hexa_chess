using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICity : IUnit
{
    int CreateArmyRange { get; }
    MyEnum.CityType CityType { get; }
    List<MyEnum.ArmyType> CreatableArmy { get; }
}
