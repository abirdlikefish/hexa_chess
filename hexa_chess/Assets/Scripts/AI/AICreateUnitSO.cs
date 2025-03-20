using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEnum;
[CreateAssetMenu(fileName = "AICreateUnitSO", menuName = "ScriptableObject/AICreateUnitSO")]
public class AICreateUnitSO : ScriptableObject
{
    public List<AICreateUnit> aiCreateUnits;
}
[Serializable]
public class AICreateUnit
{
    public int round;
    public UnitType unitType;
    public ArmyType armyType;
    public CityType cityType;
    public int count;
    public Vector2Int createPos;
}
