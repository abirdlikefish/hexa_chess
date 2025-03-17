using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitFactory
{
    UnitConfigListSO unitConfigListSO;
    Dictionary<MyEnum.ArmyType , GameObject> armyPrefabList;
    Dictionary<MyEnum.CityType , GameObject> cityPrefabList;
    Dictionary<MyEnum.TheOperator , Dictionary<MyEnum.UnitType , Transform>> parentGO;
    public UnitFactory()
    {
        unitConfigListSO = Resources.Load<UnitConfigListSO>("SO/UnitConfigListSO");
        // unitPrefabList = new Dictionary<MyEnum.UnitType, GameObject>();
        armyPrefabList = new Dictionary<MyEnum.ArmyType, GameObject>();
        cityPrefabList = new Dictionary<MyEnum.CityType, GameObject>();
        foreach (var unitConfig in unitConfigListSO.armyConfigList)
        {
            armyPrefabList.Add(unitConfig.armyType , Resources.Load<GameObject>(unitConfig.prefabPath));
        }
        foreach (var unitConfig in unitConfigListSO.cityConfigList)
        {
            cityPrefabList.Add(unitConfig.cityType , Resources.Load<GameObject>(unitConfig.prefabPath));
        }
        parentGO = new Dictionary<MyEnum.TheOperator , Dictionary<MyEnum.UnitType , Transform>>();
        foreach (MyEnum.TheOperator theOperator in System.Enum.GetValues(typeof(MyEnum.TheOperator)))
        {
            parentGO.Add(theOperator , new Dictionary<MyEnum.UnitType , Transform>());
            Transform midParent = new GameObject(theOperator.ToString() + " unit").transform;
            foreach (MyEnum.UnitType unitType in System.Enum.GetValues(typeof(MyEnum.UnitType)))
            {
                GameObject unitGO = new GameObject(unitType.ToString());
                unitGO.transform.parent = midParent;
                parentGO[theOperator].Add(unitType, unitGO.transform);
            }
        }
    }

    public Unit LoadUnit(MyEnum.TheOperator theOperator , Vector2Int coord,MyEnum.ArmyType armyTypeType)
    {
        Vector2 pos = MapManager.Coord_To_Pos(coord);
        MyStruct.UnitConfig unitConfig = unitConfigListSO.GetUnitConfig(armyTypeType);
        Unit unit = GameObject.Instantiate(armyPrefabList[armyTypeType] , parentGO[theOperator][MyEnum.UnitType.Army]).GetComponent<Unit>();
        unit.Init(theOperator , unitConfig , coord);
        return unit;
    }
    public Unit LoadUnit(MyEnum.TheOperator theOperator , Vector2Int coord,MyEnum.CityType cityType)
    {
        Vector2 pos = MapManager.Coord_To_Pos(coord);
        MyStruct.UnitConfig unitConfig = unitConfigListSO.GetUnitConfig(cityType);
        Unit unit = GameObject.Instantiate(cityPrefabList[cityType] , parentGO[theOperator][MyEnum.UnitType.City]).GetComponent<Unit>();
        unit.Init(theOperator , unitConfig , coord);
        return unit;
    }
}
