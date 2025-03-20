using System;
using System.Collections.Generic;
using UnityEngine;
using static MyEnum;

public class AIHelper
{
    internal static class AIEnum
    {
        public enum AttackValueType
        {
            Home,
            Unit,
            None
        }

        public enum AIAction
        {
            Retreat,
            Attack,
            Garrison,
            Rest,
            Move,
            MoveAttack,
            RetreatRest,
            None
        }
    }

    private static class AIConst
    {
        public static Dictionary<AIEnum.AttackValueType, int> AttackValues = new Dictionary<AIEnum.AttackValueType, int>
        {
            { AIEnum.AttackValueType.Home, 10 },
            { AIEnum.AttackValueType.Unit, 5 },
            { AIEnum.AttackValueType.None, 0 }
        };


    }


    private static AIHelper instance;
    public static AIHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AIHelper();
            }
            return instance;
        }
    }

    private AIHelper()
    {
    }

    public float GetAttackValue(Vector2Int pos)
    {
        IUnit armyUnit = MapManager.Instance.GetUnit(pos, UnitType.Army);
        IUnit cityUnit = MapManager.Instance.GetUnit(pos, UnitType.City);
        return cityUnit != null ? AIConst.AttackValues[AIEnum.AttackValueType.Home] 
            : (armyUnit != null ? AIConst.AttackValues[AIEnum.AttackValueType.Unit] 
            : AIConst.AttackValues[AIEnum.AttackValueType.None]);
    }

    public float GetHomeDistanceDelta(Vector2Int coord, Vector2Int pos)
    {
        float delta = 0;
        var res = UnitManager.Instance.GetCity(TheOperator.Player,CityType.Home);
        if(res.Count == 0)
        {
            Debug.LogError("敌方没有城市");
            return 0;
        }
        Vector2Int homePos = res[0].Coord;
        float distance = MapManager.GetMinCost(homePos - coord);
        float newDistance = MapManager.GetMinCost(homePos - pos);
        delta = distance - newDistance;
        return delta;
    }

    public float SafetyDelta()
    {
        float delta = 0;

        return delta;
    }

    public float GetDamage(IUnit unit)
    {
        return unit.Atk;
    }

    public float GetHPPercentage(IUnit unit)
    {
        float delta = 0;
        delta = (float)unit.CurrentHP / (float)unit.MaxHp;
        return delta;
    }

    public float GetDistanceToEnemy(Vector2Int pos)
    {
        float res = 0;
        List<Vector2Int> enemyPos = UnitManager.Instance.GetUnitList(MyEnum.TheOperator.Player).ConvertAll(x => x.Coord);
        float minDistance = 1000;
        foreach (var enemy in enemyPos)
        {
            float distance = MapManager.GetMinCost(enemy - pos);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        res = minDistance;
        return res == 1000 ? -1 : res;
    }

    public float GetEnemyNumCanAttackPos(Vector2Int pos)
    {
        float res = MapManager.Instance.GetWatchedCnt(MyEnum.TheOperator.Player,pos);
        return res;
    }

    public List<Vector2Int> GetReachablePos(IUnit unit)
    {
        Debug.Log($"GetReachablePos:{unit.Coord} {unit.MoveForce}");
        return MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Enemy, unit.Coord, unit.MoveForce);
    }

    public List<Vector2Int> GetReachablePos(IUnit unit,float moveForce)
    {
        return MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Enemy, unit.Coord, moveForce);
    }

    internal List<Vector2Int> GetAttackablePos(IUnit unit)
    {
        return MapManager.Instance.SearchAttackArea(MyEnum.TheOperator.Enemy, unit.Coord, unit.AttackRadius);
    }

    internal List<Vector2Int> GetAttackablePos(IUnit unit, Vector2Int pos)
    {
        return MapManager.Instance.SearchAttackArea(MyEnum.TheOperator.Enemy, pos, unit.AttackRadius);
    }
}
