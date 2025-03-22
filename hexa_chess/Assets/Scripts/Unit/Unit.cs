using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Unit : MonoBehaviour, IUnit
{
    private string unitName;
    public string UnitName{get {return unitName;} set{unitName = value;}}
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
    public float MaxMoveForce => maxMoveForce;
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
    public bool Attack(IUnit other)
    {
        if (unitState == MyEnum.UnitStates.Able)
        {
            //消耗当前所有行动力
            MoveForce = 0;
            unitState= MyEnum.UnitStates.Disable;
            Debug.Log("单位攻击指令执行！");
            other.GetDamage(Atk);
//            MyEvent.AnimaEnd?.Invoke();
            return true;
        }
        else
        {
            Debug.Log("单位不可操作！");
            return false;
        }
    }

    public virtual void GetDamage(int damage)
    {
        int finalDamage;
        switch (operationBuff)
        {
            case MyEnum.OperationBuff.Rest: damage = damage + 1 - Def;
            break;
            case MyEnum.OperationBuff.Station: damage = damage - 2 - Def;
            break;
            default:    damage = damage - Def;
            break;
        }
        if (damage < 0) damage = 0;
        StartCoroutine(Tmp_DamageAnimation(damage));
        // DestroyCheck();
    }

    IEnumerator Tmp_DamageAnimation(int damage)
    {
        yield return new WaitForSeconds(0.3f);
        yield return VFXManager.Instance.PlayVFX(MyEnum.VFXType.Bomb, transform.position);
        MyEvent.AnimaEnd?.Invoke();
        CurrentHP -= damage;
        if(CurrentHP < 0)
            CurrentHP= 0;
        DestroyCheck();
    }


    //摧毁检定
    protected virtual void DestroyCheck()
    {
        if (CurrentHP <= 0)
        {
            UnitManager.Instance.RemoveUnit(this);
        }
    }

    public void RecoverHp(int hp)
    {
        CurrentHP += hp;
        if(CurrentHP > MaxHp)   CurrentHP= MaxHp;
    }

    public MyEnum.UnitStates GetStates()
    {
        return unitState;
    }


    //回合结束检定，将单位转入可操作
    public virtual void RoundBeginCheck()
    {
        unitState = MyEnum.UnitStates.Able;
        moveForce = maxMoveForce;
    }

    public void Dead()
    {
        UnitManager.Instance.ExitGrid(this);
        Destroy(this.gameObject);
    }

    public virtual void Show()
    {
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    public virtual void Hide()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }
}
