using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class Unit : MonoBehaviour, IUnit
{
    //单位配置文件
    public UnitConfig unitConfig;
    //单位当前状态
    public MyEnum.UnitStates unitState;
    //操作增益
    public MyEnum.OprationBuff oprationBuff;
    //生命值
    public int currentHp;

    public float currentAction;//当前行动力

    public MyEnum.TheOperator theOperator;//阵营归属

    public Vector2Int croodPosition;



    public void UnitInitialize(UnitConfig iniConfig)
    {
        unitConfig = iniConfig;
        unitState = MyEnum.UnitStates.Able;
        DOTween.Init();
    }
    public void Attack(IUnit other)
    {
        if (unitState == MyEnum.UnitStates.Able)
        {
            //消耗当前所有行动力
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
            case MyEnum.OprationBuff.Rest: finalDamage = damage += 1;
            break;
            case MyEnum.OprationBuff.Station: finalDamage = damage - 2;
            break;
            default:    finalDamage = damage;
            break;
        }
        currentHp -= finalDamage;
        DestroyCheck();
    }

    public void Move(List<Vector2Int> path,float cost)
    {
        if (unitState == MyEnum.UnitStates.Able)
        {
            IUnit temp = MapManager.Instance.GetUnit(croodPosition);
            Debug.Log("移动！");
            Sequence sequence = DOTween.Sequence();
            foreach (var point in path)
            {
                sequence.Append(
                    transform.DOMove((Vector3)MapManager.Coord_To_Pos(point),
                    unitConfig.movingSpeed));
            }
            sequence.OnPlay(() => {
                unitState = MyEnum.UnitStates.Disable;
                Debug.Log("单位移动中，不可操作");
                MapManager.Instance.RemoveUnit(croodPosition);
            });
            sequence.Play();
            sequence.OnComplete(() => {
                unitState = MyEnum.UnitStates.Able;
                Debug.Log("移动动画完成");
                MapManager.Instance.AddUnit(path.Last(),temp);
            });
                MyEvent.AnimaEnd();
            currentAction -= cost;

        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    public void Station()
    {
        currentAction = 0;
        oprationBuff = MyEnum.OprationBuff.Station;
        unitState = MyEnum.UnitStates.Disable;
    }

    public void Rest()
    {
        currentAction = 0;
        oprationBuff = MyEnum.OprationBuff.Rest;
        unitState = MyEnum.UnitStates.Disable;
    }

    public void Dismiss()
    {
        ReturnCost();
        RecycleUnit();
    }

    public void Skip()
    {
        currentAction = 0;
        unitState = MyEnum.UnitStates.Disable;
    }


    //摧毁检定
    private void DestroyCheck()
    {
        if (currentHp <= 0)
        {
            Debug.Log("单位被摧毁");
            UnitManager.Instance.RemoveUnit(this);
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
        UnitManager.Instance.RemoveUnit(this);
        Destroy(gameObject);
        //todo:回收对象池
    }

    public void RecoverHp(int hp)
    {
        currentHp += hp;
    }

    public MyEnum.UnitStates GetStates()
    {
        return unitState;
    }

    public MyEnum.TheOperator GetOperater()
    {
        return theOperator;
    }

    public void ReWriteCrood(Vector2Int crood)
    {
        croodPosition = crood;
    }

    public Vector2Int GetUnitPos()
    {
        return croodPosition;
    } 

    public float GetActionForce()
    {
        return currentAction;
    }

    //回合结束检定，将单位转入可操作
    public void RoundBeginCheck()
    {
        unitState = MyEnum.UnitStates.Able;
        currentAction = unitConfig.Action;
    }

    public MyEnum.OprationBuff GetOprationBuff()
    {
        return oprationBuff;
    }

    public MyEnum.UnitType GetUnitType()
    {
        return unitConfig.unitType;
    }

    public int GetUnitHp()
    {
        return currentHp;
    }

    public UnitConfig GetUnitConfig()
    {
        return unitConfig;
    }
}
