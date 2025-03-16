using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class Unit : MonoBehaviour, IUnit, IUnitManagerOp
{
    //单位配置文件
    public UnitConfig unitConfig;
    //单位当前状态
    public UnitStates unitState;
    //操作增益
    public OprationBuff oprationBuff;
    //生命值
    public int currentHp;

    public int currentAction;//当前行动力

    public bool friendUnit;//阵营归属

    public Vector2 position;



    public void UnitInitialize(UnitConfig iniConfig)
    {
        unitConfig = iniConfig;
        unitState = UnitStates.Able;
        DOTween.Init();
    }
    public void Attack(IUnit other)
    {
        if (unitState == UnitStates.Able)
        {
            //消耗当前所有行动力
            ActionCheck(currentAction);
            Debug.Log("单位攻击指令执行！");
            other.GetDamage(unitConfig.Attak);
        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    public void GetDamage(int damage)
    {
        int finalDamage;
        switch (oprationBuff)
        {
            case OprationBuff.Rest: finalDamage = damage += 1;
            break;
            case OprationBuff.Station: finalDamage = damage - 2;
            break;
            default:    finalDamage = damage;
            break;
        }
        currentHp -= finalDamage;
        DestroyCheck();
    }

    public void Move(List<Vector2Int> path,int cost)
    {
        if (unitState == UnitStates.Able)
        {
            IUnit temp = MapManager.Instance.GetUnit(MapManager.Pos_To_Coord(position));
            Debug.Log("移动！");
            Sequence sequence = DOTween.Sequence();
            foreach (var point in path)
            {
                sequence.Append(
                    transform.DOMove((Vector3)MapManager.Coord_To_Pos(point),
                    unitConfig.movingSpeed));
            }
            sequence.OnPlay(() => {
                unitState = UnitStates.Disable;
                Debug.Log("单位移动中，不可操作");
                MapManager.Instance.RemoveUnit(MapManager.Pos_To_Coord(position));
            });
            sequence.Play();
            sequence.OnComplete(() => {
                unitState = UnitStates.Able;
                Debug.Log("移动动画完成");
                MapManager.Instance.AddUnit(path.Last(),temp);
            });
                MyEvent.AnimaEnd();
            ActionCheck(cost);

        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    public void Station()
    {
        ActionCheck(currentAction);
        oprationBuff = OprationBuff.Station;
    }

    public void Rest()
    {
        ActionCheck(currentAction);
        oprationBuff = OprationBuff.Rest;
    }

    public void Dismiss()
    {
        ReturnCost();
        RecycleUnit();
    }

    public void Skip()
    {
        ActionCheck(currentAction);
    }

    //行动力检定，判断单位操作是否转入无法操作
    private void ActionCheck(int ActionCost)
    {
        if (unitState == UnitStates.Able && currentAction != 0)
        {
            currentAction -= ActionCost;
            if (currentAction < 0) currentAction = 0;
        }
        if (currentAction == 0) unitState = UnitStates.Disable;
    }

    //摧毁检定
    private void DestroyCheck()
    {
        if (currentHp <= 0)
        {
            Debug.Log("单位被摧毁");
            UnitManager.Instance.RemoveUnit(this,unitConfig.unitType);
            Destroy(gameObject);
        }
    }

    //资源返还函数，如何返还存疑
    private void ReturnCost()
    {
        Debug.Log("返还资源！");
    }

    //回收单位
    private void RecycleUnit()
    {
        Debug.Log("回收单位！");
        UnitManager.Instance.RemoveUnit(this,unitConfig.unitType);
        Destroy(gameObject);
        //todo:回收对象池
    }

    public void RecoverHp(int hp)
    {
        currentHp += hp;
    }

    public UnitStates GetStates()
    {
        return unitState;
    }

    public bool isFriendUnit()
    {
        return friendUnit;
    }

    public void ReWritePosition(Vector2 vector2)
    {
        position = vector2;
    }

    public Vector2 GetUnitPos()
    {
        return position;
    } 

    public int GetActionForce()
    {
        return currentAction;
    }

    //回合结束检定，将单位转入可操作
    public void RoundBeginCheck()
    {
        unitState = UnitStates.Able;
        currentAction = unitConfig.Action;
    }

    public OprationBuff GetOprationBuff()
    {
        return oprationBuff;
    }
}
