using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitManagerHandler
{
    bool CheckAllOperated();//检查所有单位是否全部操作

    IUnit GetAbleUnit();//返回一个没有操作过的单位

    void RoundBeginOperation();//回合开始，对所有单位初始化

    void ResetManager();//游戏结束，重置管理器
}

public interface UnitPoolHandler
{
    void Inpool(GameObject gameObject);//进池操作
    GameObject OutPool(UnitType unitType);//出池操作

    bool isEmptyPool();//检测是否为空池
}

public class UnitManager : UnitManagerHandler
{
    public UnitManager()
    {
        manager = new Dictionary<UnitType, List<GameObject>>();
    }
    private Dictionary<UnitType,List<GameObject>> manager;

    //检查是否有未操作单位
    public bool CheckAllOperated()
    {
        foreach(var i in manager)
        {
            var temp = i.Value;
            foreach(var item in temp)
            {
                
                if( item.GetComponent<IUnit>().GetStates() == UnitStates.Able) 
                    return true;
            }
        }
        return false;
    }

    public IUnit GetAbleUnit()
    {
        foreach(var i in manager)
        {
            var temp = i.Value;
            foreach(var item in temp)
            {
                
                if( item.GetComponent<IUnit>().GetStates() == UnitStates.Able) 
                    return item.GetComponent<IUnit>();
            }
        }
        return null;
    }

    public void ResetManager()
    {
        manager.Clear();
    }

    public void RoundBeginOperation()
    {

    }
}
