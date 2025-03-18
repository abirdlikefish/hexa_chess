using System.Collections.Generic;
using UnityEngine;


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

    private List<IUnit> _units
    {
        get
        {
            return UnitManager.Instance.GetUnitList(MyEnum.TheOperator.Enemy);
        }
    }

    public AIPlayer()
    {
        //_visited = new HashSet<Vector2Int>();
        //_inviews = new HashSet<Vector2Int>();

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
        Debug.Log("AIPlayer Turn");
        //CalculateOperation();
        EndTurn();
    }

    private void CalculateOperation()
    {
        foreach (var unit in _units)
        {
            CalculateOperation(unit);
        }
    }

    private void CalculateOperation(IUnit unit)
    {
        float homeDisDelta = AIHelper.Instance.GetHomeDistanceDelta();
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
            if (MapManager.Instance.GetUnit(pos, MyEnum.UnitType.Army) != null)
            {
                attackUnit = MapManager.Instance.GetUnit(pos, MyEnum.UnitType.Army);
                double attackValue = 2.5 * AIHelper.Instance.GetAttackValue(pos) *
                    (0.4 + AIHelper.Instance.GetHPPercentage(unit)) * (1.3 - 0.1 * enemyNum); ;
                attackValues.Add(attackUnit, attackValue);
            }

            if (MapManager.Instance.GetUnit(pos, MyEnum.UnitType.City) != null)
            {
                attackUnit = MapManager.Instance.GetUnit(pos, MyEnum.UnitType.City);
                double attackValue = 2.5 * AIHelper.Instance.GetAttackValue(pos) *
                    (0.4 + AIHelper.Instance.GetHPPercentage(unit)) * (1.3 - 0.1 * enemyNum); ;
                attackValues.Add(attackUnit, attackValue);
            }
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

    }

    private void MoveUnitOneStep(IUnit unit, Vector2Int coordPosition)
    {
        //UpdateViews();
        //UpdateVisited();
    }

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
