using System;
using System.Collections.Generic;
using UnityEngine;
using static AIHelper.AIEnum;

public class AIPlayer
{
    ///// <summary>
    ///// 知道地形
    ///// </summary>
    //private HashSet<Vector2Int> _visited;

    ///// <summary>
    ///// 视野范围
    ///// </summary>
    //private HashSet<Vector2Int> _inviews;
    private AICreateUnitSO aiCreateUnitSO;
    private List<IUnit> _units
    {
        get
        {
            return UnitManager.Instance.GetUnitList(MyEnum.TheOperator.Enemy);
        }
    }

    public AIPlayer(AICreateUnitSO aiCreateUnitSO)
    {
        //_visited = new HashSet<Vector2Int>();
        //_inviews = new HashSet<Vector2Int>();
        this.aiCreateUnitSO = aiCreateUnitSO;
    }

    public void OnInit()
    {
        Debug.Log("AIPlayer OnInit");
    }

    /// <summary>
    /// 回合行动
    /// </summary>
    public void Turn(int round)
    {
        Debug.Log($"AIPlayer Turn: Round {round}");
        CreateUnit(round);
        CalculateOperation();
        EndTurn();
    }

    private void CreateUnit(int round)
    {
        foreach (var aiCreateUnit in aiCreateUnitSO.aiCreateUnits)
        {
            if (aiCreateUnit.round == round)
            {
                if (aiCreateUnit.unitType == MyEnum.UnitType.City)
                {
                    var city = UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Enemy,
                        aiCreateUnit.createPos, aiCreateUnit.cityType);
                    Debug.Log($"AIPlayer CreateCity: {city} {aiCreateUnit.cityType}");
                }
                else
                {
                    var army = UnitManager.Instance.CreateNewUnit(MyEnum.TheOperator.Enemy, 
                        aiCreateUnit.createPos, aiCreateUnit.armyType);
                    Debug.Log($"AIPlayer CreateArmy: {army} {aiCreateUnit.armyType}");
                }
            }
        }
    }

    private void CalculateOperation()
    {
        Debug.Log($"AIPlayer CalculateOperation Start:{_units.Count}");
        foreach (var unit in _units)
        {
            CalculateOperation(unit);
        }
    }

    private void CalculateOperation(IUnit unit)
    {
        if (unit == null || unit is ICity)
        {
            return;
        }

        float homeDisDelta = 0;
        float safetyDelta = AIHelper.Instance.SafetyDelta();
        float damage = AIHelper.Instance.GetDamage(unit);
        float hpPercentage = AIHelper.Instance.GetHPPercentage(unit);
        float distanceToEnemy = AIHelper.Instance.GetDistanceToEnemy(unit.Coord);
        float enemyNum = AIHelper.Instance.GetEnemyNumCanAttackPos(unit.Coord);

        List<Vector2Int> moveablePos = AIHelper.Instance.GetReachablePos(unit);
        List<Vector2Int> attackablePos = AIHelper.Instance.GetAttackablePos(unit);
        //计算撤退
        Dictionary<Vector2Int, double> retreatValues = new();
        foreach (var pos in moveablePos)
        {
            var newEnemyNum = AIHelper.Instance.GetEnemyNumCanAttackPos(pos);
            double retreatValue = 4 * Mathf.Pow(1 - hpPercentage, 3) * (newEnemyNum * 1.5 + 1)
                / (Mathf.Sqrt(distanceToEnemy) + 0.2);
            retreatValues.Add(pos, retreatValue);
        }

        //计算进攻
        Dictionary<IUnit, double> attackValues = new();
        foreach (var pos in attackablePos)
        {
            IUnit attackUnit = null;
            IUnit cityUnit = MapManager.Instance.GetUnit(pos, MyEnum.UnitType.City);
            IUnit armyUnit = MapManager.Instance.GetUnit(pos, MyEnum.UnitType.Army);

            attackUnit = cityUnit != null ? cityUnit : armyUnit;
            if (attackUnit == null)
            {
                continue;
            }
            double attackValue = 2.5 * AIHelper.Instance.GetAttackValue(pos) *
                (0.4 + AIHelper.Instance.GetHPPercentage(attackUnit)) * (1.3 - 0.1 * enemyNum); ;
            attackValues.Add(attackUnit, attackValue);
        }

        // 计算驻扎
        double garrisionValue = (3 - hpPercentage) * (1 + 2 / (distanceToEnemy + 1));

        // 计算休息
        double restValue = (3 - 1.5 * hpPercentage) * (distanceToEnemy / 3.0 + 0.5);

        //移动
        Dictionary<Vector2Int, double> moveValues = new();
        foreach (var pos in moveablePos)
        {
            var newEnemyNum = AIHelper.Instance.GetEnemyNumCanAttackPos(pos);
            double moveValue = (0.5 + hpPercentage) * (1 + 0.3 * newEnemyNum) * 5;
            moveValues.Add(pos, moveValue);
        }

        List<Vector2Int> moveableCanRestOrAttackPos = AIHelper.Instance.GetReachablePos(unit, unit.MoveForce - 0.5f);

        // 移动攻击
        Dictionary<Vector2Int, KeyValuePair<IUnit, double>> moveAttackValues = new();
        foreach (var pos in moveableCanRestOrAttackPos)
        {
            List<Vector2Int> attackablePos2 = AIHelper.Instance.GetAttackablePos(unit, pos);
            homeDisDelta = AIHelper.Instance.GetHomeDistanceDelta(unit.Coord, pos);
            double maxMoveAttackValue = 0;
            foreach (var attackPos in attackablePos2)
            {
                IUnit attackUnit = null;
                IUnit cityUnit = MapManager.Instance.GetUnit(attackPos, MyEnum.UnitType.City);
                IUnit armyUnit = MapManager.Instance.GetUnit(attackPos, MyEnum.UnitType.Army);

                attackUnit = cityUnit != null ? cityUnit : armyUnit;
                if (attackUnit == null)
                {
                    continue;
                }
                double attackValue = 2.5 * AIHelper.Instance.GetAttackValue(attackPos) *
                    (0.4 + AIHelper.Instance.GetHPPercentage(attackUnit)) * (1.3 - 0.1 * enemyNum);
                double moveAttackValue = attackValue + 0.3 * homeDisDelta;
                if (moveAttackValue > maxMoveAttackValue)
                {
                    maxMoveAttackValue = moveAttackValue;
                    moveAttackValues[pos] = new KeyValuePair<IUnit, double>(attackUnit, moveAttackValue);
                }
            }
        }

        //撤退休息
        Dictionary<Vector2Int, double> retreatRestValues = new();
        foreach (var pos in moveablePos)
        {
            homeDisDelta = AIHelper.Instance.GetHomeDistanceDelta(unit.Coord, pos);
            double newRestValue = (3 - 1.5 * hpPercentage) * (AIHelper.Instance.GetDistanceToEnemy(pos) / 3 + 0.5);
            double retreatRestValue = restValue + homeDisDelta * 0.1;
            retreatRestValues.Add(pos, retreatRestValue);
        }

        // 选取
        Vector2Int targetPos = new Vector2Int();
        IUnit targetUnit = null;
        double maxValue = 0;
        AIAction aiAction = AIAction.None;
        foreach (var (k, v) in retreatValues)
        {
            if (v > maxValue)
            {
                maxValue = v;
                targetPos = k;
                aiAction = AIAction.Retreat;
            }
        }
        foreach (var (k, v) in attackValues)
        {
            if (v > maxValue)
            {
                maxValue = v;
                targetUnit = k;
                aiAction = AIAction.Attack;
            }
        }
        if (garrisionValue > maxValue)
        {
            aiAction = AIAction.Garrison;
        }
        if (restValue > maxValue)
        {
            aiAction = AIAction.Rest;
        }
        foreach (var (k, v) in moveValues)
        {
            if (v > maxValue)
            {
                maxValue = v;
                targetPos = k;
                aiAction = AIAction.Move;
            }
        }
        foreach (var (k, v) in moveAttackValues)
        {
            if (v.Value > maxValue)
            {
                maxValue = v.Value;
                targetPos = k;
                targetUnit = v.Key;
                aiAction = AIAction.MoveAttack;
            }
        }
        foreach (var (k, v) in retreatRestValues)
        {
            if (v > maxValue)
            {
                maxValue = v;
                targetPos = k;
                aiAction = AIAction.RetreatRest;
            }
        }
        Debug.Log($"AIPlayer CalculateOperation End:{aiAction}");
        switch (aiAction)
        {
            case AIAction.Retreat:
                MoveUnit(unit, targetPos);
                break;
            case AIAction.Attack:
                AttackUnit(unit, targetUnit);
                break;
            case AIAction.Garrison:
                GarrisonUnit(unit);
                break;
            case AIAction.Rest:
                RestUnit(unit);
                break;
            case AIAction.Move:
                MoveUnit(unit, targetPos);
                break;
            case AIAction.MoveAttack:
                MoveUnit(unit, targetPos);
                AttackUnit(unit, targetUnit);
                break;
            case AIAction.RetreatRest:
                MoveUnit(unit, targetPos);
                RestUnit(unit);
                break;
            case AIAction.None:
                break;
        }
    }

    private void RestUnit(IUnit unit)
    {
        (unit as IArmy)?.Rest();
    }

    private void GarrisonUnit(IUnit unit)
    {
        (unit as IArmy)?.Station();
    }

    private void CreateUnit(MyEnum.UnitType unitType, Vector2Int coordPosition)
    {
        //UpdateViews();
        //UpdateVisited();
    }

    private void DestroyUnit(IUnit unit)
    {
        //UpdateViews();

    }

    private void MoveUnit(IUnit unit, Vector2Int coordPosition)
    {
        MapManager.Instance.SearchMovableArea(unit.TheOperator, unit.Coord, unit.MoveForce);
        var movePath = MapManager.Instance.GetMovePath(coordPosition, out var moveCost);
        (unit as IArmy)?.Move(movePath, moveCost);
    }

    //private void MoveUnitOneStep(IUnit unit, Vector2Int coordPosition)
    //{
    //    //UpdateViews();
    //    //UpdateVisited();
    //}

    private void AttackUnit(IUnit unit, IUnit target)
    {
        unit.Attack(target);
    }

    private void EndTurn()
    {
        Debug.Log("AIPlayer EndTurn");
        AIManager.Instance.EndTurn();
    }


    //private void UpdateVisited()
    //{
    //    foreach (var unit in _units)
    //    {
    //        foreach (var view in GetViews(unit))
    //        {
    //            _visited.Add(view);
    //        }
    //    }
    //}

    //private void UpdateViews()
    //{
    //    _inviews.Clear();
    //    foreach (var unit in _units)
    //    {
    //        foreach (var view in GetViews(unit))
    //        {
    //            _inviews.Add(view);
    //        }
    //    }
    //}

    //// todo: unit位置修改
    //private List<Vector2Int> GetViews(Unit unit)
    //{
    //    List<Vector2Int> views = new List<Vector2Int>();
    //    Vector2Int view = new Vector2Int();
    //    foreach (var dir in MyConst.MoveStep.Values)
    //    {
    //        if (MapManager.Instance.IsInMap(view + dir))
    //            views.Add(view + dir);
    //    }
    //    return views;
    //}

}
