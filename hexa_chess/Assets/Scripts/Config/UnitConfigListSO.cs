using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfigListSO", menuName = "SO/UnitConfigListSO")]
public class UnitConfigListSO : ScriptableObject , ISerializationCallbackReceiver
{
    public MyStruct.UnitConfig GetUnitConfig(MyEnum.ArmyType armyType)
    {
        return armyConfigs[armyType];
    }
    public MyStruct.UnitConfig GetUnitConfig(MyEnum.CityType cityType)
    {
        return cityConfigs[cityType];
    }
    Dictionary<MyEnum.ArmyType, MyStruct.UnitConfig> armyConfigs = new Dictionary<MyEnum.ArmyType, MyStruct.UnitConfig>();
    Dictionary<MyEnum.CityType, MyStruct.UnitConfig> cityConfigs = new Dictionary<MyEnum.CityType, MyStruct.UnitConfig>();
    public List<MyStruct.UnitConfig> armyConfigList = new List<MyStruct.UnitConfig>();
    public List<MyStruct.UnitConfig> cityConfigList = new List<MyStruct.UnitConfig>();
    bool flag = false;
    public void OnAfterDeserialize()
    {
        armyConfigs = new Dictionary<MyEnum.ArmyType, MyStruct.UnitConfig>();
        cityConfigs = new Dictionary<MyEnum.CityType, MyStruct.UnitConfig>();
        foreach (var unitConfig in armyConfigList)
        {
            if(armyConfigs.ContainsKey(unitConfig.armyType))
            {
                var mid = unitConfig;
                mid.armyType = MyEnum.ArmyType.None;
                armyConfigs[MyEnum.ArmyType.None] = mid;
            }
            else
            {
                armyConfigs[unitConfig.armyType] = unitConfig;
            }
        }
        foreach (var unitConfig in cityConfigList)
        {
            if(cityConfigs.ContainsKey(unitConfig.cityType))
            {
                var mid = unitConfig;
                mid.cityType = MyEnum.CityType.None;
                cityConfigs[MyEnum.CityType.None] = mid;
            }
            else
            {
                cityConfigs[unitConfig.cityType] = unitConfig;
            }
        }
        flag = true;
    }
    public void OnBeforeSerialize()
    {
        if(flag == false) return;
        // unitConfigList = new List<MyStruct.UnitConfig>();
        // foreach (var unitConfig in unitConfigs.Values)
        // {
        //     unitConfigList.Add(unitConfig);
        // }
        armyConfigList = new List<MyStruct.UnitConfig>();
        foreach (var unitConfig in armyConfigs.Values)
        {
            armyConfigList.Add(unitConfig);
        }
        cityConfigList = new List<MyStruct.UnitConfig>();
        foreach (var unitConfig in cityConfigs.Values)
        {
            cityConfigList.Add(unitConfig);
        }
        flag = false;
    }


}

