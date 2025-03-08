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

    public void UnitInitialize(UnitConfig iniConfig)
    {
        unitConfig = iniConfig;
        unitState = UnitStates.Waiting;
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
        
    }

}
