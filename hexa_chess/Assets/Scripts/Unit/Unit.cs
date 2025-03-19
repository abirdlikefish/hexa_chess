using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Unit : MonoBehaviour, IUnit
{
    protected MyEnum.UnitType unitType;
    public MyEnum.UnitType UnitType => unitType;
    //单位配置文件
    // public MyStruct.UnitConfig unitConfig;

    //单位当前状态
    public MyEnum.UnitStates unitState;
    //操作增益
    public MyEnum.OperationBuff operationBuff;
    //生命值
    protected int maxHp;
    public int MaxHp { get { return maxHp; } }
    protected int currentHp;
    public virtual int CurrentHP {get {return currentHp;} set{currentHp = value;}}

    protected float maxMoveForce;
    private float moveForce;//当前行动力
    public float MoveForce {get {return moveForce;} set{moveForce = value;}}

    protected int atk;
    public int Atk{get {return atk + MapManager.Instance.GetAtkOffset(TheOperator , Coord);}}

    protected int def;
    public int Def{get {return def + MapManager.Instance.GetDefOffset(TheOperator , Coord);}}

    protected int attackRadius;
    public int AttackRadius{get {return attackRadius;}}

    protected int coin;
    public int Coin{get {return coin;}}

    protected int occupation;
    public int Occupation{get {return occupation;}}

    protected MyEnum.TheOperator theOperator;//阵营归属
    public MyEnum.TheOperator TheOperator => theOperator;
    protected bool haveZOC;
    public bool HaveZOC{get {return haveZOC;}set{haveZOC = value;}}
    protected int viewRange;
    public int ViewRange{get {return viewRange;}set{viewRange = value;}}
    public List<Vector2Int> virtualArea{get;set;}
    private Vector2Int coordPosition;
    public virtual Vector2Int Coord {get {return coordPosition;} set{coordPosition = value; transform.position = MapManager.Coord_To_Pos(value);}}
    public void Init(MyEnum.TheOperator theOperator , MyStruct.UnitConfig config , Vector2Int coord)
    {
        // unitConfig = iniConfig;
        unitState = MyEnum.UnitStates.Able;
        Coord = coord;
        this.theOperator = theOperator;
        InitConfig(config);
        UnitManager.Instance.EnterGrid(this);
    }
    protected virtual void InitConfig(MyStruct.UnitConfig iniConfig)
    {
        
    }
    public void Attack(IUnit other)
    {
        if (unitState == MyEnum.UnitStates.Able)
        {
            //消耗当前所有行动力
            Debug.Log("单位攻击指令执行！");
            other.GetDamage(Atk);
            MyEvent.AnimaEnd?.Invoke();
        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    public virtual void GetDamage(int damage)
    {
        int finalDamage;
        switch (operationBuff)
        {
            case MyEnum.OperationBuff.Rest: finalDamage = damage += 1;
            break;
            case MyEnum.OperationBuff.Station: finalDamage = damage - 2;
            break;
            default:    finalDamage = damage;
            break;
        }
        currentHp -= finalDamage;
        DestroyCheck();
    }


    public void Station()
    {
        moveForce = 0;
        operationBuff = MyEnum.OperationBuff.Station;
        unitState = MyEnum.UnitStates.Disable;
    }

    public void Rest()
    {
        moveForce = 0;
        operationBuff = MyEnum.OperationBuff.Rest;
        unitState = MyEnum.UnitStates.Disable;
    }

    public void Dismiss()
    {
        UnitManager.Instance.ReceiveIncome(theOperator,coin);
        UnitManager.Instance.RemoveUnit(this);
    }

    public void Skip()
    {
        moveForce = 0;
        unitState = MyEnum.UnitStates.Disable;
    }


    //摧毁检定
    private void DestroyCheck()
    {
        if (CurrentHP <= 0)
        {
            UnitManager.Instance.RemoveUnit(this);
        }
    }

    public void RecoverHp(int hp)
    {
        CurrentHP += hp;
    }

    public MyEnum.UnitStates GetStates()
    {
        return unitState;
    }


    //回合结束检定，将单位转入可操作
    public void RoundBeginCheck()
    {
        unitState = MyEnum.UnitStates.Able;
        moveForce = maxMoveForce;
    }

    public void Dead()
    {
        UnitManager.Instance.ExitGrid(this);
        Destroy(this.gameObject);
    }
}
