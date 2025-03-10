using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_new : MonoBehaviour, IUnit
{
    //单位配置文件
    public UnitConfig unitConfig;
    //单位当前状态
    public  UnitStates unitState;
    //生命值
    public int currentHp;
    public int currentAction;//现在的行动点数是多少

    public void UnitInitialize(UnitConfig iniConfig)
    {
        unitConfig = iniConfig;
        unitState = UnitStates.Waiting;
        GameManager.instance.AddUnitIntoPlayerUnits(this);
    }
    public void Attack(IUnit unit)
    {
        unit.GetDamage(unitConfig.Attak);
    }

    public void GetDamage(int damage)
    {
        currentHp -= damage;
    }

    public void Move()
    {
        //TODO: 这里调用移动的函数，传入当前坐标和当前行动力
    }

}
