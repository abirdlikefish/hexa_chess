using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCity : Unit , ICity
{
    protected List<MyEnum.ArmyType> creatableArmy;

    public List<MyEnum.ArmyType> CreatableArmy { get{ return creatableArmy; } }
    protected override void InitConfig(MyStruct.UnitConfig iniConfig)
    {
        maxHp = iniConfig.MaxHp;
        currentHp = maxHp;
        creatableArmy = iniConfig.creatableArmy;
        base.InitConfig(iniConfig);
    }


}
