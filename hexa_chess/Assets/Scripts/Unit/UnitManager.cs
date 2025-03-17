using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public interface UnitManagerAPI
{
    bool CheckAllOperated(MyEnum.TheOperator theOperator);//检查所有单位是否全部操作

    IUnit GetAbleUnit(MyEnum.TheOperator theOperator);//返回一个没有操作过的单位

    void RoundBeginOperation(MyEnum.TheOperator theOperator);//回合开始，对所有单位初始化

    void RoundEndOperation(MyEnum.TheOperator theOperator);//回合结束，对所有单位的血量进行回复

    // void ResetUnitList();//重置管理器设置

    //创建新单位
    void CreateNewUnit(MyEnum.TheOperator theOperator,Vector2Int coord,MyEnum.ArmyType armyType);
    void CreateNewUnit(MyEnum.TheOperator theOperator,Vector2Int coord,MyEnum.CityType cityType);

    //根据参数移除单位
    void RemoveUnit(IUnit unit);

}

// public interface UnitPoolHandler
// {
//     void InPool(GameObject gameObject);//进池操作
//     GameObject OutPool(MyEnum.UnitType unitType);//出池操作

//     bool isEmptyPool();//检测是否为空池
// }

public class UnitManager : UnitManagerAPI
{
    private static UnitManager _instance;
    private UnitManager()
    {
        ResetUnitList();
        unitFactory = new UnitFactory();
        DOTween.Init();
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

    private Dictionary<MyEnum.TheOperator , List<Unit>> unitList;

    private UnitFactory unitFactory;

    //检查是否有未操作单位
    public bool CheckAllOperated(MyEnum.TheOperator theOperator)
    {
        foreach(var i in unitList[theOperator])
        {
            if( i.GetStates() == MyEnum.UnitStates.Able) 
                return true;
        }
        return false;
    }
    
    //获取一个没有被操作过的单位
    public IUnit GetAbleUnit(MyEnum.TheOperator theOperator)
    {
        foreach(var i in unitList[theOperator])
        // foreach(var i in unitList)
        {
            if( i.GetStates() == MyEnum.UnitStates.Able) 
                return i;
        }
        return null;
    }

    public void ResetUnitList()
    {
        unitList = new Dictionary<MyEnum.TheOperator, List<Unit>>();
        // foreach (MyEnum.UnitType type in System.Enum.GetValues(typeof(MyEnum.UnitType)))
        // {
        //     unitList.Add(type,new List<Unit>());
        // }
        foreach (MyEnum.TheOperator type in System.Enum.GetValues(typeof(MyEnum.TheOperator)))
        {
            unitList.Add(type,new List<Unit>());
        }
    }

    public void RoundBeginOperation(MyEnum.TheOperator theOperator)
    {
        // Debug.LogWarning("回合开始操作");
        foreach (Unit unit in unitList[theOperator])
        {
            unit.RoundBeginCheck();
        }
    }

    public void RoundEndOperation(MyEnum.TheOperator theOperator)
    {
        foreach (var item in unitList[theOperator])
        {
            switch(item.GetOperationBuff())
            {
            case MyEnum.OperationBuff.Rest: 
                item.RecoverHp(2);
            break;
            case MyEnum.OperationBuff.Station: 
                item.RecoverHp(1);
            break;
            default:
            break;
            }
        }
    }
    public void CreateNewUnit(MyEnum.TheOperator theOperator , Vector2Int coord,MyEnum.ArmyType armyType)
    {
        // Debug.Log("加载一个单位");
        Unit unit = unitFactory.LoadUnit(theOperator,coord,armyType);
        //单位加入管理器
        unitList[theOperator].Add(unit);
        // MapManager.Instance.AddUnit(coord,unit);   
    }
    public void CreateNewUnit(MyEnum.TheOperator theOperator , Vector2Int coord,MyEnum.CityType cityType)
    {
        // Debug.Log("加载一个单位");
        Unit unit = unitFactory.LoadUnit(theOperator,coord,cityType);
        //单位加入管理器
        unitList[theOperator].Add(unit);
        // MapManager.Instance.AddUnit(coord,unit);   
    }

    public void RemoveUnit(IUnit midUnit)
    {
        Unit unit = midUnit as Unit;
        unitList[unit.TheOperator].Remove(unit);
        unit.Dead();
        return;
    }

    public void EnterGrid(Unit unit)
    {
        Vector2Int coord = unit.Coord;
        MapManager.Instance.AddUnit(coord,unit);
        if(unit.HaveZOC)    MapManager.Instance.ChangeZOC(unit.TheOperator,coord,true);
        unit.virtualArea =  MapManager.Instance.SetVirtualArea(unit.TheOperator,coord,unit.ViewRange);
        // Debug.Log("enter grid");
        // Debug.Log(unit.ViewRange);
    }

    public void ExitGrid(Unit unit)
    {
        Vector2Int coord = unit.Coord;
        MapManager.Instance.RemoveUnit(coord , unit.UnitType);
        if(unit.HaveZOC)    MapManager.Instance.ChangeZOC(unit.TheOperator,coord,false);
        MapManager.Instance.CleanVirtualArea(unit.TheOperator,unit.virtualArea);
        unit.virtualArea.Clear(); 
    }

}

