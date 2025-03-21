using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitFactory
{
    UnitConfigListSO unitConfigListSO;

    public UnitConfigListSO UnitConfigListSO { get{ return unitConfigListSO ;} }
    Dictionary<MyEnum.ArmyType , GameObject> armyPrefabList;
    Dictionary<MyEnum.ArmyType , Dictionary<MyEnum.TheOperator , Sprite>> armySpriteList;
    Dictionary<MyEnum.CityType , Dictionary<MyEnum.TheOperator , Sprite>> citySpriteList;
    Dictionary<MyEnum.CityType , GameObject> cityPrefabList;
    Dictionary<MyEnum.ArmyType , int> armyCntList;
    Dictionary<MyEnum.CityType , int> cityCntList;
    Dictionary<MyEnum.TheOperator , Dictionary<MyEnum.UnitType , Transform>> parentGO;
    public UnitFactory()
    {
        unitConfigListSO = Resources.Load<UnitConfigListSO>("SO/UnitConfigListSO");
        // unitPrefabList = new Dictionary<MyEnum.UnitType, GameObject>();
        armyPrefabList = new Dictionary<MyEnum.ArmyType, GameObject>();
        cityPrefabList = new Dictionary<MyEnum.CityType, GameObject>();
        armyCntList = new Dictionary<MyEnum.ArmyType, int>();
        cityCntList = new Dictionary<MyEnum.CityType, int>();
        foreach (var unitConfig in unitConfigListSO.armyConfigList)
        {
            armyPrefabList.Add(unitConfig.armyType , Resources.Load<GameObject>(unitConfig.prefabPath));
        }
        foreach (var unitConfig in unitConfigListSO.cityConfigList)
        {
            cityPrefabList.Add(unitConfig.cityType , Resources.Load<GameObject>(unitConfig.prefabPath));
        }
        foreach (MyEnum.ArmyType armyType in System.Enum.GetValues(typeof(MyEnum.ArmyType)))
        {
            armyCntList.Add(armyType , 0);
        }
        foreach (MyEnum.CityType cityType in System.Enum.GetValues(typeof(MyEnum.CityType)))
        {
            cityCntList.Add(cityType , 0);
        }

        armySpriteList = new Dictionary<MyEnum.ArmyType , Dictionary<MyEnum.TheOperator , Sprite>>();
        foreach (MyEnum.ArmyType armyType in System.Enum.GetValues(typeof(MyEnum.ArmyType)))
        {
            armySpriteList.Add(armyType , new Dictionary<MyEnum.TheOperator , Sprite>());
            foreach (MyEnum.TheOperator theOperator in System.Enum.GetValues(typeof(MyEnum.TheOperator)))
            {
                armySpriteList[armyType].Add(theOperator , Resources.Load<Sprite>("Sprite/Unit/" + armyType.ToString() + "_" + theOperator.ToString()));
            }
        }

        citySpriteList = new Dictionary<MyEnum.CityType , Dictionary<MyEnum.TheOperator , Sprite>>();
        foreach (MyEnum.CityType cityType in System.Enum.GetValues(typeof(MyEnum.CityType)))
        {
            citySpriteList.Add(cityType , new Dictionary<MyEnum.TheOperator , Sprite>());
            foreach (MyEnum.TheOperator theOperator in System.Enum.GetValues(typeof(MyEnum.TheOperator)))
            {
                citySpriteList[cityType].Add(theOperator , Resources.Load<Sprite>("Sprite/Unit/" + cityType.ToString() + "_" + theOperator.ToString()));
            }
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
        armyCntList[armyTypeType]++;
        unit.UnitName = MyConst.ArmyName[armyTypeType] + armyCntList[armyTypeType].ToString();
        unit.transform.GetComponent<SpriteRenderer>().sprite = armySpriteList[armyTypeType][theOperator];
        return unit;
    }
    public Unit LoadUnit(MyEnum.TheOperator theOperator , Vector2Int coord,MyEnum.CityType cityType)
    {
        Vector2 pos = MapManager.Coord_To_Pos(coord);
        MyStruct.UnitConfig unitConfig = unitConfigListSO.GetUnitConfig(cityType);
        Unit unit = GameObject.Instantiate(cityPrefabList[cityType] , parentGO[theOperator][MyEnum.UnitType.City]).GetComponent<Unit>();
        unit.Init(theOperator , unitConfig , coord);
        cityCntList[cityType]++;
        unit.UnitName = MyConst.CityName[cityType] + cityCntList[cityType].ToString();
        unit.transform.GetComponent<SpriteRenderer>().sprite = citySpriteList[cityType][theOperator];
        return unit;
    }
}
