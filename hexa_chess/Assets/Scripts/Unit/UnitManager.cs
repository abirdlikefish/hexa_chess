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
    bool CreateNewUnit(MyEnum.TheOperator theOperator, Vector2Int coord, MyEnum.ArmyType armyType);
    bool CreateNewUnit(MyEnum.TheOperator theOperator, Vector2Int coord, MyEnum.CityType cityType);

    //根据参数移除单位
    void RemoveUnit(IUnit unit);

    List<IUnit> GetUnitList(MyEnum.TheOperator theOperator);//获取单位列表

    // 获取建筑
    public List<ICity> GetCity(MyEnum.TheOperator theOperator, MyEnum.CityType cityType);

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
        currentCoin = new Dictionary<MyEnum.TheOperator, int>();
        currentPopulation = new Dictionary<MyEnum.TheOperator, int>();
        maxPopulation = new Dictionary<MyEnum.TheOperator, int>();
        unitFactory = new UnitFactory();
        DOTween.Init();

        InitUnitManager(MyEnum.TheOperator.Player);
        InitUnitManager(MyEnum.TheOperator.Enemy);
    }

    public void InitUnitManager(MyEnum.TheOperator theOperator)
    {
        currentCoin.Add(theOperator, GameManager.instance.globalSettingSO.InitialCoin);
        currentPopulation.Add(theOperator, 0);
        maxPopulation.Add(theOperator, GameManager.instance.globalSettingSO.InitialPopulation);
    }

    //单例访问模式
    public static UnitManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UnitManager();
            }
            return _instance;
        }
    }
    //单位表
    private Dictionary<MyEnum.TheOperator, List<Unit>> unitList;
    //费
    private Dictionary<MyEnum.TheOperator, int> currentCoin;
    //人口
    private Dictionary<MyEnum.TheOperator, int> maxPopulation;
    private Dictionary<MyEnum.TheOperator, int> currentPopulation;
    private UnitFactory unitFactory;

    public List<IUnit> GetUnitList(MyEnum.TheOperator theOperator)
    {
        List<IUnit> list = new List<IUnit>();
        foreach (var item in unitList[theOperator])
        {
            list.Add(item);
        }
        return list;
    }

    //检查是否有未操作单位
    public bool CheckAllOperated(MyEnum.TheOperator theOperator)
    {
        foreach (var i in unitList[theOperator])
        {
            if (i.GetStates() == MyEnum.UnitStates.Able)
                return true;
        }
        return false;
    }

    //获取一个没有被操作过的单位
    public IUnit GetAbleUnit(MyEnum.TheOperator theOperator)
    {
        foreach (var i in unitList[theOperator])
        // foreach(var i in unitList)
        {
            if (i.GetStates() == MyEnum.UnitStates.Able)
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
            unitList.Add(type, new List<Unit>());
        }
    }

    public void RoundBeginOperation(MyEnum.TheOperator theOperator)
    {
        // Debug.LogWarning("回合开始操作");
        currentCoin[theOperator] += GameManager.instance.ReplyCost;
        foreach (Unit unit in unitList[theOperator])
        {
            unit.RoundBeginCheck();
        }
    }

    public void RoundEndOperation(MyEnum.TheOperator theOperator)
    {
        foreach (var item in unitList[theOperator])
        {
            switch (item.operationBuff)
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
    public bool CreateNewUnit(MyEnum.TheOperator theOperator, Vector2Int coord, MyEnum.ArmyType armyType)
    {
        MyStruct.UnitConfig unitConfig = unitFactory.UnitConfigListSO.GetUnitConfig(armyType);
        if (CheckCoinConsumption(theOperator, unitConfig.Coin) && CheckPopulation(theOperator, unitConfig.Occupation))
        {
            CostCoinAndPopulation(theOperator, unitConfig.Coin, unitConfig.Occupation);
            // Debug.Log("加载一个单位");
            Unit unit = unitFactory.LoadUnit(theOperator, coord, armyType);
            //单位加入管理器
            unitList[theOperator].Add(unit);
            currentPopulation[theOperator]++;
            // MapManager.Instance.AddUnit(coord,unit);
            return true;
        }
        else return false;
    }
    public bool CreateNewUnit(MyEnum.TheOperator theOperator, Vector2Int coord, MyEnum.CityType cityType)
    {
        if (CheckCoinConsumption(theOperator, unitFactory.UnitConfigListSO.GetUnitConfig(cityType).Coin))
        {
            // Debug.Log("加载一个单位");
            Unit unit = unitFactory.LoadUnit(theOperator, coord, cityType);
            //单位加入管理器
            unitList[theOperator].Add(unit);
            return true;
        }
        // MapManager.Instance.AddUnit(coord,unit);   
        else return false;
    }

    public void RemoveUnit(IUnit midUnit)
    {
        Unit unit = midUnit as Unit;
        unitList[unit.TheOperator].Remove(unit);
        if (unit.UnitType == MyEnum.UnitType.Army) currentPopulation[unit.TheOperator]--;
        unit.Dead();
        return;
    }

    public void EnterGrid(Unit unit)
    {
        Vector2Int coord = unit.Coord;
        MapManager.Instance.AddUnit(coord, unit);
        if (unit.HaveZOC) MapManager.Instance.ChangeZOC(unit.TheOperator, coord, true);
        if(unit.UnitType == MyEnum.UnitType.Army)
            unit.virtualArea = MapManager.Instance.SetVirtualArea(unit.TheOperator, coord, unit.ViewRange);
        else
        {
            unit.virtualArea = MapManager.Instance.SetVirtualArea_noHeight(unit.TheOperator, coord, unit.ViewRange);
            Debug.Log($"enter grid:{unit.UnitType} {unit.ViewRange}");
        }
        // Debug.Log("enter grid");
        // Debug.Log(unit.ViewRange);
    }

    public void ExitGrid(Unit unit)
    {
        Vector2Int coord = unit.Coord;
        MapManager.Instance.RemoveUnit(coord, unit.UnitType);
        if (unit.HaveZOC) MapManager.Instance.ChangeZOC(unit.TheOperator, coord, false);
        MapManager.Instance.CleanVirtualArea(unit.TheOperator, unit.virtualArea);
        unit.virtualArea.Clear();
    }

    //查费
    public int GetCoin(MyEnum.TheOperator theOperator)
    {
        return currentCoin[theOperator];
    }

    public int GetPopulation(MyEnum.TheOperator theOperator)
    {
        return currentPopulation[theOperator];
    }

    private bool CheckCoinConsumption(MyEnum.TheOperator theOperator, int coin)
    {
        if (currentCoin[theOperator] >= coin)
        {
            // currentCoin[theOperator] -= coin;
            return true;
        }
        else
        {
            Debug.Log("金币不足");
            return false;
        }

    }

    private bool CheckPopulation(MyEnum.TheOperator theOperator, int occupation)
    {
        if (currentPopulation[theOperator] + occupation <= maxPopulation[theOperator])
        {
            // currentPopulation[theOperator]+=occupation;
            return true;
        }
        else
        {
            Debug.Log("人口不足");
            return false;
        }
    }

    private void CostCoinAndPopulation(MyEnum.TheOperator theOperator, int coin, int occupation)
    {
        currentCoin[theOperator] -= coin;
        currentPopulation[theOperator] += occupation;
    }
    public void ReceiveIncome(MyEnum.TheOperator theOperator, int coin)
    {
        currentCoin[theOperator] += coin;
    }

    public List<ICity> GetCity(MyEnum.TheOperator theOperator, MyEnum.CityType cityType)
    {
        List<ICity > cityList = new List<ICity>();
        foreach (var item in unitList[theOperator])
        {
            if (item.UnitType == MyEnum.UnitType.City && (item as ICity).CityType == cityType)
            {
                cityList.Add(item as ICity);
            }
        }
        return cityList;
    }
}

