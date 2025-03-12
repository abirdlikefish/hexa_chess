using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    //单位配置文件
    public UnitConfig unitConfig;
    //单位当前状态
    public  UnitStates unitState;
    //操作增益
    public OprationBuff oprationBuff;
    //生命值
    public int currentHp;

<<<<<<< .mine
    public void InitInformation(MyEnum.UnitType _unitType, int _maxHp, int _action, int _attack, int _attackRadius,
        int _coin, int _occupation, bool _haveZOC)



=======
    public int currentAction;//当前行动力



    public void UnitInitialize(UnitConfig iniConfig)
>>>>>>> .theirs
    {
<<<<<<< .mine
        SelfInformation.unitType = _unitType;
        SelfInformation.Action = _action;
        SelfInformation.MaxHp = _maxHp;
        SelfInformation.Attak = _attack;
        SelfInformation.Coin = _coin;
        SelfInformation.AttackRadius = _attackRadius;
        SelfInformation.Occupation = _occupation;
        SelfInformation.HaveZOC = _haveZOC;
        
        currentState = new UnitState(this, MyEnum.UnitStates.Action);//所有单位初始成等待操作的状态
        CurrentHp = SelfInformation.MaxHp;
        CurrentAction = 0;//刚出生的时候行动力为0
=======
        unitConfig = iniConfig;
        unitState = UnitStates.Able;










>>>>>>> .theirs
    }
    public void Attack(IUnit other)
    {
        if(unitState == UnitStates.Able)
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
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
        int finalDamage;
        switch (oprationBuff)
        {
            case OprationBuff.Rest: finalDamage = damage += 1;
            break;
            default:    finalDamage = damage;
            break;
        }
        currentHp -= finalDamage;
        DestroyCheck();
    }

    public void Move(int cost)
    {
        if(unitState == UnitStates.Able)
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
        if(unitState == UnitStates.Able && currentAction != 0)   
        {
            currentAction -= ActionCost;
            if(currentAction < 0)   currentAction = 0;
        } 
        if(currentAction == 0)  unitState = UnitStates.Disable;
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
        if(currentHp <= 0)
        {
            Debug.Log("单位被摧毁");
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
}
