using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    //单位配置文件
    public UnitConfig unitConfig;
    //单位当前状态
    public UnitStates unitState;
    //操作增益
    public OprationBuff oprationBuff;
    //生命值
    public int currentHp;

    public float currentAction;//当前行动力

    public bool friendUnit;//阵营归属

    public Vector2 position;



    public void UnitInitialize(UnitConfig iniConfig)
    {
        unitConfig = iniConfig;
        unitState = UnitStates.Able;
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
            case OprationBuff.Rest:
                finalDamage = damage += 1;
                break;
            default:
                finalDamage = damage;
                break;
        }
        currentHp -= finalDamage;
        DestroyCheck();
    }

    public void Move(int cost)
    {
        if (unitState == UnitStates.Able)
        {
            Debug.Log("移动！");
            //todo:棋子寻路和移动方式
            ActionCheck(cost);
        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    public void Station()
    {

    }

    public void Rest()
    {
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

    //回合结束检定，将单位转入可操作
    private void RoundEndCheck()
    {
        unitState = UnitStates.Able;
        currentAction = unitConfig.Action;
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

    private void RecycleUnit()
    {
        Debug.Log("回收单位！");
        Destroy(gameObject);
        //todo:回收对象池
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
}
