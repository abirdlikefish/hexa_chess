using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIPlayer
{
    /// <summary>
    /// 知道地形
    /// </summary>
    private HashSet<Vector2Int> _visited;

    /// <summary>
    /// 视野范围
    /// </summary>
    private HashSet<Vector2Int> _inviews;

    private List<Unit> _units;

    public AIPlayer()
    {
        _visited = new HashSet<Vector2Int>();
        _inviews = new HashSet<Vector2Int>();

    }

    public void OnInit()
    {
        Debug.Log("AIPlayer OnInit");
    }

    /// <summary>
    /// 回合行动
    /// </summary>
    public void Turn()
    {
        CalculateOperation();
        EndTurn();
    }

    private void CalculateOperation()
    {

    }

    private void CreateUnit(MyEnum.UnitType unitType, Vector2Int coordPosition)
    {
        UpdateViews();
        UpdateVisited();
    }

    private void DestroyUnit(Unit unit)
    {
        UpdateViews();

    }

    private void MoveUnit(Unit unit, Vector2Int coordPosition)
    {
    }

    private void MoveUnitOneStep(Unit unit, Vector2Int coordPosition)
    {
        UpdateViews();
        UpdateVisited();
    }

    private void AttackUnit(Unit unit, Unit target)
    {
    }

    private void EndTurn()
    {

    }


    private void UpdateVisited()
    {
        foreach (var unit in _units)
        {
            foreach (var view in GetViews(unit))
            {
                _visited.Add(view);
            }
        }
    }

    private void UpdateViews()
    {
        _inviews.Clear();
        foreach (var unit in _units)
        {
            foreach (var view in GetViews(unit))
            {
                _inviews.Add(view);
            }
        }
    }

    // todo: unit位置修改
    private List<Vector2Int> GetViews(Unit unit)
    {
        List<Vector2Int> views = new List<Vector2Int>();
        Vector2Int view = new Vector2Int();
        foreach (var dir in MyConst.MoveStep.Values)
        {
            if (MapManager.Instance.IsInMap(view + dir))
                views.Add(view + dir);
        }
        return views;
    }

    // todo: unit位置修改
    private List<Vector2Int> GetReachablePos(Unit unit)
    {
       return MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Enemy, new Vector2Int(), unit.currentAction);
    }
}
