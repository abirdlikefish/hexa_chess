using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float GetMaxAttackDelta(Vector2Int beforePos, Vector2Int afterPos, int AttackRange)
    {
        float delta = 0;



        return delta;
    }

    public float GetHomeDelta()
    {
        float delta = 0;

        return delta;
    }

    public float SafetyDelta()
    {
        float delta = 0;

        return delta;
    }

    public float GetDamage(IUnit unit)
    {
        return unit.GetUnitConfig().Attak;
    }

    public float GetHPPercentage(IUnit unit)
    {
        float delta = 0;
        delta = (float)unit.GetUnitHp() / (float)unit.GetUnitConfig().MaxHp;
        return delta;
    }

    // todo: implement this enemys
    public float GetDistanceToEnemy(Vector2Int pos)
    {
        float res = 0;
        List<Vector2Int> enemyPos = null;
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

    // todo: implement this enemys
    public float GetEnemyNumCanAttackPos(Vector2Int pos)
    {
        float res = 0;
        List<IUnit> enemys = null;
        foreach (var enemy in enemys)
        {
            if (MapManager.GetMinCost(enemy.GetUnitPos() - pos) <= enemy.GetUnitConfig().AttackRadius)
            {
                res++;
            }
        }
        return res;
    }

    public List<Vector2Int> GetReachablePos(IUnit unit)
    {
        return MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Enemy, unit.GetUnitPos(), unit.GetActionForce());
    }
}
