using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        maxHp = iniConfig.MaxHp;
        currentHp = maxHp;
        creatableArmy = iniConfig.creatableArmy;
        createArmyRange = iniConfig.createArmyRange;
        base.InitConfig(iniConfig);
    }


}
