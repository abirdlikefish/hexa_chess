using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEnum;

public class BaseCity : Unit , ICity
{
    protected List<MyEnum.ArmyType> creatableArmy;
    private MyEnum.CityType cityType;
    public MyEnum.CityType CityType => cityType;


    public List<MyEnum.ArmyType> CreatableArmy { get{ return creatableArmy; } }

    private int createArmyRange;
    public int CreateArmyRange => createArmyRange;

    protected override void InitConfig(MyStruct.UnitConfig iniConfig)
    {
        atk = iniConfig.Atk;
        def = iniConfig.Def;
        attackRadius = iniConfig.AttackRadius;
        viewRange = iniConfig.viewRange;
        coin = iniConfig.Coin;
        occupation = iniConfig.Occupation;
        haveZOC = iniConfig.HaveZOC;

        unitType = MyEnum.UnitType.City;
        cityType = iniConfig.cityType;
        maxHp = iniConfig.MaxHp;
        CurrentHP = maxHp;
        creatableArmy = iniConfig.creatableArmy;
        createArmyRange = iniConfig.createArmyRange;
        base.InitConfig(iniConfig);
    }


}
