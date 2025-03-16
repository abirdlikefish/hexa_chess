using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface UnitManagerAPI
{
    bool CheckAllOperated();//检查所有单位是否全部操作

    IUnit GetAbleUnit();//返回一个没有操作过的单位

    void RoundBeginOperation();//回合开始，对所有单位初始化

    void RoundEndOperation();//回合结束，对所有单位的血量进行回复

    void ResetManager();//重置管理器设置

    //创建新单位
    void CreateNewUnit(Vector2Int crood,MyEnum.UnitType unitType);

    //根据参数移除单位
    void RemoveUnit(IUnit unit);

}

public interface UnitPoolHandler
{
    void Inpool(GameObject gameObject);//进池操作
    GameObject OutPool(MyEnum.UnitType unitType);//出池操作

    bool isEmptyPool();//检测是否为空池
}

public class UnitManager : UnitManagerAPI
{
    private static UnitManager _instance;
    private UnitManager()
    {
        manager = new Dictionary<MyEnum.UnitType, List<IUnit>>();
    }

    //单例访问模式
    public static UnitManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UnitManager();
            }
            return _instance;
        }
    }

    private Dictionary<MyEnum.UnitType,List<IUnit>> manager;

    //检查是否有未操作单位
    public bool CheckAllOperated()
    {
        foreach(var i in manager)
        {
            var temp = i.Value;
            foreach(var item in temp)
            {
                
                if( item.GetStates() == MyEnum.UnitStates.Able) 
                    return true;
            }
        }
        return false;
    }

    //获取一个没有被操作过的单位
    public IUnit GetAbleUnit()
    {
        foreach(var i in manager)
        {
            var temp = i.Value;
            foreach(var item in temp)
            {
                
                if( item.GetStates() == MyEnum.UnitStates.Able) 
                    return item;
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
        foreach (var type in manager)
        {
            foreach (var item in type.Value)
            {
                item.RoundBeginCheck();
            }
        }
    }

    public void RoundEndOperation()
    {
        foreach (var type in manager)
        {
            foreach (var item in type.Value)
            {
                switch(item.GetOprationBuff())
                {
                    case MyEnum.OprationBuff.Rest: item.RecoverHp(2);
                    break;
                    case MyEnum.OprationBuff.Station: item.RecoverHp(1);
                    break;
                    default:
                    break;
                }
            }
        }
    }
    public void CreateNewUnit(Vector2Int crood,MyEnum.UnitType unitType)
    {
        Debug.Log("加载一个单位");
        //单位加入管理器
        manager[unitType].Add(UnitFactory.LoadUnit(crood,unitType));
        
    }

    public void RemoveUnit(IUnit unit)
    {
        var item = manager[unit.GetUnitType()];
        foreach (var i in item)
        {
            if( i == unit)
            {
                manager[unit.GetUnitType()].Remove(unit);
                break;
            }
        }
        //从地图中移除单位
        MapManager.Instance.RemoveUnit(MapManager.Pos_To_Coord(unit.GetUnitPos()));
        return;
    }

}

